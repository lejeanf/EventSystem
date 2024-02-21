using UnityEngine;
using UnityEngine.Playables;

namespace jeanf.EventSystem
{
    public class TimelineBoolEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public TimelineTriggerEventChannelSO timelineMessageChannel;
    
        public void SendIntBool(PlayableAsset timeline, bool value)
        {
            timelineMessageChannel.RaiseEvent(timeline, value);
        }
    }
}

