using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using UnityEngine;

namespace jeanf.EventSystem
{
    public static class EventDiagnostics
    {
        public const int BufferCapacity = 5000;

        public static bool TrackingEnabled = false;
        public static bool CaptureSenderStack = false;
        public static bool CaptureReceivers = false;

        const BindingFlags DelegateFieldFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        public static int MainThreadId { get; private set; } = -1;

        public sealed class ChannelRecord
        {
            public int key;
            public string guid;
            public string typeName;
            public int firstSeenThreadId;
            public bool seenOffMainThread;
            public double lastSeenTime;
            public WeakReference<ScriptableObject> instance;
        }

        public struct RaiseRecord
        {
            public long seq;
            public double time;
            public int key;
            public string guid;
            public string typeName;
            public string payload;
            public int threadId;
            public bool offMainThread;
            public string senderType;
            public string senderMethod;
            public UnityEngine.Object senderObject;
            public string receivers;
        }

        public sealed class PublisherRecord
        {
            public string typeName;
            public WeakReference<Component> component;
        }

        public sealed class StackSenderRecord
        {
            public string typeName;
            public string method;
            public int count;
        }

        static readonly Dictionary<int, ChannelRecord> Channels = new Dictionary<int, ChannelRecord>();
        static readonly Dictionary<string, Dictionary<int, PublisherRecord>> Publishers =
            new Dictionary<string, Dictionary<int, PublisherRecord>>();
        static readonly Dictionary<string, Dictionary<string, StackSenderRecord>> StackSenders =
            new Dictionary<string, Dictionary<string, StackSenderRecord>>();
        [ThreadStatic] static Stack<IEventPublisher> _ambient;
        static readonly RaiseRecord[] Buffer = new RaiseRecord[BufferCapacity];
        static int bufferHead;
        static int bufferCount;
        static long seqCounter;
        static readonly Stopwatch Clock = Stopwatch.StartNew();
        static readonly object Gate = new object();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void CaptureMainThread()
        {
            MainThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        static bool IsMainThread => Thread.CurrentThread.ManagedThreadId == MainThreadId;

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void NoteSeen(ScriptableObject channel)
        {
            if (channel == null) return;
            if (!Application.isPlaying) return;

            int key = RuntimeHelpers.GetHashCode(channel);
            int tid = Thread.CurrentThread.ManagedThreadId;
            bool offMain = MainThreadId != -1 && tid != MainThreadId;
            string guid = (channel as SerializableScriptableObject)?.Guid;
            string typeName = channel.GetType().Name;
            double now = Clock.Elapsed.TotalSeconds;

            lock (Gate)
            {
                if (!Channels.TryGetValue(key, out var rec))
                {
                    rec = new ChannelRecord
                    {
                        key = key,
                        guid = guid,
                        typeName = typeName,
                        firstSeenThreadId = tid,
                        instance = new WeakReference<ScriptableObject>(channel),
                    };
                    Channels[key] = rec;
                }
                if (offMain) rec.seenOffMainThread = true;
                if (string.IsNullOrEmpty(rec.guid) && !string.IsNullOrEmpty(guid)) rec.guid = guid;
                rec.lastSeenTime = now;
            }
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void RecordRaise(ScriptableObject channel, object payload)
        {
            if (!TrackingEnabled) return;
            if (channel == null) return;

            NoteSeen(channel);

            int tid = Thread.CurrentThread.ManagedThreadId;
            bool main = MainThreadId == -1 || tid == MainThreadId;
            string senderType = null;
            string senderMethod = null;
            UnityEngine.Object senderObject = null;

            var ambient = CurrentSender;
            if (ambient != null)
            {
                senderType = ambient.GetType().Name;
                senderObject = ambient.Source;
                RegisterPublisher(ambient, channel);
            }
            else if (CaptureSenderStack)
            {
                ResolveSender(out senderType, out senderMethod);
                if (senderType != null)
                {
                    string gk = GroupKey((channel as SerializableScriptableObject)?.Guid, RuntimeHelpers.GetHashCode(channel));
                    RegisterStackSender(gk, senderType, senderMethod);
                }
            }

            string receivers = CaptureReceivers ? ResolveReceivers(channel) : null;

            var rec = new RaiseRecord
            {
                time = Clock.Elapsed.TotalSeconds,
                key = RuntimeHelpers.GetHashCode(channel),
                guid = (channel as SerializableScriptableObject)?.Guid,
                typeName = channel.GetType().Name,
                payload = PayloadToString(payload, main),
                threadId = tid,
                offMainThread = !main,
                senderType = senderType,
                senderMethod = senderMethod,
                senderObject = senderObject,
                receivers = receivers,
            };

            lock (Gate)
            {
                rec.seq = ++seqCounter;
                Buffer[bufferHead] = rec;
                bufferHead = (bufferHead + 1) % BufferCapacity;
                if (bufferCount < BufferCapacity) bufferCount++;
            }
        }

        static void ResolveSender(out string senderType, out string senderMethod)
        {
            senderType = null;
            senderMethod = null;
            var st = new StackTrace(2, false);
            int frames = st.FrameCount;
            for (int i = 0; i < frames; i++)
            {
                var method = st.GetFrame(i)?.GetMethod();
                var declaring = method?.DeclaringType;
                if (declaring == null) continue;
                if (declaring.Namespace == "jeanf.EventSystem") continue;
                senderType = declaring.FullName;
                senderMethod = method.Name;
                return;
            }
        }

        static string ResolveReceivers(ScriptableObject channel)
        {
            ScriptableObject canonical = channel;
            try { canonical = CanonicalChannelResolver.GetCanonical(channel) ?? channel; }
            catch { canonical = channel; }

            StringBuilder sb = null;
            int count = 0;
            var t = canonical.GetType();
            while (t != null && t != typeof(ScriptableObject))
            {
                foreach (var f in t.GetFields(DelegateFieldFlags))
                {
                    if (!typeof(Delegate).IsAssignableFrom(f.FieldType)) continue;
                    if (!(f.GetValue(canonical) is Delegate del)) continue;
                    foreach (var d in del.GetInvocationList())
                    {
                        var name = d.Method?.DeclaringType?.Name ?? d.Target?.GetType().Name ?? "static";
                        if (count >= 8)
                        {
                            if (sb != null) sb.Append(", …");
                            return sb?.ToString();
                        }
                        if (sb == null) sb = new StringBuilder();
                        else sb.Append(", ");
                        sb.Append(name).Append('.').Append(d.Method?.Name);
                        count++;
                    }
                }
                t = t.BaseType;
            }
            return count == 0 ? "(none)" : sb.ToString();
        }

        static string PayloadToString(object payload, bool mainThread)
        {
            if (payload == null) return "—";
            if (mainThread)
            {
                if (payload is UnityEngine.Object uo) return uo ? uo.name : "null";
                return payload.ToString();
            }
            if (payload is string s) return s;
            var t = payload.GetType();
            if (t.IsPrimitive || t.IsEnum || payload is decimal) return payload.ToString();
            return t.Name;
        }

        public static double Now => Clock.Elapsed.TotalSeconds;

        public static ChannelRecord[] SnapshotChannels()
        {
            lock (Gate)
            {
                var result = new ChannelRecord[Channels.Count];
                Channels.Values.CopyTo(result, 0);
                return result;
            }
        }

        public static int SnapshotRaises(RaiseRecord[] dst)
        {
            if (dst == null) return 0;
            lock (Gate)
            {
                int n = Math.Min(dst.Length, bufferCount);
                int start = (bufferHead - bufferCount + BufferCapacity) % BufferCapacity;
                int skip = bufferCount - n;
                start = (start + skip) % BufferCapacity;
                for (int i = 0; i < n; i++)
                    dst[i] = Buffer[(start + i) % BufferCapacity];
                return n;
            }
        }

        public static int RaiseCount
        {
            get { lock (Gate) { return bufferCount; } }
        }

        public static void Clear()
        {
            lock (Gate)
            {
                bufferHead = 0;
                bufferCount = 0;
                seqCounter = 0;
            }
        }

        public static void PruneChannel(int key)
        {
            lock (Gate)
            {
                Channels.Remove(key);
            }
        }

        public static string GroupKey(string guid, int key)
        {
            return string.IsNullOrEmpty(guid) ? "id:" + key : guid;
        }

        static IEventPublisher CurrentSender =>
            (_ambient != null && _ambient.Count > 0) ? _ambient.Peek() : null;

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void PushSender(IEventPublisher sender)
        {
            if (sender == null) return;
            (_ambient ?? (_ambient = new Stack<IEventPublisher>())).Push(sender);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void PopSender(IEventPublisher sender)
        {
            if (_ambient != null && _ambient.Count > 0) _ambient.Pop();
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void RegisterPublisher(IEventPublisher sender, ScriptableObject channel)
        {
            if (sender == null || channel == null) return;
            var comp = sender.Source;
            if (ReferenceEquals(comp, null)) return;

            string gk = GroupKey((channel as SerializableScriptableObject)?.Guid, RuntimeHelpers.GetHashCode(channel));
            int pk = RuntimeHelpers.GetHashCode(comp);
            string typeName = comp.GetType().Name;

            lock (Gate)
            {
                if (!Publishers.TryGetValue(gk, out var inner))
                {
                    inner = new Dictionary<int, PublisherRecord>();
                    Publishers[gk] = inner;
                }
                if (!inner.ContainsKey(pk))
                    inner[pk] = new PublisherRecord { typeName = typeName, component = new WeakReference<Component>(comp) };
            }
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void UnregisterPublisher(IEventPublisher sender, ScriptableObject channel)
        {
            if (sender == null || channel == null) return;
            var comp = sender.Source;
            if (ReferenceEquals(comp, null)) return;

            string gk = GroupKey((channel as SerializableScriptableObject)?.Guid, RuntimeHelpers.GetHashCode(channel));
            int pk = RuntimeHelpers.GetHashCode(comp);
            lock (Gate)
            {
                if (Publishers.TryGetValue(gk, out var inner)) inner.Remove(pk);
            }
        }

        public static PublisherRecord[] SnapshotPublishers(string groupKey)
        {
            lock (Gate)
            {
                if (!Publishers.TryGetValue(groupKey, out var inner) || inner.Count == 0)
                    return Array.Empty<PublisherRecord>();
                var result = new PublisherRecord[inner.Count];
                inner.Values.CopyTo(result, 0);
                return result;
            }
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void RegisterStackSender(string groupKey, string typeName, string method)
        {
            if (string.IsNullOrEmpty(typeName)) return;
            string sk = typeName + "|" + method;
            lock (Gate)
            {
                if (!StackSenders.TryGetValue(groupKey, out var inner))
                {
                    inner = new Dictionary<string, StackSenderRecord>();
                    StackSenders[groupKey] = inner;
                }
                if (!inner.TryGetValue(sk, out var rec))
                {
                    rec = new StackSenderRecord { typeName = typeName, method = method };
                    inner[sk] = rec;
                }
                rec.count++;
            }
        }

        public static StackSenderRecord[] SnapshotStackSenders(string groupKey)
        {
            lock (Gate)
            {
                if (!StackSenders.TryGetValue(groupKey, out var inner) || inner.Count == 0)
                    return Array.Empty<StackSenderRecord>();
                var result = new StackSenderRecord[inner.Count];
                inner.Values.CopyTo(result, 0);
                return result;
            }
        }
    }
}
