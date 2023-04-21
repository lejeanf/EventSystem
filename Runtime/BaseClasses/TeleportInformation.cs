using UnityEngine;

namespace jeanf.EventSystem
{
    public class TeleportInformation
    {
        public bool objectIsPlayer;
        public Transform objectToTeleport;
        public Transform targetDestination;
        public bool isUsingFilter;
        public FilterSO filter;
        public TeleportInformation(Transform objectToTeleport, Transform targetDestination, bool objectIsPlayer, FilterSO filter, bool isUsingFilter)
        {
            this.objectToTeleport = objectToTeleport;
            this.targetDestination = targetDestination;
            this.objectIsPlayer = objectIsPlayer;
            this.filter = filter;
            this.isUsingFilter = isUsingFilter;
        }
    }
}