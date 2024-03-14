using UnityEngine;

namespace jeanf.EventSystem
{
    public class StringBoolEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        {
            get => _isDebug;
            set => _isDebug = value;
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public StringBoolEventChannelSO stringBoolMessageChannel;

        public void SendIntBool(string str, bool value)
        {
            stringBoolMessageChannel.RaiseEvent(str, value);
        }
    }
}
