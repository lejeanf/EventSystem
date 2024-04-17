using UnityEngine;

namespace jeanf.EventSystem
{
    public class DecimalIntEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public DecimalIntEventChannelSO _channel;
    
        public void SendDecimalInt(int nb, int value)
        {
            _channel.RaiseEvent(nb, value);
        }
    }
}

