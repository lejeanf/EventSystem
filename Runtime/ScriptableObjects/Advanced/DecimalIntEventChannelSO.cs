using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Decimal,Int> Event Channel")]
    
    public class DecimalIntEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<decimal, int> OnEventRaised;

        public void RaiseEvent(decimal nb, int value)
        {
            OnEventRaised?.Invoke(nb, value);
        }
    }
}