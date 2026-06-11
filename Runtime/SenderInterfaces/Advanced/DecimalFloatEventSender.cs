using UnityEngine;
using UnityEngine.Serialization;

namespace jeanf.EventSystem
{
    public class DecimalFloatEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public DecimalFloatEventChannelSO channel;
        public Component Source => this;
    
        public void SendIntFloat(int nb, float value)
        {
            this.Publish(() => channel.RaiseEvent(nb, value));
        }
    }
}

