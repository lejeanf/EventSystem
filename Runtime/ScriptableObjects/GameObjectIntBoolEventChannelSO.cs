using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/Advanced/GameObjectIntBoolEventChannel")]
	public class GameObjectIntBoolEventChannelSO : DescriptionBaseSO
	{
		[NonSerialized] private UnityAction<GameObject, int, bool> _onEventRaised;

		public event UnityAction<GameObject, int, bool> OnEventRaised
		{
			add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
			remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
		}

		public void RaiseEvent(GameObject gameObject, int number, bool value)
		{
			EventDiagnostics.RecordRaise(this, (gameObject, number, value));
			CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(gameObject, number, value);
		}
	}
}
