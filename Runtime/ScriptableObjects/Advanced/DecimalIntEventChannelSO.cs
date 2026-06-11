using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Decimal,Int> Event Channel")]
    public class DecimalIntEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<decimal, int> _onEventRaised;

        public event UnityAction<decimal, int> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(decimal nb, int value)
        {
            EventDiagnostics.RecordRaise(this, (nb, value));
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(nb, value);
        }
    }
}
