using UnityEngine;

namespace jeanf.EventSystem
{
    public class DecimalStringEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public DecimalStringEventChannelSO _channel;
    
        public void SendIntString(decimal nb, string value)
        {
            _channel.RaiseEvent(nb, value);
        }
    }
}

