using jeanf.EventSystem;
using UnityEngine;

namespace jeanf.EventSystem
{
    public class IntEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public IntEventChannelSO intMessageChannel;
        public Component Source => this;
    
        public void SendInt(int value)
        {
            this.Publish(() => intMessageChannel.RaiseEvent(value));
        }
    }
}