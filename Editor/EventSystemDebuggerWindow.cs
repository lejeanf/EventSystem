using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace jeanf.EventSystem.EditorTools
{
    public class EventSystemDebuggerWindow : EditorWindow
    {
        public const string PrefAutoSender = "jeanf.evtdbg.autoSender";
        public const string PrefAutoReceiver = "jeanf.evtdbg.autoReceiver";

        enum Tab { Channels, EventLog }

        Tab _tab = Tab.Channels;
        Vector2 _channelScroll;
        Vector2 _logScroll;
        string _channelSearch = "";
        string _logSearch = "";
        bool _duplicatesOnly;
        readonly HashSet<int> _expanded = new HashSet<int>();

        EventDiagnostics.RaiseRecord[] _logBuffer = new EventDiagnostics.RaiseRecord[EventDiagnostics.BufferCapacity];
        int _logCount;

        readonly Dictionary<string, List<Component>> _senderScan = new Dictionary<string, List<Component>>();
        double _nextScan;

        const BindingFlags FieldFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        [MenuItem("Window/jeanf/Event System Debugger")]
        static void Open()
        {
            var w = GetWindow<EventSystemDebuggerWindow>();
            w.titleContent = new GUIContent("Event Debugger");
            w.minSize = new Vector2(540, 320);
            w.Show();
        }

        void OnEnable()
        {
            EditorApplication.update += OnEditorUpdate;
        }

        void OnDisable()
        {
            EditorApplication.update -= OnEditorUpdate;
        }

        double _nextRepaint;

        void OnEditorUpdate()
        {
            if (!EditorApplication.isPlaying) return;
            if (EditorApplication.timeSinceStartup >= _nextScan)
            {
                _nextScan = EditorApplication.timeSinceStartup + 1.0;
                RebuildSenderScan();
            }
            if (EditorApplication.timeSinceStartup < _nextRepaint) return;
            _nextRepaint = EditorApplication.timeSinceStartup + 0.2;
            Repaint();
        }

        void RebuildSenderScan()
        {
            _senderScan.Clear();
            var comps = Object.FindObjectsOfType<MonoBehaviour>(true);
            foreach (var comp in comps)
            {
                if (comp == null) continue;
                foreach (var itf in comp.GetType().GetInterfaces())
                {
                    if (itf.Namespace != "jeanf.EventSystem") continue;
                    if (!itf.Name.EndsWith("EventSender", StringComparison.Ordinal)) continue;
                    foreach (var p in itf.GetProperties())
                    {
                        if (!typeof(DescriptionBaseSO).IsAssignableFrom(p.PropertyType)) continue;
                        if (!(p.GetValue(comp) is DescriptionBaseSO ch)) continue;
                        string gk = EventDiagnostics.GroupKey(ch.Guid, RuntimeHelpers.GetHashCode(ch));
                        if (!_senderScan.TryGetValue(gk, out var list))
                        {
                            list = new List<Component>();
                            _senderScan[gk] = list;
                        }
                        if (!list.Contains(comp)) list.Add(comp);
                    }
                }
            }
        }

        void OnGUI()
        {
            DrawToolbar();
            if (_tab == Tab.Channels) DrawChannels();
            else DrawEventLog();
        }

        void DrawToolbar()
        {
            using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                if (GUILayout.Toggle(_tab == Tab.Channels, "Channels", EditorStyles.toolbarButton)) _tab = Tab.Channels;
                if (GUILayout.Toggle(_tab == Tab.EventLog, "Event Log", EditorStyles.toolbarButton)) _tab = Tab.EventLog;
                GUILayout.FlexibleSpace();
                bool resolver = CanonicalChannelResolver.Resolve != null;
                var c = GUI.color;
                GUI.color = resolver ? new Color(0.5f, 1f, 0.5f) : new Color(1f, 0.6f, 0.4f);
                GUILayout.Label(resolver ? "● resolver installed" : "○ no resolver", EditorStyles.miniLabel);
                GUI.color = c;
            }

            if (!EditorApplication.isPlaying)
                EditorGUILayout.HelpBox("Enter Play mode to capture live channels and events.", MessageType.Info);
        }

        void DrawChannels()
        {
            using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                _channelSearch = GUILayout.TextField(_channelSearch, EditorStyles.toolbarSearchField, GUILayout.MinWidth(120));
                _duplicatesOnly = GUILayout.Toggle(_duplicatesOnly, "Duplicates only", EditorStyles.toolbarButton, GUILayout.Width(110));
            }

            var groups = BuildGroups();

            GUILayout.Label($"Live channels: {groups.Count}", EditorStyles.miniBoldLabel);

            _channelScroll = EditorGUILayout.BeginScrollView(_channelScroll);
            foreach (var g in groups)
            {
                if (!string.IsNullOrEmpty(_channelSearch) &&
                    g.displayName.IndexOf(_channelSearch, StringComparison.OrdinalIgnoreCase) < 0)
                    continue;
                if (_duplicatesOnly && g.instances.Count <= 1) continue;
                DrawGroup(g);
            }
            EditorGUILayout.EndScrollView();
        }

        class Group
        {
            public string groupKey;
            public string displayName;
            public string typeName;
            public List<(EventDiagnostics.ChannelRecord rec, ScriptableObject so)> instances =
                new List<(EventDiagnostics.ChannelRecord, ScriptableObject)>();
        }

        List<Group> BuildGroups()
        {
            var byKey = new Dictionary<string, Group>();
            var records = EventDiagnostics.SnapshotChannels();
            foreach (var rec in records)
            {
                ScriptableObject so = null;
                rec.instance?.TryGetTarget(out so);
                if (so == null)
                {
                    EventDiagnostics.PruneChannel(rec.key);
                    continue;
                }

                string gk = EventDiagnostics.GroupKey(rec.guid, rec.key);
                if (!byKey.TryGetValue(gk, out var g))
                {
                    g = new Group { groupKey = gk, displayName = so.name, typeName = rec.typeName };
                    byKey[gk] = g;
                }
                g.instances.Add((rec, so));
            }

            var list = new List<Group>(byKey.Values);
            list.Sort((a, b) => string.Compare(a.displayName, b.displayName, StringComparison.OrdinalIgnoreCase));
            return list;
        }

        void DrawGroup(Group g)
        {
            bool dup = g.instances.Count > 1;
            int subTotal = 0;
            foreach (var inst in g.instances) subTotal += CountSubscribers(inst.so);

            int gkHash = g.groupKey.GetHashCode();
            bool open = _expanded.Contains(gkHash);

            using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                bool nowOpen = EditorGUILayout.Foldout(open, GUIContent.none, true, EditorStyles.foldout);
                if (nowOpen != open)
                {
                    if (nowOpen) _expanded.Add(gkHash); else _expanded.Remove(gkHash);
                }

                GUILayout.Label(g.displayName, EditorStyles.boldLabel, GUILayout.Width(200));
                GUILayout.Label(g.typeName, EditorStyles.miniLabel, GUILayout.Width(150));
                GUILayout.Label($"inst {g.instances.Count}", EditorStyles.miniLabel, GUILayout.Width(54));
                GUILayout.Label($"subs {subTotal}", EditorStyles.miniLabel, GUILayout.Width(54));

                GUILayout.FlexibleSpace();
                if (dup)
                {
                    var c = GUI.color; GUI.color = new Color(1f, 0.55f, 0.4f);
                    GUILayout.Label("▲ DUPLICATE", EditorStyles.miniBoldLabel, GUILayout.Width(90));
                    GUI.color = c;
                }
                else if (subTotal == 0)
                {
                    GUILayout.Label("◌ no listeners", EditorStyles.miniLabel, GUILayout.Width(90));
                }

                if (GUILayout.Button("Select GOs", EditorStyles.miniButton, GUILayout.Width(80)))
                    SelectAllListeners(g);
            }

            if (open) DrawGroupDetails(g);
        }

        void DrawGroupDetails(Group g)
        {
            EditorGUI.indentLevel++;
            ScriptableObject canonical = null;
            try { canonical = CanonicalChannelResolver.GetCanonical((ScriptableObject)g.instances[0].so); }
            catch { }

            foreach (var inst in g.instances)
            {
                var so = inst.so;
                var rec = inst.rec;
                bool isCanonical = canonical != null && ReferenceEquals(canonical, so);

                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Space(16);
                    GUILayout.Label(isCanonical ? "[CANONICAL]" : "[duplicate]",
                        isCanonical ? EditorStyles.miniBoldLabel : EditorStyles.miniLabel, GUILayout.Width(86));
                    GUILayout.Label($"id {so.GetInstanceID()}", EditorStyles.miniLabel, GUILayout.Width(96));
                    GUILayout.Label($"thread {rec.firstSeenThreadId}" + (rec.seenOffMainThread ? " ⚠" : ""),
                        EditorStyles.miniLabel, GUILayout.Width(90));
                    if (GUILayout.Button("Ping asset", EditorStyles.miniButton, GUILayout.Width(74)))
                        EditorGUIUtility.PingObject(so);
                }

                var subs = GetSubscribers(so);
                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Space(22);
                    RoleBadge("RECV", RecvColor);
                    GUILayout.Label($"receivers ({subs.Count})", EditorStyles.miniBoldLabel);
                }
                foreach (var s in subs)
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        GUILayout.Space(40);
                        GUILayout.Label("↳ " + s.label, EditorStyles.miniLabel, GUILayout.Width(320));
                        GUILayout.FlexibleSpace();
                        using (new EditorGUI.DisabledScope(s.target == null))
                        {
                            if (GUILayout.Button("Select", EditorStyles.miniButton, GUILayout.Width(60)))
                            {
                                Selection.activeObject = s.SelectionTarget;
                                EditorGUIUtility.PingObject(s.SelectionTarget);
                            }
                            if (GUILayout.Button("Ping", EditorStyles.miniButton, GUILayout.Width(50)))
                                EditorGUIUtility.PingObject(s.SelectionTarget);
                        }
                    }
                }
            }

            var publishers = GetPublishers(g);
            var stackSenders = EventDiagnostics.SnapshotStackSenders(g.groupKey);
            int senderTotal = publishers.Count + stackSenders.Length;
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(16);
                RoleBadge("SEND", SendColor);
                GUILayout.Label($"senders ({senderTotal})", EditorStyles.miniBoldLabel);
                if (senderTotal == 0)
                    GUILayout.Label("— none yet (enable “Track sender” to auto-detect by class, or this.Publish(…) for exact GameObject)", EditorStyles.miniLabel);
            }
            foreach (var ss in stackSenders)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Space(40);
                    string m = string.IsNullOrEmpty(ss.method) ? "" : "." + ss.method + "()";
                    var prev = GUI.color;
                    GUI.color = new Color(prev.r, prev.g, prev.b, 0.75f);
                    GUILayout.Label($"⇧ {ss.typeName}{m}   ×{ss.count}   (by stack, no GameObject)", EditorStyles.miniLabel, GUILayout.Width(420));
                    GUI.color = prev;
                    GUILayout.FlexibleSpace();
                }
            }
            foreach (var p in publishers)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Space(40);
                    string name = p.comp != null ? p.comp.GetType().Name + "  ▸ \"" + p.comp.gameObject.name + "\"" : p.label;
                    GUILayout.Label("⇧ " + name, EditorStyles.miniLabel, GUILayout.Width(320));
                    GUILayout.FlexibleSpace();
                    using (new EditorGUI.DisabledScope(p.comp == null))
                    {
                        if (GUILayout.Button("Select", EditorStyles.miniButton, GUILayout.Width(60)))
                        {
                            Selection.activeObject = p.comp != null ? p.comp.gameObject : null;
                            if (p.comp != null) EditorGUIUtility.PingObject(p.comp.gameObject);
                        }
                        if (GUILayout.Button("Ping", EditorStyles.miniButton, GUILayout.Width(50)))
                            if (p.comp != null) EditorGUIUtility.PingObject(p.comp.gameObject);
                    }
                }
            }

            EditorGUI.indentLevel--;
            GUILayout.Space(4);
        }

        static readonly Color RecvColor = new Color(0.45f, 0.8f, 1f);
        static readonly Color SendColor = new Color(1f, 0.72f, 0.4f);

        static void RoleBadge(string text, Color color)
        {
            var prev = GUI.color;
            GUI.color = color;
            GUILayout.Label(text, EditorStyles.miniBoldLabel, GUILayout.Width(38));
            GUI.color = prev;
        }

        struct PubInfo
        {
            public string label;
            public Component comp;
        }

        List<PubInfo> GetPublishers(Group g)
        {
            var list = new List<PubInfo>();
            var seen = new HashSet<int>();

            foreach (var pr in EventDiagnostics.SnapshotPublishers(g.groupKey))
            {
                Component c = null;
                pr.component?.TryGetTarget(out c);
                if (c == null) continue;
                if (!seen.Add(c.GetInstanceID())) continue;
                list.Add(new PubInfo { comp = c, label = pr.typeName });
            }

            if (_senderScan.TryGetValue(g.groupKey, out var scanned))
            {
                foreach (var c in scanned)
                {
                    if (c == null) continue;
                    if (!seen.Add(c.GetInstanceID())) continue;
                    list.Add(new PubInfo { comp = c, label = c.GetType().Name });
                }
            }
            return list;
        }

        struct SubInfo
        {
            public string label;
            public Object target;
            public GameObject go;
            public Object SelectionTarget => go != null ? go : target;
        }

        int CountSubscribers(ScriptableObject so)
        {
            int n = 0;
            ForEachSubscriber(so, (_, __) => n++);
            return n;
        }

        List<SubInfo> GetSubscribers(ScriptableObject so)
        {
            var list = new List<SubInfo>();
            ForEachSubscriber(so, (target, method) =>
            {
                var go = (target as Component)?.gameObject;
                string typeName = method?.DeclaringType?.Name ?? target?.GetType().Name ?? "static";
                string goName = go != null ? "  ▸ \"" + go.name + "\"" : "";
                list.Add(new SubInfo
                {
                    label = $"{typeName}.{method?.Name}(){goName}",
                    target = target as Object,
                    go = go,
                });
            });
            return list;
        }

        static void ForEachSubscriber(ScriptableObject so, Action<object, MethodInfo> action)
        {
            if (so == null) return;
            var t = so.GetType();
            while (t != null && t != typeof(ScriptableObject))
            {
                foreach (var f in t.GetFields(FieldFlags))
                {
                    if (!typeof(Delegate).IsAssignableFrom(f.FieldType)) continue;
                    if (!(f.GetValue(so) is Delegate del)) continue;
                    foreach (var d in del.GetInvocationList())
                        action(d.Target, d.Method);
                }
                t = t.BaseType;
            }
        }

        void SelectAllListeners(Group g)
        {
            var objs = new List<Object>();
            foreach (var inst in g.instances)
                foreach (var s in GetSubscribers(inst.so))
                    if (s.SelectionTarget != null) objs.Add(s.SelectionTarget);
            if (objs.Count > 0)
            {
                Selection.objects = objs.ToArray();
                EditorGUIUtility.PingObject(objs[0]);
            }
        }

        void DrawEventLog()
        {
            using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label("On play:", EditorStyles.miniLabel, GUILayout.Width(54));
                DrawPrefToggle(PrefAutoSender, "Track sender", v =>
                {
                    EventDiagnostics.CaptureSenderStack = v;
                    EventDiagnostics.TrackingEnabled = v || EventDiagnostics.CaptureReceivers;
                });
                DrawPrefToggle(PrefAutoReceiver, "Track receiver", v =>
                {
                    EventDiagnostics.CaptureReceivers = v;
                    EventDiagnostics.TrackingEnabled = v || EventDiagnostics.CaptureSenderStack;
                });
                if (GUILayout.Button("Clear", EditorStyles.toolbarButton, GUILayout.Width(50)))
                    EventDiagnostics.Clear();
                GUILayout.FlexibleSpace();
                _logSearch = GUILayout.TextField(_logSearch, EditorStyles.toolbarSearchField, GUILayout.MinWidth(120));
                GUILayout.Label($"{EventDiagnostics.RaiseCount}/{EventDiagnostics.BufferCapacity}", EditorStyles.miniLabel);
            }

            _logCount = EventDiagnostics.SnapshotRaises(_logBuffer);

            DrawCounts();

            using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("Seq", EditorStyles.miniBoldLabel, GUILayout.Width(54));
                GUILayout.Label("Time", EditorStyles.miniBoldLabel, GUILayout.Width(64));
                GUILayout.Label("Channel", EditorStyles.miniBoldLabel, GUILayout.Width(180));
                GUILayout.Label("Payload", EditorStyles.miniBoldLabel, GUILayout.Width(110));
                GUILayout.Label("Sender", EditorStyles.miniBoldLabel, GUILayout.Width(180));
                GUILayout.Label("Receiver(s)", EditorStyles.miniBoldLabel);
            }

            _logScroll = EditorGUILayout.BeginScrollView(_logScroll);
            for (int i = _logCount - 1; i >= 0; i--)
            {
                var r = _logBuffer[i];
                if (!string.IsNullOrEmpty(_logSearch) &&
                    (r.typeName?.IndexOf(_logSearch, StringComparison.OrdinalIgnoreCase) ?? -1) < 0 &&
                    (r.payload?.IndexOf(_logSearch, StringComparison.OrdinalIgnoreCase) ?? -1) < 0)
                    continue;

                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Label("#" + r.seq, EditorStyles.miniLabel, GUILayout.Width(54));
                    GUILayout.Label(r.time.ToString("0.000"), EditorStyles.miniLabel, GUILayout.Width(64));
                    GUILayout.Label(r.typeName, EditorStyles.miniLabel, GUILayout.Width(180));
                    GUILayout.Label(r.payload, EditorStyles.miniLabel, GUILayout.Width(110));
                    string sender = r.senderObject != null
                        ? r.senderType
                        : r.senderType != null ? r.senderType + "." + r.senderMethod + "()" : "—";
                    if (r.offMainThread) sender = "⚠ t" + r.threadId + "  " + sender;
                    GUILayout.Label(sender, EditorStyles.miniLabel, GUILayout.Width(146));
                    using (new EditorGUI.DisabledScope(r.senderObject == null))
                    {
                        if (GUILayout.Button("Sel", EditorStyles.miniButton, GUILayout.Width(32)) && r.senderObject != null)
                        {
                            Selection.activeObject = r.senderObject;
                            EditorGUIUtility.PingObject(r.senderObject);
                        }
                    }
                    GUILayout.Label(string.IsNullOrEmpty(r.receivers) ? "—" : r.receivers, EditorStyles.miniLabel);
                }
            }
            EditorGUILayout.EndScrollView();
        }

        static void DrawPrefToggle(string key, string label, Action<bool> applyLive)
        {
            bool cur = EditorPrefs.GetBool(key, false);
            bool next = GUILayout.Toggle(cur, label, EditorStyles.toolbarButton, GUILayout.Width(100));
            if (next != cur)
            {
                EditorPrefs.SetBool(key, next);
                if (EditorApplication.isPlaying) applyLive?.Invoke(next);
            }
        }

        void DrawCounts()
        {
            if (_logCount == 0) return;
            var counts = new Dictionary<string, int>();
            int max = 1;
            for (int i = 0; i < _logCount; i++)
            {
                var name = _logBuffer[i].typeName ?? "?";
                counts.TryGetValue(name, out int n);
                n++;
                counts[name] = n;
                if (n > max) max = n;
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                foreach (var kv in counts)
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        GUILayout.Label(kv.Key, EditorStyles.miniLabel, GUILayout.Width(180));
                        var rect = GUILayoutUtility.GetRect(120, 10);
                        EditorGUI.DrawRect(new Rect(rect.x, rect.y + 1, 120f * kv.Value / max, 8),
                            new Color(0.4f, 0.7f, 1f));
                        GUILayout.Label(kv.Value.ToString(), EditorStyles.miniLabel, GUILayout.Width(50));
                    }
                }
            }
        }
    }

    [InitializeOnLoad]
    static class EventDiagnosticsAutoStart
    {
        static EventDiagnosticsAutoStart()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state != PlayModeStateChange.EnteredPlayMode) return;

            bool sender = EditorPrefs.GetBool(EventSystemDebuggerWindow.PrefAutoSender, false);
            bool receiver = EditorPrefs.GetBool(EventSystemDebuggerWindow.PrefAutoReceiver, false);

            EventDiagnostics.CaptureSenderStack = sender;
            EventDiagnostics.CaptureReceivers = receiver;
            EventDiagnostics.TrackingEnabled = sender || receiver;
        }
    }
}
