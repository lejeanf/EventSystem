using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Timeline,Bool> Event Channel")]
    public class TimelineTriggerEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<PlayableAsset, bool> _onEventRaised;

        public event UnityAction<PlayableAsset, bool> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(PlayableAsset timeline, bool state)
        {
            EventDiagnostics.RecordRaise(this, (timeline, state));
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(timeline, state);
        }
    }
}
