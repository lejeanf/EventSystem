using UnityEngine;
using UnityEngine.Serialization;

namespace jeanf.EventSystem
{
    public class DecimalFloatEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public DecimalFloatEventChannelSO channel;
    
        public void SendIntFloat(int nb, float value)
        {
            channel.RaiseEvent(nb, value);
        }
    }
}

