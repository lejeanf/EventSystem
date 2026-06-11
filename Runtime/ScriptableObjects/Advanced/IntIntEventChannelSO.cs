using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Int,Int> Event Channel")]
    public class IntIntEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<int, int> _onEventRaised;

        public event UnityAction<int, int> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(int nb, int value)
        {
            EventDiagnostics.RecordRaise(this, (nb, value));
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(nb, value);
        }
    }
}
