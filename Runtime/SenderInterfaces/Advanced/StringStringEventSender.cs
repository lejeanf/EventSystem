using UnityEngine;

namespace jeanf.EventSystem
{
    public class StringStringEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public StringStringEventChannelSO stringStringMessageChannel;
    
        public void SendIntInt(string str1, string str2)
        {
            stringStringMessageChannel.RaiseEvent(str1, str2);
        }
    }
}

