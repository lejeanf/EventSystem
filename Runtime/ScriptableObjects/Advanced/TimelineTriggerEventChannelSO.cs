using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Timeline,Bool> Event Channel")]
    
    public class TimelineTriggerEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<PlayableAsset, bool> OnEventRaised;

        public void RaiseEvent(PlayableAsset timeline, bool state)
        {
            OnEventRaised?.Invoke(timeline, state);
        }
    }
}