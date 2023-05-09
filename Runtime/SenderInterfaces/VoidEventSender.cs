using jeanf.EventSystem;
using UnityEngine;

public class VoidEventSender : MonoBehaviour, IDebugBehaviour
{
    public bool isDebug
    { 
        get => _isDebug;
        set => _isDebug = value; 
    }
    [SerializeField] private bool _isDebug = false;

    [field: Header("Broadcasting on:")] public VoidEventChannelSO voidMessageChannel;
    
    public void SendVoidEvent()
    {
        voidMessageChannel.RaiseEvent();
    }
}
