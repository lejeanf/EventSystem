using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<string,string> Event Channel")]
    
    public class StringStringEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<string, string> OnEventRaised;

        public void RaiseEvent(string str1, string str2)
        {
            OnEventRaised?.Invoke(str1, str2);
        }
    }
}