using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/Vector2 Event Channel")]
	public class Vector2EventChannelSO : DescriptionBaseSO
	{
		[NonSerialized] private UnityAction<Vector2> _onEventRaised;

		public event UnityAction<Vector2> OnEventRaised
		{
			add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
			remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
		}

		public void RaiseEvent(Vector2 value)
		{
			EventDiagnostics.RecordRaise(this, value);
			CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(value);
		}
	}
}
