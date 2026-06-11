using UnityEngine;

namespace jeanf.EventSystem
{
    public class GameObjectEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public GameObjectEventChannelSO GameObjectMessageChannel;
        public Component Source => this;
    
        public void SendGameObject(GameObject value)
        {
            this.Publish(() => GameObjectMessageChannel.RaiseEvent(value));
        }
    }
}