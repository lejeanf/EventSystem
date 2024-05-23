using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Lists/String List Event Channel")]
    public class StringListEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<List<string>> OnEventRaised;

        public void RaiseEvent(List<string> strings)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(strings);
        }

    }
}
