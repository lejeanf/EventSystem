using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Decimal,Float> Event Channel")]
    public class DecimalFloatEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<decimal, float> _onEventRaised;

        public event UnityAction<decimal, float> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(decimal nb, float value)
        {
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(nb, value);
        }
    }
}
