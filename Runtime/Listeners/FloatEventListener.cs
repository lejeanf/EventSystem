using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
	[System.Serializable]
	public class FloatEvent : UnityEvent<float>
	{

	}
	
	public class FloatEventListener : MonoBehaviour
	{
		public FloatEventChannelSO _channel = default;

		public FloatEvent OnFloatEventRaised;

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

		private void Respond(float value)
		{
			OnFloatEventRaised?.Invoke(value);
		}

		public FloatEventListener(FloatEventChannelSO _channel, FloatEvent OnFloatEventRaised)
		{
			this._channel = _channel;
			this.OnFloatEventRaised = OnFloatEventRaised;
		}
	}
}