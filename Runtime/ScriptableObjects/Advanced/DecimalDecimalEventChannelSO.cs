using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Decimal,Decimal> Event Channel")]
    
    public class DecimalDecimalEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<decimal, decimal> OnEventRaised;

        public void RaiseEvent(decimal nb, decimal value)
        {
            OnEventRaised?.Invoke(nb, value);
        }
    }
}