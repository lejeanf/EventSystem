using jeanf.EventSystem;
using UnityEngine;

namespace jeanf.EventSystem
{
    public interface IScriptableObjectEventSender 
    {
        ScriptableObjectEventChannelSO channel { get; set; }
        void AddScriptableObjectToList(string id, ScriptableObject value);
    }
}