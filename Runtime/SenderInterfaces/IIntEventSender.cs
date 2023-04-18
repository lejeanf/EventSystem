using UnityEngine;
using UnityEngine.Serialization;

namespace jeanf.EventSystem
{
    public interface IIntEventSender
    {
        IntEventChannelSO intMessageChannel { get; set; }
    }
}