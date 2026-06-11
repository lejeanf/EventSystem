using UnityEngine;

namespace jeanf.EventSystem
{
    public class DecimalDecimalEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public DecimalDecimalEventChannelSO _channel;
        public Component Source => this;
    
        public void SendIntInt(decimal nb, decimal value)
        {
            this.Publish(() => _channel.RaiseEvent(nb, value));
        }
    }
}

