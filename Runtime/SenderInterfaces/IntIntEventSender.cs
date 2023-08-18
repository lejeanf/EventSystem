using UnityEngine;

namespace jeanf.EventSystem
{
    public class IntIntEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public IntIntEventChannelSO intintMessageChannel;
    
        public void SendIntInt(int nb, int value)
        {
            intintMessageChannel.RaiseEvent(nb, value);
        }
    }
}

