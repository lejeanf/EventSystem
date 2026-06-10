using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have one int argument.
/// Example: An Achievement unlock event, where the int is the Achievement ID.
/// </summary>
namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/Int Event Channel")]
	public class IntEventChannelSO : DescriptionBaseSO
	{
		[NonSerialized] private UnityAction<int> _onEventRaised;

		public event UnityAction<int> OnEventRaised
		{
			add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
			remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
		}

		public void RaiseEvent(int value)
		{
			CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(value);
		}
	}
}
