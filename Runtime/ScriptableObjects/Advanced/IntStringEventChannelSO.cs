using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Int,String> Event Channel")]
    public class IntStringEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<int, string> _onEventRaised;

        public event UnityAction<int, string> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(int nb, string value)
        {
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(nb, value);
        }
    }
}
