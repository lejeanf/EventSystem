using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Lists/String List Event Channel")]
    public class StringListEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<List<string>> _onEventRaised;

        public event UnityAction<List<string>> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(List<string> strings)
        {
            EventDiagnostics.RecordRaise(this, strings);
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(strings);
        }
    }
}
