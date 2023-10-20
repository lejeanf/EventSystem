using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<Int,GameObject> Event Channel")]
    
    public class IntGameObjectEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<int, GameObject> OnEventRaised;

        public void RaiseEvent(int nb, GameObject value)
        {
            OnEventRaised?.Invoke(nb, value);
        }
    }
}