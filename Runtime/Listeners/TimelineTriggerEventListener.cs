﻿using UnityEngine;
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
		private PlayState _lastPlayableState;
		
		[Tooltip("Use this if you want to override the timeline assigned to the playable director.")]
		[SerializeField] private bool assignTimelineToPlayableDirector = true;
		[Space(10)]

		[Tooltip("If you want to do more than just turning triggering play/stop on the timeline")]
		public TimelineBoolEvent OnEventRaised;

		[SerializeField] private BoolEventChannelSO generalPauseEvent;
		private bool isStop = true;

		private void OnEnable()
		{
            isStop = true;
            if (_channel != null) _channel.OnEventRaised += Respond;
            if (generalPauseEvent) generalPauseEvent.OnEventRaised += Pause;
		}

		private void OnDisable()
		{
			if (_channel != null) _channel.OnEventRaised -= Respond;
			if (generalPauseEvent) generalPauseEvent.OnEventRaised -= Pause;
		}

		private void Respond(PlayableAsset timeline, bool value)
		{
			if (assignTimelineToPlayableDirector) _playableDirectorToControl.playableAsset = timeline;
			if(timeline != _playableDirectorToControl.playableAsset) return;
			OnEventRaised?.Invoke(timeline, value); 
			if (value)
			{
				isStop = false;
				_playableDirectorToControl.Play();
				_lastPlayableState = _playableDirectorToControl.state;
			}
			else
			{
				isStop = true;
				_playableDirectorToControl.Stop();
				_lastPlayableState = _playableDirectorToControl.state;
			}

			if(isDebug) Debug.Log($" timeline-bool event raised: <{timeline},{value}>, timelineState: {_playableDirectorToControl.state}");
		}

		private void Pause(bool state)
		{
			switch (state && !isStop)
			{
				case true when _lastPlayableState == PlayState.Playing:
					_playableDirectorToControl.Pause();
					_lastPlayableState = _playableDirectorToControl.state;
					break;
				
				case false when _lastPlayableState == PlayState.Paused:
					_playableDirectorToControl.Play();
					_lastPlayableState = _playableDirectorToControl.state;
					break;
			}
		}
	}
}