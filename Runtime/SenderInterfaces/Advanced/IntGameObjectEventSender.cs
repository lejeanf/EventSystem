using UnityEngine;

namespace jeanf.EventSystem
{
    public class IntGameObjectEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public IntGameObjectEventChannelSO gameObjectMessageChannel;
    
        public void SendIntBool(int nb, GameObject value)
        {
            gameObjectMessageChannel.RaiseEvent(nb, value);
        }
    }
}