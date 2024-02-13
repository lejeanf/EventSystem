using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace jeanf.EventSystem
{
    public class InputActionEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<InputAction> OnEventRaised;

        public void RaiseEvent(InputAction value)
        {
            if (OnEventRaised != null)
                OnEventRaised?.Invoke(value);
        }
    }
}
