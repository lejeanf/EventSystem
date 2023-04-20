using UnityEngine;
using UnityEngine.Serialization;

namespace jeanf.EventSystem
{
    public interface IVoidEventSender
    {
        VoidEventChannelSO voidMessageChannel { get; set; }
    }
}