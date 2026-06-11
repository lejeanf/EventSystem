using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jeanf.EventSystem
{
    public class StringListEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        {
            get => _isDebug;
            set => _isDebug = value;
        }
        [SerializeField] private bool _isDebug = false;
        [field: Header("Broadcasting on:")] public StringListEventChannelSO stringListEventChannel;
        public Component Source => this;

        public void SendStringList(List<string> list)
        {
            this.Publish(() => stringListEventChannel.RaiseEvent(list));
        }

    }
}
