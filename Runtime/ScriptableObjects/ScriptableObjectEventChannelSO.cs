using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/ScriptableObject Event Channel")]
	public class ScriptableObjectEventChannelSO : DescriptionBaseSO
	{
		[NonSerialized] private UnityAction<string, ScriptableObject> _onEventRaised;
		[NonSerialized] private UnityAction<string, ScriptableObject> _onEventRemove;

		public event UnityAction<string, ScriptableObject> OnEventRaised
		{
			add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
			remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
		}

		public event UnityAction<string, ScriptableObject> OnEventRemove
		{
			add { CanonicalChannelResolver.GetCanonical(this)._onEventRemove += value; }
			remove { CanonicalChannelResolver.GetCanonical(this)._onEventRemove -= value; }
		}

		public Object type;
		[HideInInspector] public string id;

		public void RaiseEvent(string id, ScriptableObject value)
		{
			CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(id, value);
		}

		public void UnsubscribeEvent(string id, ScriptableObject value)
		{
			CanonicalChannelResolver.GetCanonical(this)._onEventRemove?.Invoke(id, value);
		}
	}
}
