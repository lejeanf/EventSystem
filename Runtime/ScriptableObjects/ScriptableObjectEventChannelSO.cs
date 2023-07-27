using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace jeanf.EventSystem
{
	[CreateAssetMenu(menuName = "Events/ScriptableObject Event Channel")]
	public class ScriptableObjectEventChannelSO : DescriptionBaseSO
	{
		public UnityAction<string, ScriptableObject> OnEventRaised;
		public UnityAction<string, ScriptableObject> OnEventRemove;
		public Object type;
		[HideInInspector] public string id;

		public void RaiseEvent(string id, ScriptableObject value)
		{
			OnEventRaised?.Invoke(id, value);
		}

		public void UnsubscribeEvent(string id, ScriptableObject value)
		{
			OnEventRemove?.Invoke(id, value);
		}
	}
}