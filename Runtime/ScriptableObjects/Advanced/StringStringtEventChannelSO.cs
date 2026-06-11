using System;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Advanced/<string,string> Event Channel")]
    public class StringStringEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<string, string> _onEventRaised;

        public event UnityAction<string, string> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(string str1, string str2)
        {
            EventDiagnostics.RecordRaise(this, (str1, str2));
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(str1, str2);
        }
    }
}
