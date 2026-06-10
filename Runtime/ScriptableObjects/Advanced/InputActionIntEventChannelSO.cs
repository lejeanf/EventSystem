using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<InputAction,Int> Event Channel")]
    public class InputActionIntEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<InputAction, int> _onEventRaised;

        public event UnityAction<InputAction, int> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(InputAction action, int value)
        {
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(action, value);
        }
    }
}
