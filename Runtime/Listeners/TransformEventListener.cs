using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [System.Serializable]
    public class TransformEvent : UnityEvent<Transform>
    {
        
    }
    public class TransformEventListener : MonoBehaviour
    {
        [Header("Receiving on channel:")]
        [SerializeField] private TransformEventChannelSO _channel = default;

        public UnityEvent<Transform> OnEventRaised;

        private void OnEnable()
        {
            if (_channel != null) _channel.OnEventRaised += Respond;
        }

        private void OnDisable()
        {
            if (_channel != null) _channel.OnEventRaised -= Respond;
        }

        private void Respond( Transform transform)
        {
            OnEventRaised?.Invoke(transform);
        }

    }
}