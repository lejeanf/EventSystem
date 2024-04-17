using UnityEngine;

namespace jeanf.EventSystem
{
    public class DecimalGameObjectEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public DecimalGameObjectEventChannelSO gameObjectMessageChannel;
    
        public void SendIntBool(decimal nb, GameObject value)
        {
            gameObjectMessageChannel.RaiseEvent(nb, value);
        }
    }
}