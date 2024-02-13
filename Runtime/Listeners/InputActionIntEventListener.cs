using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
/// <summary>
/// To use a generic UnityEvent type you must override the generic type.
/// </summary>
namespace jeanf.EventSystem
{
	[System.Serializable]
	public class InputActionIntEvent : UnityEvent<InputAction, int>
	{

	}

	/// <summary>
	/// A flexible handler for int events in the form of a MonoBehaviour. Responses can be connected directly from the Unity Inspector.
	/// </summary>
	public class InputActionIntEventListener : MonoBehaviour, IDebugBehaviour
	{
		public bool isDebug
		{
			get => _isDebug;
			set => _isDebug = value;
		}
		[SerializeField] private bool _isDebug = false;

		[SerializeField] private InputActionIntEventChannelSO _channel = default;

		public InputActionIntEvent OnEventRaised;

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

		private void Respond(InputAction action, int value)
		{
			OnEventRaised?.Invoke(action, value);
			if (isDebug) Debug.Log($" int-bool event raised: <{action},{value}>");
		}
	}
}
