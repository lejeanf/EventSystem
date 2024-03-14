using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<InputAction,Int> Event Channel")]

    public class InputActionIntEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<InputAction, int> OnEventRaised;

        public void RaiseEvent(InputAction action, int value)
        {
            OnEventRaised?.Invoke(action, value);
        }
    }
}

