using UnityEngine;

namespace jeanf.EventSystem
{
    public class DecimalStringEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public DecimalStringEventChannelSO _channel;
        public Component Source => this;
    
        public void SendIntString(decimal nb, string value)
        {
            this.Publish(() => _channel.RaiseEvent(nb, value));
        }
    }
}

