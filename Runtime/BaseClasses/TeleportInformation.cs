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
        public bool shouldFade; // screen fade in/out during teleport
        
        // fade is activated by default if we don't specify otherwise
        public TeleportInformation(Transform objectToTeleport, Transform targetDestination, bool objectIsPlayer, FilterSO filter, bool isUsingFilter)
        {
            this.objectToTeleport = objectToTeleport;
            this.targetDestination = targetDestination;
            this.objectIsPlayer = objectIsPlayer;
            this.filter = filter;
            this.isUsingFilter = isUsingFilter;
            this.shouldFade = true;
        }
        
        // here we can specify to desactivate the fade using the shouldFade parameter
        public TeleportInformation(Transform objectToTeleport, Transform targetDestination, bool objectIsPlayer, FilterSO filter, bool isUsingFilter,  bool shouldFade)
        {
            this.objectToTeleport = objectToTeleport;
            this.targetDestination = targetDestination;
            this.objectIsPlayer = objectIsPlayer;
            this.filter = filter;
            this.isUsingFilter = isUsingFilter;
            this.shouldFade = shouldFade;
        }
    }
}