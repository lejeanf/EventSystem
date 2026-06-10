using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Decimal,String> Event Channel")]
    public class DecimalStringEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<decimal, string> _onEventRaised;

        public event UnityAction<decimal, string> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(decimal nb, string value)
        {
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(nb, value);
        }
    }
}
