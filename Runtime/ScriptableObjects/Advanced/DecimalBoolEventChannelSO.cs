using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Decimal,Bool> Event Channel")]
    public class DecimalBoolEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<decimal, bool> _onEventRaised;

        public event UnityAction<decimal, bool> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(decimal nb, bool value)
        {
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(nb, value);
        }
    }
}
