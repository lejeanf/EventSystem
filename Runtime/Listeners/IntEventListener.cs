using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
	[System.Serializable]
	public class IntEvent : UnityEvent<int>
	{

	}
	
	public class IntEventListener : MonoBehaviour
	{
		public IntEventChannelSO _channel = default;

		public IntEvent OnIntEventRaised;

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

		private void Respond(int value)
		{
			OnIntEventRaised?.Invoke(value);
		}

		public IntEventListener(IntEventChannelSO _channel, IntEvent OnIntEventRaised)
		{
			this._channel = _channel;
			this.OnIntEventRaised = OnIntEventRaised;
		}
	}
}