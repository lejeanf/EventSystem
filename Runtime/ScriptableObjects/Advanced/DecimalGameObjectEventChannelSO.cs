using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Decimal,GameObject> Event Channel")]
    
    public class DecimalGameObjectEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<decimal, GameObject> OnEventRaised;

        public void RaiseEvent(decimal nb, GameObject value)
        {
            OnEventRaised?.Invoke(nb, value);
        }
    }
}