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
	public class TimelineTriggerEventListener : MonoBehaviour, IDebugBehaviour
	{
		public bool isDebug
		{ 
		    get => _isDebug;
		    set => _isDebug = value; 
		}
		[SerializeField] private bool _isDebug = false;
		
		[SerializeField] private TimelineTriggerEventChannelSO _channel = default;
		[SerializeField] private PlayableDirector _playableDirectorToControl;
		[Tooltip("Use this if you want to override the timeline assigned to the playable director.")]
		[SerializeField] private bool assignTimelineToPlayableDirector = true;
		[Space(10)]

		[Tooltip("If you want to do more than just turning triggering play/stop on the timeline")]
		public TimelineBoolEvent OnEventRaised;

		private void OnEnable()
		{
			if (_channel != null)
				_channel.OnEventRaised += Respond;
		}

		private void OnDisable()
		{
			if (_channel != null)
				_channel.OnEventRaised -= Respond;
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
	}
}