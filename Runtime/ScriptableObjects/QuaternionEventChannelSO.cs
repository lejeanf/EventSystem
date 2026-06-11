using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/Quaternion Event Channel")]
	public class QuaternionEventChannelSO : DescriptionBaseSO
	{
		[NonSerialized] private UnityAction<Quaternion> _onEventRaised;

		public event UnityAction<Quaternion> OnEventRaised
		{
			add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
			remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
		}

		public void RaiseEvent(Quaternion value)
		{
			EventDiagnostics.RecordRaise(this, value);
			CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(value);
		}
	}
}
