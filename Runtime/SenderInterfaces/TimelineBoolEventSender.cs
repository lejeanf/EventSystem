using UnityEngine;
using UnityEngine.Playables;

namespace jeanf.EventSystem
{
    public class TimelineBoolEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public TimelineTriggerEventChannelSO timelineMessageChannel;
        public Component Source => this;
    
        public void SendIntBool(PlayableAsset timeline, bool value)
        {
            this.Publish(() => timelineMessageChannel.RaiseEvent(timeline, value));
        }
    }
}

