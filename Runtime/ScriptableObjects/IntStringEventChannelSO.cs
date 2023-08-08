using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/<Int,String> Event Channel")]
    
    public class IntStringEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<int, string> OnEventRaised;

        public void RaiseEvent(int nb, string value)
        {
            OnEventRaised?.Invoke(nb, value);
        }
    }
}