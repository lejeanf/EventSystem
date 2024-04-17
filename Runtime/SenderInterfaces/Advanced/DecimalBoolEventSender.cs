using UnityEngine;

namespace jeanf.EventSystem
{
    public class DecimalBoolEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public DecimalBoolEventChannelSO boolMessageChannel;
    
        public void SendIntBool(decimal nb, bool value)
        {
            boolMessageChannel.RaiseEvent(nb, value);
        }
    }
}

