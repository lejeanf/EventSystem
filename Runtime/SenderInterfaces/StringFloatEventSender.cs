using UnityEngine;

namespace jeanf.EventSystem
{
    public class StringFloatEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public StringFloatEventChannelSO stringStringMessageChannel;
    
        public void SendIntInt(string str, float value)
        {
            stringStringMessageChannel.RaiseEvent(str, value);
        }
    }
}

