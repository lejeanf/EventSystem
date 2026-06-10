using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Void Event Channel")]
    public class VoidEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction _onEventRaised;

        public event UnityAction OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent()
        {
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke();
        }
    }

}
