using jeanf.EventSystem;
using UnityEngine;

namespace jeanf.EventSystem
{
    public class FloatEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public FloatEventChannelSO floatMessageChannel;
    
        public void SendFloat(float value)
        {
            floatMessageChannel.RaiseEvent(value);
        }
    }
}