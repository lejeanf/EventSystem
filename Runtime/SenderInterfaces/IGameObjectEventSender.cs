using UnityEngine;
using UnityEngine.Serialization;

namespace jeanf.EventSystem
{
    public interface IGameObjectEventSender
    {
        GameObjectEventChannelSO GameObjectMessageChannel { get; set; }
    }
}