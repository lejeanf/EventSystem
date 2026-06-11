using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/Transform Event Channel")]
	public class TransformEventChannelSO : DescriptionBaseSO
	{
		[NonSerialized] private UnityAction<Transform> _onEventRaised;

		public event UnityAction<Transform> OnEventRaised
		{
			add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
			remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
		}

		public void RaiseEvent(Transform value)
		{
			EventDiagnostics.RecordRaise(this, value);
			CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(value);
		}
	}
}
