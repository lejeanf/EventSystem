using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/Transform Event Channel")]
	public class TransformEventChannelSO : DescriptionBaseSO
	{
		public UnityAction<Transform> OnEventRaised;

		public void RaiseEvent(Transform value)
		{
			OnEventRaised?.Invoke(value);
		}
	}
}