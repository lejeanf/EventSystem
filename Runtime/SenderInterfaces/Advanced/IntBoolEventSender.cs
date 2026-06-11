using UnityEngine;

namespace jeanf.EventSystem
{
    public class IntBoolEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public IntBoolEventChannelSO boolMessageChannel;
        public Component Source => this;
    
        public void SendIntBool(int nb, bool value)
        {
            this.Publish(() => boolMessageChannel.RaiseEvent(nb, value));
        }
    }
}

