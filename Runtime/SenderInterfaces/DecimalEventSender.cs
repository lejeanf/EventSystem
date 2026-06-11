using jeanf.EventSystem;
using UnityEngine;

namespace jeanf.EventSystem
{
    public class DecimalEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public DecimalEventChannelSO floatMessageChannel;
        public Component Source => this;
    
        public void SendFloat(decimal value)
        {
            this.Publish(() => floatMessageChannel.RaiseEvent(value));
        }
    }
}