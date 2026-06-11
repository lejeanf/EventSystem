using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Int,Bool> Event Channel")]
    public class IntBoolEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<int, bool> _onEventRaised;

        public event UnityAction<int, bool> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(int nb, bool value)
        {
            EventDiagnostics.RecordRaise(this, (nb, value));
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(nb, value);
        }
    }
}
