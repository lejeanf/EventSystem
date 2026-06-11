using UnityEngine;

namespace jeanf.EventSystem
{
    public class StringFloatEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public StringFloatEventChannelSO stringStringMessageChannel;
        public Component Source => this;
    
        public void SendIntInt(string str, float value)
        {
            this.Publish(() => stringStringMessageChannel.RaiseEvent(str, value));
        }
    }
}

