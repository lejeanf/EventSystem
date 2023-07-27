using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/Teleport Event Channel")]
	public class TeleportEventChannelSO : DescriptionBaseSO
	{
		public UnityAction<TeleportInformation> OnEventRaised;

		public void RaiseEvent(TeleportInformation value)
		{
			OnEventRaised?.Invoke(value);
		}
	}
}