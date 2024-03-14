using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<string,bool> Event Channel")]
    public class StringBoolEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<string, bool> OnEventRaised;

        public void RaiseEvent(string str1, bool value)
        {
            OnEventRaised?.Invoke(str1, value);
        }
    }
}
