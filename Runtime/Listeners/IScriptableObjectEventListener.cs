using UnityEngine;

namespace jeanf.EventSystem
{
	public interface  IScriptableObjectEventListener
	{
		public ScriptableObjectEventChannelSO channel { get; set; }
		public ScriptableObjectEvent OnEventRaised { get; set; }

		private void OnEnable()
		{
			if (channel != null) channel.OnEventRaised += Respond;
		}

		private void OnDisable()
		{
			if (channel != null) channel.OnEventRaised -= Respond;
		}

		private void Respond(string id, ScriptableObject value)
		{
			OnEventRaised?.Invoke(value);
		}
	}
}