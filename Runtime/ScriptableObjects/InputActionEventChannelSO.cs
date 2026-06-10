using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace jeanf.EventSystem
{
    public class InputActionEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<InputAction> _onEventRaised;

        public event UnityAction<InputAction> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(InputAction value)
        {
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(value);
        }
    }
}
