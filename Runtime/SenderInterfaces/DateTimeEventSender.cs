using System;
using jeanf.EventSystem;
using UnityEngine;

public class DateTimeEventSender : MonoBehaviour, IDebugBehaviour
{
    public bool isDebug
    { 
        get => _isDebug;
        set => _isDebug = value; 
    }
    [SerializeField] private bool _isDebug = false;

    [field: Header("Broadcasting on:")] public DateTimeEventChannelSO boolMessageChannel;
    
    public void SendBool(DateTime value)
    {
        boolMessageChannel.RaiseEvent(value);
    }
}
