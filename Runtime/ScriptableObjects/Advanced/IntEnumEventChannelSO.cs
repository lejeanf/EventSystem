using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Int,Enum> Event Channel")]
    public class IntEnumEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<int, Enum> _onEventRaised;

        public event UnityAction<int, Enum> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(int nb, Enum value)
        {
            EventDiagnostics.RecordRaise(this, (nb, value));
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(nb, value);
        }
    }
}
