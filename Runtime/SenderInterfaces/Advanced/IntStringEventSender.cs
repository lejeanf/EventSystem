using UnityEngine;

namespace jeanf.EventSystem
{
    public class IntStringEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public IntStringEventChannelSO intintMessageChannel;
    
        public void SendIntString(int nb, string value)
        {
            intintMessageChannel.RaiseEvent(nb, value);
        }
    }
}

