using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Decimal,Bool> Event Channel")]
    
    public class DecimalBoolEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<decimal, bool> OnEventRaised;

        public void RaiseEvent(decimal nb, bool value)
        {
            OnEventRaised?.Invoke(nb, value);
        }
    }
}