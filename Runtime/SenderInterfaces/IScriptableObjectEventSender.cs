using jeanf.EventSystem;
using UnityEngine;

public interface IScriptableObjectEventSender 
{
   ScriptableObjectEventChannelSO channel { get; set; }
   void AddScriptableObjectToList(string id, ScriptableObject value);
}
