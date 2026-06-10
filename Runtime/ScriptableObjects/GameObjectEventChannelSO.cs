using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/GameObject Event Channel")]
    public class GameObjectEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<GameObject> _onEventRaised;

        public event UnityAction<GameObject> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(GameObject value)
        {
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(value);
        }
    }
}
