using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<string,float> Event Channel")]
    
    public class StringFloatEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<string, float> OnEventRaised;

        public void RaiseEvent(string str, float value)
        {
            OnEventRaised?.Invoke(str, value);
        }
    }
}