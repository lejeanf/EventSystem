using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Int,Enum> Event Channel")]
    
    public class IntEnumEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<int, Enum> OnEventRaised;

        public void RaiseEvent(int nb, Enum value)
        {
            OnEventRaised?.Invoke(nb, value);
        }
    }
}