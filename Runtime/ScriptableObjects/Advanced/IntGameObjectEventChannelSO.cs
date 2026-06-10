using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Int,GameObject> Event Channel")]
    public class IntGameObjectEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<int, GameObject> _onEventRaised;

        public event UnityAction<int, GameObject> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(int nb, GameObject value)
        {
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(nb, value);
        }
    }
}
