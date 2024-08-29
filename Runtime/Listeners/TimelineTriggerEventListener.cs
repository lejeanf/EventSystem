using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Serialization;

/// <summary>
/// To use a generic UnityEvent type you must override the generic type.
/// </summary>
namespace jeanf.EventSystem
{
	[System.Serializable]
	public class TimelineBoolEvent : UnityEvent<PlayableAsset, bool>
	{

	}

	/// <summary>
	/// A flexible handler for int events in the form of a MonoBehaviour. Responses can be connected directly from the Unity Inspector.
	/// </summary>
	[DefaultExecutionOrder(-1)]
	public class TimelineTriggerEventListener : MonoBehaviour, IDebugBehaviour
	{
		public bool isDebug
		{ 
		    get => _isDebug;
		    set => _isDebug = value; 
		}
		[SerializeField] private bool _isDebug = false;
		
		[SerializeField] private TimelineTriggerEventChannelSO _channel;
		[SerializeField] private PlayableDirector _playableDirectorToControl;
		[Tooltip("Use this if you want to override the timeline assigned to the playable director.")]
		[SerializeField] private bool assignTimelineToPlayableDirector = true;
		[Space(10)]

		[Tooltip("If you want to do more than just turning triggering play/stop on the timeline")]
		public TimelineBoolEvent OnEventRaised;

		[SerializeField] private BoolEventChannelSO generalPauseEvent;

		private void OnEnable()
		{
            if (_channel != null)
				_channel.OnEventRaised += Respond;
            if (generalPauseEvent) generalPauseEvent.OnEventRaised += Pause;
		}

		private void OnDisable()
		{
			if (_channel != null)
				_channel.OnEventRaised -= Respond;
			if (generalPauseEvent) generalPauseEvent.OnEventRaised -= Pause;
		}

		private void Respond(PlayableAsset timeline, bool value)
		{
			if (assignTimelineToPlayableDirector) _playableDirectorToControl.playableAsset = timeline;
			if(timeline != _playableDirectorToControl.playableAsset) return;
			OnEventRaised?.Invoke(timeline, value); 
			if (value)
			{
				_playableDirectorToControl.Play();
			}
			else
			{
				_playableDirectorToControl.Stop();
			}
			if(isDebug) Debug.Log($" timeline-bool event raised: <{timeline},{value}>");
		}

		private PlayState _lastPlayableState;
		private void Pause(bool state)
		{
			switch (state)
			{
				case true when _lastPlayableState == PlayState.Playing:
					_playableDirectorToControl.Pause();
					break;
				
				case false when _lastPlayableState == PlayState.Paused:
					_playableDirectorToControl.Play();
					break;
			}

			_lastPlayableState = _playableDirectorToControl.state;
		}
	}
}