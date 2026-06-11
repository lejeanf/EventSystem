using UnityEngine;

namespace jeanf.EventSystem
{
    public class StringBoolEventSender : MonoBehaviour, IDebugBehaviour, IEventPublisher
    {
        public bool isDebug
        {
            get => _isDebug;
            set => _isDebug = value;
        }
        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public StringBoolEventChannelSO stringBoolMessageChannel;
        public Component Source => this;

        public void SendIntBool(string str, bool value)
        {
            this.Publish(() => stringBoolMessageChannel.RaiseEvent(str, value));
        }
    }
}
