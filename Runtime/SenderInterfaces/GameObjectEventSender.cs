using UnityEngine;

namespace jeanf.EventSystem
{
    public class GameObjectEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public GameObjectEventChannelSO GameObjectMessageChannel;
    
        public void SendGameObject(GameObject value)
        {
            GameObjectMessageChannel.RaiseEvent(value);
        }
    }
}