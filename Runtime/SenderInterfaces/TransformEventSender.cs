using UnityEngine;
using jeanf.propertyDrawer;

namespace jeanf.EventSystem
{
    public class TransformEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;
        [SerializeField] private bool _sendTransfromOnAwake;
        [SerializeField] private static bool _sendCustomTransform;
        [DrawIf("_sendCustomTransform", true, ComparisonType.Equals)]
        [SerializeField] private Transform _transform;

        [Header("Broadcasting on:")] public TransformEventChannelSO transformMessageChannel;
    
        public void SendTransform(Transform value)
        {
            transformMessageChannel.RaiseEvent(value);
        }

        public void SendCustomTransform()
        {
            SendTransform(_transform);
        }

        private void Awake()
        {
            if(!_sendTransfromOnAwake) return;
            
            if(!_sendCustomTransform)SendTransform(this.gameObject.transform);
            else
            {
                SendTransform(_transform);
            }
        }
    }
}

