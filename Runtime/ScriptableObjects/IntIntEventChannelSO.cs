using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/<Int,Int> Event Channel")]
    
    public class IntIntEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<int, int> OnEventRaised;

        public void RaiseEvent(int nb, int value)
        {
            OnEventRaised?.Invoke(nb, value);
        }
    }
}