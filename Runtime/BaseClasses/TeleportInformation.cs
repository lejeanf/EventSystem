using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;

namespace jeanf.EventSystem
{
    public class TeleportInformation
    {
        public bool objectIsPlayer;
        public Transform objectToTeleport;
        public Transform targetDestination;
        public bool alignToTarget;
        public bool isUsingFilter;
        public FilterSO filter;
        
        public TeleportInformation(Transform targetDestination, bool alignToTarget)
        {
            this.targetDestination = targetDestination;
            this.alignToTarget = alignToTarget;
            this.objectIsPlayer = true;
        }
		
        public TeleportInformation(Transform objectToTeleport, Transform targetDestination, bool alignToTarget, bool objectIsPlayer, FilterSO filter, bool isUsingFilter)
        {
            this.objectToTeleport = objectToTeleport;
            this.targetDestination = targetDestination;
            this.alignToTarget = alignToTarget;
            this.objectIsPlayer = objectIsPlayer;
            this.filter = filter;
            this.isUsingFilter = isUsingFilter;
        }
    }
}