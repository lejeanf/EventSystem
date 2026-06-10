using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Decimal,Decimal> Event Channel")]
    public class DecimalDecimalEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<decimal, decimal> _onEventRaised;

        public event UnityAction<decimal, decimal> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(decimal nb, decimal value)
        {
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(nb, value);
        }
    }
}
