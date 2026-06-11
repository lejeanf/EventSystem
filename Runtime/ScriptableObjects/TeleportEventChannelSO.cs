using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/Teleport Event Channel")]
	public class TeleportEventChannelSO : DescriptionBaseSO
	{
		[NonSerialized] private UnityAction<TeleportInformation> _onEventRaised;

		public event UnityAction<TeleportInformation> OnEventRaised
		{
			add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
			remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
		}

		public void RaiseEvent(TeleportInformation value)
		{
			EventDiagnostics.RecordRaise(this, value);
			CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(value);
		}
	}
}
