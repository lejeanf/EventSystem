using jeanf.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Bool Float Event Channel")]
public class BoolFloatEventChannelSO : DescriptionBaseSO, RaiseEvent
{
    public UnityAction<bool, float> OnEventRaised;

    public void RaiseEvent(bool boolean, float value)
    {
        OnEventRaised?.Invoke(boolean, value);
    }
}
