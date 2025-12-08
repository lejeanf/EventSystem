using UnityEngine;

namespace jeanf.EventSystem
{
    public class VoidEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public VoidEventChannelSO voidMessageChannel;

        [SerializeField] private bool sendEventOnAwake = true;

        private void Awake()
        {
            if(sendEventOnAwake) SendVoidEvent();
        }

        public void SendVoidEvent()
        {
            if (voidMessageChannel == null)
            {
                Debug.LogError($"[VoidEventSender] Missing voidMessageChannel reference on GameObject: {gameObject.name}. " +
                             $"Please assign a VoidEventChannelSO to this component.", this);
                return;
            }

            voidMessageChannel.RaiseEvent();
        }
    }
}