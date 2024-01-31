using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface RaiseEvent
{
    public delegate void TransferFunction();
    
    public void RaiseEvent(TransferFunction transferFunction)
    {
        transferFunction.Invoke();
    }
}
