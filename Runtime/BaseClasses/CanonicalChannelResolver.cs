using System;
using UnityEngine;

namespace jeanf.EventSystem
{
    /// <summary>
    /// Decoupling hook used by every <see cref="DescriptionBaseSO"/> derived event channel to
    /// redirect subscribe / raise calls to a canonical runtime instance.
    ///
    /// In an Addressables build, the same logical channel asset is often deserialized into
    /// multiple distinct runtime ScriptableObjects (one per bundle that referenced it). Without
    /// this resolver, a publisher in scene A raises on its duplicate while a subscriber in
    /// scene B is wired to a different duplicate, so the event is silently dropped.
    ///
    /// External code (the uvs.AddressableSystem registry) installs a delegate into
    /// <see cref="Resolve"/> at <c>RuntimeInitializeLoadType.BeforeSceneLoad</c>. Every channel
    /// then asks the resolver, by reference, for the canonical instance and forwards all
    /// subscribe / raise operations there. If no resolver is registered (editor playmode with
    /// no duplication, package used standalone, etc.) every call falls back to <c>this</c>, so
    /// the channels behave identically to the original implementation.
    ///
    /// The contract for an installed <see cref="Resolve"/>:
    ///   - Given a duplicate instance, return the canonical instance (must be assignment-
    ///     compatible with the input type).
    ///   - Given the canonical instance itself, return it (or <c>null</c>; the wrapper falls
    ///     back to <c>self</c>).
    ///   - Must be thread-safe with respect to assignment; channels read <see cref="Resolve"/>
    ///     once per call into a local.
    ///   - Must not allocate or throw on the hot path.
    /// </summary>
    public static class CanonicalChannelResolver
    {
        public static Func<ScriptableObject, ScriptableObject> Resolve;

        public static T GetCanonical<T>(T self) where T : ScriptableObject
        {
            if (self == null)
                return null;

            EventDiagnostics.NoteSeen(self);

            var resolver = Resolve;
            if (resolver == null)
                return self;

            ScriptableObject canonical;
            try
            {
                canonical = resolver(self);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return self;
            }

            return (canonical as T) ?? self;
        }
    }
}
