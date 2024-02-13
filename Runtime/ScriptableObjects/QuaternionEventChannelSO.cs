using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have one int argument.
/// Example: An Achievement unlock event, where the int is the Achievement ID.
/// </summary>
namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/Quaternion Event Channel")]
	public class QuaternionEventChannelSO : DescriptionBaseSO
	{
		public UnityAction<Quaternion> OnEventRaised;

		public void RaiseEvent(Quaternion value)
		{
			if (OnEventRaised != null)
				OnEventRaised.Invoke(value);
		}
	}
}
