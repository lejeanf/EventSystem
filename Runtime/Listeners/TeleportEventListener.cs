using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [System.Serializable]
    public class TeleportEvent : UnityEvent<TeleportInformation>
    {
        
    }
    public class TeleportEventListener : MonoBehaviour
    {
        [Header("Receiving on channel:")]
        [SerializeField] private TeleportEventChannelSO _channel = default;

        public UnityEvent<TeleportInformation> OnEventRaised;

        private void OnEnable()
        {
            if (_channel != null) _channel.OnEventRaised += Respond;
        }

        private void OnDisable()
        {
            if (_channel != null) _channel.OnEventRaised -= Respond;
        }

        private void Respond( TeleportInformation teleportInformation)
        {
            OnEventRaised?.Invoke(teleportInformation);
        }

    }
}