using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Decimal,String> Event Channel")]
    
    public class DecimalStringEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<decimal, string> OnEventRaised;

        public void RaiseEvent(decimal nb, string value)
        {
            OnEventRaised?.Invoke(nb, value);
        }
    }
}