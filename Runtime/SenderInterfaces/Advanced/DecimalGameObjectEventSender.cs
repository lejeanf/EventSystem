using UnityEngine;

namespace jeanf.EventSystem
{
    public class DecimalGameObjectEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public DecimalGameObjectEventChannelSO gameObjectMessageChannel;
        public Component Source => this;
    
        public void SendIntBool(decimal nb, GameObject value)
        {
            this.Publish(() => gameObjectMessageChannel.RaiseEvent(nb, value));
        }
    }
}