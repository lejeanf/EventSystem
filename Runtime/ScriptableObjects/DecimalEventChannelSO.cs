using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have one decimal argument.
/// </summary>
namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/Decimal Event Channel")]
	public class DecimalEventChannelSO : DescriptionBaseSO
	{
		[NonSerialized] private UnityAction<decimal> _onEventRaised;

		public event UnityAction<decimal> OnEventRaised
		{
			add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
			remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
		}

		public void RaiseEvent(decimal value)
		{
			CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(value);
		}
	}
}
