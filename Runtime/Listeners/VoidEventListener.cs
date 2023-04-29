using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [System.Serializable]
    public class VoidEvent : UnityEvent<object>
    {
        
    }
    public class VoidEventListener : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;
        
        [SerializeField] private VoidEventChannelSO _channel = default;

        public UnityEvent OnEventRaised;

        private void OnEnable()
        {
            if (_channel != null)
                _channel.OnEventRaised += Respond;
            if(_isDebug) Debug.Log($"received event on channel {_channel.name}.");
        }

        private void OnDisable()
        {
            if (_channel != null)
                _channel.OnEventRaised -= Respond;
        }

        private void Respond()
        {
            OnEventRaised?.Invoke();
        }

    }
}