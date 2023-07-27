using System;
using System.Collections.Generic;
using jeanf.EventSystem;
using UnityEngine;
using Object = UnityEngine.Object;

public class ScriptableObjectManager : MonoBehaviour, IDebugBehaviour
{
    public bool isDebug
    { 
        get => _isDebug;
        set => _isDebug = value; 
    }
    [SerializeField] private bool _isDebug = false;
    
    [SerializeField] private VoidEventChannelSO RefreshPingableObjects;
    [SerializeField] private List<ScriptableObjectEventChannelSO> listOfPingableObjects = new List<ScriptableObjectEventChannelSO>();

    private Dictionary<Object, Dictionary<string, Object>> genericDictionary = new Dictionary<Object, Dictionary<string, Object>>();

    private void Awake()
    {
        foreach (var so in listOfPingableObjects)
        {
            Debug.Log($"registering events for channel: {so.name}");
            so.OnEventRaised += RegisterSO;
            so.OnEventRemove += RemoveSOFromLists;
        }
    }

    private void OnEnable() => RefreshPingableObjects.RaiseEvent();

    private void OnDestroy()
    {
        foreach (var so in listOfPingableObjects)
        {
            Debug.Log($"unregistering events for channel: {so.name}");
            so.OnEventRaised -= null;
            so.OnEventRemove -= null;
        }
    }

    private void RegisterSO(string id, ScriptableObject so)
    {
        UpdateLists(id, so);
    }

    private void UpdateLists(string id, ScriptableObject so)
    {
        if (so is not ScriptableObjectEventChannelSO mySo) return;
        var type = mySo.type;
        if(genericDictionary.ContainsKey(type))
        {
            if (!genericDictionary[type].ContainsKey(id))
            {
                if(isDebug) Debug.Log($"adding [id: {id}] to the list of type {so.name}");
                genericDictionary[type].Add(id, mySo);
            }
        }
        else
        {
            if(isDebug) Debug.Log($"new type detected, adding a list of type {so.name} and adding [id: {id}] to it");
            genericDictionary.Add(type, new Dictionary<string, Object>(){{id, mySo}});
        }
    }

    private void RemoveSOFromLists(string id, ScriptableObject so)
    {
        if (so is not ScriptableObjectEventChannelSO mySo) return;
        var type = mySo.type;
        if (!genericDictionary.ContainsKey(type)) return;
        if (!genericDictionary[type].ContainsKey(id)) return;
        if(isDebug) Debug.Log($"removing [id: {id}] to the list of type {so.name}");
        genericDictionary[type].Remove(id);
    }
}
