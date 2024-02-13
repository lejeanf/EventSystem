using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [System.Serializable]
    public class InputActionEvent : UnityEvent<InputAction>
    {

    }
    public class InputActionEventListener : MonoBehaviour
    {
        public InputActionEventChannelSO _channel = default;

        public InputActionEvent OnInputActionEventRaised;

        private void OnEnable()
        {
            if (_channel != null)
                _channel.OnEventRaised += Respond;
        }

        private void OnDisable()
        {
            if (_channel != null)
                _channel.OnEventRaised -= Respond;
        }

        private void Respond(InputAction value)
        {
            OnInputActionEventRaised?.Invoke(value);
        }

        public InputActionEventListener(InputActionEventChannelSO _channel, InputActionEvent OnInputActionEventRaised)
        {
            this._channel = _channel;
            this.OnInputActionEventRaised = OnInputActionEventRaised;
        }
    }
}

