using jeanf.EventSystem;
using UnityEngine;

public class ScriptableObjectEventSender : MonoBehaviour, IDebugBehaviour
{
    public bool isDebug
    { 
        get => _isDebug;
        set => _isDebug = value; 
    }
    [SerializeField] private bool _isDebug = false;

    [field: Header("Broadcasting on:")] public ScriptableObjectEventChannelSO scriptableObjectMessageChannel;
    
    public void SendScriptableObject(string id, ScriptableObject value)
    {
        scriptableObjectMessageChannel.RaiseEvent(id, value);
    }
}
