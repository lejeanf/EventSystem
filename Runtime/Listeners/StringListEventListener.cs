using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [System.Serializable]
    public class StringListEvent: UnityEvent<List<string>>
    {

    }

    public class StringListEventListener : MonoBehaviour
    {
        public StringListEventChannelSO _channel = default;

        public StringListEvent OnStringListEventRaised;

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

        private void Respond(List<string> list)
        {
            OnStringListEventRaised?.Invoke(list);
        }

        public StringListEventListener(StringListEventChannelSO _channel, StringListEvent OnStringListEventRaised)
        {
            this._channel = _channel;
            this.OnStringListEventRaised = OnStringListEventRaised;
        }
    }
}
