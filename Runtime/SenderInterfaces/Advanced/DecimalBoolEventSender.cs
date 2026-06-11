using UnityEngine;

namespace jeanf.EventSystem
{
    public class DecimalBoolEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public DecimalBoolEventChannelSO boolMessageChannel;
        public Component Source => this;
    
        public void SendIntBool(decimal nb, bool value)
        {
            this.Publish(() => boolMessageChannel.RaiseEvent(nb, value));
        }
    }
}

