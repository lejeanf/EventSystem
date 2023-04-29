using jeanf.EventSystem;
using UnityEngine;

public class BoolEventSender : MonoBehaviour, IDebugBehaviour
{
    public bool isDebug
    { 
        get => _isDebug;
        set => _isDebug = value; 
    }
    [SerializeField] private bool _isDebug = false;

    [field: Header("Broadcasting on:")] public BoolEventChannelSO boolMessageChannel;
    
    public void SendBool(bool value)
    {
        boolMessageChannel.RaiseEvent(value);
    }
}
