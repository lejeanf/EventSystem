using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have one int argument.
/// Example: An Achievement unlock event, where the int is the Achievement ID.
/// </summary>
namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/Vector3 Event Channel")]
	public class Vector3EventChannelSO : DescriptionBaseSO
	{
		public UnityAction<Vector3> OnEventRaised;

		public void RaiseEvent(Vector3 value)
		{
			if (OnEventRaised != null)
				OnEventRaised.Invoke(value);
		}
	}
}
