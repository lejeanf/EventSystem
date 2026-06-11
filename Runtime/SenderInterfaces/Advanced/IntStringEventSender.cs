using UnityEngine;

namespace jeanf.EventSystem
{
    public class IntStringEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public IntStringEventChannelSO intintMessageChannel;
        public Component Source => this;
    
        public void SendIntString(int nb, string value)
        {
            this.Publish(() => intintMessageChannel.RaiseEvent(nb, value));
        }
    }
}

