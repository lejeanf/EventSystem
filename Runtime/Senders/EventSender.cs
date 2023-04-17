using UnityEngine;
using UnityEngine.Serialization;

namespace jeanf.EventSystem
{
    public class EventSender : MonoBehaviour
    {
        [FormerlySerializedAs("_messageChannel")] [Header("Broadcasting on channels")]
        public StringEventChannelSO _stringMessageChannel;
    }
}