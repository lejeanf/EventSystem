using UnityEngine;

namespace jeanf.EventSystem
{
    public class StringEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public StringEventChannelSO stringMessageChannel;
    
        public void SendString(string value)
        {
            stringMessageChannel.RaiseEvent(value);
        }
    }
}

