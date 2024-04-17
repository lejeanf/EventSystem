using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace jeanf.EventSystem
{
	[System.Serializable]
	public class DecimalEvent : UnityEvent<decimal>
	{

	}
	
	public class DecimalEventListener : MonoBehaviour
	{
		public DecimalEventChannelSO _channel = default;
		public DecimalEvent OnDecimalEventRaised;

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

		private void Respond(decimal value)
		{
			OnDecimalEventRaised?.Invoke(value);
		}

		public DecimalEventListener(DecimalEventChannelSO _channel, DecimalEvent onDecimalEventRaised)
		{
			this._channel = _channel;
			this.OnDecimalEventRaised = onDecimalEventRaised;
		}
	}
}