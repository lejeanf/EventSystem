using UnityEngine;

namespace jeanf.EventSystem
{
    public class StringEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public StringEventChannelSO stringMessageChannel;
        public Component Source => this;
    
        public void SendString(string value)
        {
            this.Publish(() => stringMessageChannel.RaiseEvent(value));
        }
    }
}

