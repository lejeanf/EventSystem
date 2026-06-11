using UnityEngine;

namespace jeanf.EventSystem
{
    public class DecimalIntEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public DecimalIntEventChannelSO _channel;
        public Component Source => this;
    
        public void SendDecimalInt(int nb, int value)
        {
            this.Publish(() => _channel.RaiseEvent(nb, value));
        }
    }
}

