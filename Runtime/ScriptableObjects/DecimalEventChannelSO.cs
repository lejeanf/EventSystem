using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have one int argument.
/// Example: An Achievement unlock event, where the int is the Achievement ID.
/// </summary>
namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/Float Event Channel")]
	public class DecimalEventChannelSO : DescriptionBaseSO
	{
		public UnityAction<decimal> OnEventRaised;

		public void RaiseEvent(decimal value)
		{
			if (OnEventRaised != null)
				OnEventRaised.Invoke(value);
		}
	}
}