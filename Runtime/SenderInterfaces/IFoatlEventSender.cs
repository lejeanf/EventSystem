using UnityEngine;
using UnityEngine.Serialization;

namespace jeanf.EventSystem
{
    public interface IFloatEventSender
    {
        FloatEventChannelSO floatMessageChannel { get; set; }
    }
}