using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have one int argument.
/// Example: An Achievement unlock event, where the int is the Achievement ID.
/// </summary>
namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/Advanced/GameObjectIntBoolEventChannel")]
	public class GameObjectIntBoolEventChannelSO : DescriptionBaseSO
	{
		public UnityAction<GameObject, int, bool> OnEventRaised;

		public void RaiseEvent(GameObject gameObject, int number, bool value)
		{
			if (OnEventRaised != null)
				OnEventRaised.Invoke(gameObject, number, value);
		}
	}
}
