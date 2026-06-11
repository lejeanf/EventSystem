using UnityEngine;

namespace jeanf.EventSystem
{
    public class IntIntEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public IntIntEventChannelSO intintMessageChannel;
        public Component Source => this;
    
        public void SendIntInt(int nb, int value)
        {
            this.Publish(() => intintMessageChannel.RaiseEvent(nb, value));
        }
    }
}

