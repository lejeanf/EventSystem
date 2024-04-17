using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Decimal,Float> Event Channel")]
    
    public class DecimalFloatEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<decimal, float> OnEventRaised;

        public void RaiseEvent(decimal nb, float value)
        {
            OnEventRaised?.Invoke(nb, value);
        }
    }
}