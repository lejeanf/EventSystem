using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [System.Serializable]
    public class StringEvent : UnityEvent<string>
    {

    }

    public class StringEventListener : MonoBehaviour
    {
        [SerializeField] private StringEventChannelSO _channel = default;

        public StringEvent OnEventRaised;

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

        private void Respond(string value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}