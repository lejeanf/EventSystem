using UnityEngine;
using UnityEngine.Serialization;

namespace jeanf.EventSystem
{
    public interface IStringEventSender
    {
        StringEventChannelSO stringMessageChannel { get; set; }
    }
}