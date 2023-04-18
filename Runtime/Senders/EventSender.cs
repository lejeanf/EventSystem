using UnityEngine;
using UnityEngine.Serialization;

namespace jeanf.EventSystem
{
    public class EventSender : MonoBehaviour
    {
        [Header("Broadcasting on channels")]
        public StringEventChannelSO _stringMessageChannel;
        public BoolEventChannelSO _boolMessageChannel;
        public IntEventChannelSO _intMessageChannel;
        public VoidEventChannelSO _voidMessageChannel;
    }
}