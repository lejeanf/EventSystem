using System;
using jeanf.EventSystem;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Bool Float Event Channel")]
public class BoolFloatEventChannelSO : DescriptionBaseSO, RaiseEvent
{
    [NonSerialized] private UnityAction<bool, float> _onEventRaised;

    public event UnityAction<bool, float> OnEventRaised
    {
        add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
        remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
    }

    public void RaiseEvent(bool boolean, float value)
    {
        CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(boolean, value);
    }
}
