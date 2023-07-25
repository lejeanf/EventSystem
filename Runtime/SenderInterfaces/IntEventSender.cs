using jeanf.EventSystem;
using UnityEngine;

public class IntEventSender : MonoBehaviour, IDebugBehaviour
{
    public bool isDebug
    { 
        get => _isDebug;
        set => _isDebug = value; 
    }
    [SerializeField] private bool _isDebug = false;

    [field: Header("Broadcasting on:")] public IntEventChannelSO intMessageChannel;
    
    public void SendInt(int value)
    {
        intMessageChannel.RaiseEvent(value);
    }
}
