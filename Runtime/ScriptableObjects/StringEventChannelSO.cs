using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have one int argument.
/// Example: An Achievement unlock event, where the int is the Achievement ID.
/// </summary>
namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/String Event Channel")]
    public class StringEventChannelSO : DescriptionBaseSO
    {
        // OnEventRaised is exposed as an event so add/remove can be redirected onto the
        // canonical instance held by CanonicalChannelResolver. The backing delegate lives only
        // on the canonical; duplicate instances are inert proxies that forward all operations.
        [NonSerialized] private UnityAction<string> _onEventRaised;

        public event UnityAction<string> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(string value)
        {
            EventDiagnostics.RecordRaise(this, value);
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(value);
        }
    }
}
