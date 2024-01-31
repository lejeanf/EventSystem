using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Int,Float> Event Channel")]
    
    public class IntFloatEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<int, float> OnEventRaised;

        public void RaiseEvent(int nb, float value)
        {
            OnEventRaised?.Invoke(nb, value);
        }
    }
}