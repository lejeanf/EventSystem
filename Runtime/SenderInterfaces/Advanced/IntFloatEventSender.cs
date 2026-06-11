using UnityEngine;

namespace jeanf.EventSystem
{
    public class IntFloatEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public IntFloatEventChannelSO intfloatMessageChannel;
        public Component Source => this;
    
        public void SendIntFloat(int nb, float value)
        {
            this.Publish(() => intfloatMessageChannel.RaiseEvent(nb, value));
        }
    }
}

