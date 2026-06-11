using UnityEngine;

namespace jeanf.EventSystem
{
    public class ScriptableObjectEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public ScriptableObjectEventChannelSO scriptableObjectMessageChannel;
        public Component Source => this;
    
        public void SendScriptableObject(string id, ScriptableObject value)
        {
            this.Publish(() => scriptableObjectMessageChannel.RaiseEvent(id, value));
        }
    }
}

