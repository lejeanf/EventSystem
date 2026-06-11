using jeanf.EventSystem;
using UnityEngine;

namespace jeanf.EventSystem
{
    public class FloatEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public FloatEventChannelSO floatMessageChannel;
        public Component Source => this;
    
        public void SendFloat(float value)
        {
            this.Publish(() => floatMessageChannel.RaiseEvent(value));
        }
    }
}