using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/Vector3 Event Channel")]
	public class Vector3EventChannelSO : DescriptionBaseSO
	{
		[NonSerialized] private UnityAction<Vector3> _onEventRaised;

		public event UnityAction<Vector3> OnEventRaised
		{
			add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
			remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
		}

		public void RaiseEvent(Vector3 value)
		{
			EventDiagnostics.RecordRaise(this, value);
			CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(value);
		}
	}
}
