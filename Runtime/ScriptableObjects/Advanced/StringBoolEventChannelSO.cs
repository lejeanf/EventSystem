using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<string,bool> Event Channel")]
    public class StringBoolEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<string, bool> _onEventRaised;

        public event UnityAction<string, bool> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(string str1, bool value)
        {
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(str1, value);
        }
    }
}
