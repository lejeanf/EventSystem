using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jeanf.EventSystem
{
    public class TeleportInformation
    {
        public bool objectIsPlayer;
        public Transform objectToTeleport;
        public Transform targetDestination;
        public bool alignToTarget;
        
        public TeleportInformation(Transform targetDestination, bool alignToTarget)
        {
            this.targetDestination = targetDestination;
            this.alignToTarget = alignToTarget;
            this.objectIsPlayer = true;
        }
		
        public TeleportInformation(Transform objectToTeleport, Transform targetDestination, bool alignToTarget, bool objectIsPlayer)
        {
            this.objectToTeleport = objectToTeleport;
            this.targetDestination = targetDestination;
            this.alignToTarget = alignToTarget;
            this.objectIsPlayer = objectIsPlayer;
        }
    }
}