using UnityEngine;

namespace jeanf.EventSystem
{
    public class BoolEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public BoolEventChannelSO boolMessageChannel;
        public Component Source => this;
    
        public void SendBool(bool value)
        {
            this.Publish(() => boolMessageChannel.RaiseEvent(value));
        }
    }
}

