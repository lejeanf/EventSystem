using UnityEngine;

namespace jeanf.EventSystem
{
    public class DecimalDecimalEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public DecimalDecimalEventChannelSO _channel;
    
        public void SendIntInt(decimal nb, decimal value)
        {
            _channel.RaiseEvent(nb, value);
        }
    }
}

