using UnityEngine;

namespace jeanf.EventSystem
{
    public class IntGameObjectEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public IntGameObjectEventChannelSO gameObjectMessageChannel;
        public Component Source => this;
    
        public void SendIntBool(int nb, GameObject value)
        {
            this.Publish(() => gameObjectMessageChannel.RaiseEvent(nb, value));
        }
    }
}