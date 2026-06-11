using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Decimal,GameObject> Event Channel")]
    public class DecimalGameObjectEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<decimal, GameObject> _onEventRaised;

        public event UnityAction<decimal, GameObject> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(decimal nb, GameObject value)
        {
            EventDiagnostics.RecordRaise(this, (nb, value));
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(nb, value);
        }
    }
}
