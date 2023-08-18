using UnityEngine;

namespace jeanf.EventSystem
{
    public class IntBoolEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public IntBoolEventChannelSO boolMessageChannel;
    
        public void SendIntBool(int nb, bool value)
        {
            boolMessageChannel.RaiseEvent(nb, value);
        }
    }
}

