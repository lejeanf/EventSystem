using UnityEngine;

namespace jeanf.EventSystem
{
    public class IntFloatEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public IntFloatEventChannelSO intfloatMessageChannel;
    
        public void SendIntFloat(int nb, float value)
        {
            intfloatMessageChannel.RaiseEvent(nb, value);
        }
    }
}

