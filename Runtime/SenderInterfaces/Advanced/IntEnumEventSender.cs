using System;
using UnityEngine;

namespace jeanf.EventSystem
{
    public class IntEnumEventSender : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }

        [SerializeField] private bool _isDebug = false;

        [field: Header("Broadcasting on:")] public IntEnumEventChannelSO intEnumMessageChannel;
    
        public void SendIntEnum(int nb, Enum value)
        {
            intEnumMessageChannel.RaiseEvent(nb, value);
        }
    }
}

