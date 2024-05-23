using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jeanf.EventSystem
{
    public class StringListEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        {
            get => _isDebug;
            set => _isDebug = value;
        }
        [SerializeField] private bool _isDebug = false;
        [field: Header("Broadcasting on:")] public StringListEventChannelSO stringListEventChannel;

        public void SendStringList(List<string> list)
        {
            stringListEventChannel.RaiseEvent(list);
        }

    }
}
