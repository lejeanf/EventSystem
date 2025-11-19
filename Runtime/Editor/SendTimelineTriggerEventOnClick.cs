#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Playables;

namespace jeanf.EventSystem
{
    public class SendTimelineTriggerEventOnClick : MonoBehaviour
    {
     #if UNITY_EDITOR   
        [Header("Broadcasting on:")] [SerializeField]
        private TimelineTriggerEventChannelSO TestChannel;

        [Space(20)]
        [SerializeField] private PlayableAsset timeline;
        [SerializeField] private bool state = true;
        public void CallFunction()
        {
            TestChannel.RaiseEvent(timeline, state);
        }
        #endif
    }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(SendTimelineTriggerEventOnClick))]
    public class SendTimelineTriggerEventOnClickEditor : Editor {
        override public void  OnInspectorGUI () {
            DrawDefaultInspector();
            var eventToSend = (SendTimelineTriggerEventOnClick)target;
            if(GUILayout.Button("Send timeline trigger", GUILayout.Height(30))) {
                eventToSend.CallFunction(); // how do i call this?
            }
            GUILayout.Space(10);
        }
    }
    #endif
}