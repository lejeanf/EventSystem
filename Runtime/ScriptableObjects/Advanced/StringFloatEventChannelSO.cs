using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<String,Float> Event Channel")]
    public class StringFloatEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<string, float> _onEventRaised;

        public event UnityAction<string, float> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(string str, float value)
        {
            EventDiagnostics.RecordRaise(this, (str, value));
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(str, value);
        }
    }
}
