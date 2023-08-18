using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/<Int,Bool> Event Channel")]
    
    public class IntBoolEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<int, bool> OnEventRaised;

        public void RaiseEvent(int nb, bool value)
        {
            OnEventRaised?.Invoke(nb, value);
        }
    }
}