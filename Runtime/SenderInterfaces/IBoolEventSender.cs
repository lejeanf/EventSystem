using UnityEngine;
using UnityEngine.Serialization;

namespace jeanf.EventSystem
{
    public interface IBoolEventSender
    {
        BoolEventChannelSO boolMessageChannel { get; set; }
    }
}