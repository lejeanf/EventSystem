using System;
using UnityEngine;

namespace jeanf.EventSystem
{
    public class IntEnumEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }

        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public IntEnumEventChannelSO intEnumMessageChannel;
        public Component Source => this;
    
        public void SendIntEnum(int nb, Enum value)
        {
            this.Publish(() => intEnumMessageChannel.RaiseEvent(nb, value));
        }
    }
}

