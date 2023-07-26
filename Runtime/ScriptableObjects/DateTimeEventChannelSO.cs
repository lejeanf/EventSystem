using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/DateTime Event Channel")]
	public class DateTimeEventChannelSO : DescriptionBaseSO
	{
		public UnityAction<DateTime> OnEventRaised;

		public void RaiseEvent(DateTime value)
		{
			OnEventRaised?.Invoke(value);
		}
	}
}