using UnityEngine;
using UnityEngine.Serialization;

namespace jeanf.EventSystem
{
    public interface EventSender
    {
        StringEventChannelSO stringMessageChannel { get; set; }
    }
}