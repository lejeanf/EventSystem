using UnityEngine;
using UnityEngine.Serialization;

namespace jeanf.EventSystem
{
    public interface ITeleportEventSender
    {
        TeleportEventChannelSO teleportMessageChannel { get; set; }
    }
}