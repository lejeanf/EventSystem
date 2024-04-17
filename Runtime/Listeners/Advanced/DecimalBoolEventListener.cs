using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// To use a generic UnityEvent type you must override the generic type.
/// </summary>
namespace jeanf.EventSystem
{
	[System.Serializable]
	public class DecimalBoolEvent : UnityEvent<decimal, bool>
	{

	}

	/// <summary>
	/// A flexible handler for int events in the form of a MonoBehaviour. Responses can be connected directly from the Unity Inspector.
	/// </summary>
	public class DecimalBoolEventListener : MonoBehaviour, IDebugBehaviour
	{
		public bool isDebug
		{ 
		    get => _isDebug;
		    set => _isDebug = value; 
		}
		[SerializeField] private bool _isDebug = false;
		
		[SerializeField] private DecimalBoolEventChannelSO _channel = default;

		public DecimalBoolEvent OnEventRaised;

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

		private void Respond(decimal nb, bool value)
		{
			OnEventRaised?.Invoke(nb, value);
			if(isDebug) Debug.Log($" decimal-bool event raised: <{nb},{value}>");
		}
	}
}