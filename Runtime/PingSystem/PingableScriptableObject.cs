using System;
using UnityEngine;

namespace jeanf.EventSystem
{
    using jeanf.propertyDrawer;
    public class PingableScriptableObject : MonoBehaviour, IScriptableObjectEventSender, IScriptableObjectEventListener, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;
    
        [SerializeField] private VoidEventChannelSO refreshPingableObjects;
        #if UNITY_EDITOR
        [ReadOnly] 
        #endif
        [SerializeField] private string id = Guid.NewGuid().ToString();
    
        public ScriptableObjectEventChannelSO channel
        {
            get => _channel;
            set => _channel = value;
        }
    
        [SerializeField] private ScriptableObjectEventChannelSO _channel;
        public ScriptableObjectEvent OnEventRaised { get; set; }
    
    
        private void Awake()
        {
            refreshPingableObjects.OnEventRaised += RefreshPingable;
        }
    
        private void OnEnable()
        {
            if (channel != null) channel.OnEventRaised += Respond;
            AddScriptableObjectToList(id, channel);
        }
        private void OnDisable() => Unsubscribe();
        private void OnDestroy() => Unsubscribe();
        private void Unsubscribe()
        {
    
            if (channel != null)
            {
                channel.UnsubscribeEvent(id, channel);
                channel.OnEventRaised -= null;
            }
    
            if (refreshPingableObjects.OnEventRaised != null)
                refreshPingableObjects.OnEventRaised -= null;
        }
    
        private void Respond(string id, ScriptableObject value)
        {
            OnEventRaised?.Invoke(value);
            if(isDebug) Debug.Log($" scriptableObject event raised: {value}");
        }
        public void AddScriptableObjectToList(string id, ScriptableObject value)
        {
            channel.RaiseEvent(id, value);
        }
    
        public void RefreshPingable()
        {
            AddScriptableObjectToList(id, channel);
        }
    }
}

