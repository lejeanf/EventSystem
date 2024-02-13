using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jeanf.EventSystem
{
    public interface IInputActionEventSender
    {
        InputActionEventChannelSO inputActionMessageChannel { get; set; }
    }
}
