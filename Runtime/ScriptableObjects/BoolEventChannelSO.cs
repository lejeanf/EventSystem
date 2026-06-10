using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/Bool Event Channel")]
	public class BoolEventChannelSO : DescriptionBaseSO
	{
		[NonSerialized] private UnityAction<bool> _onEventRaised;

		public event UnityAction<bool> OnEventRaised
		{
			add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
			remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
		}

		public void RaiseEvent(bool value)
		{
			CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(value);
		}
	}
}
