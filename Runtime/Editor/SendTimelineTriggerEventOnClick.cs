#if UNITY_EDITOR

using jeanf.EventSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace jeanf.EventSystem
{
    public class SendTimelineTriggerEventOnClick : MonoBehaviour
    {
        
        [Header("Broadcasting on:")] [SerializeField]
        private TimelineTriggerEventChannelSO TestChannel;

        [Space(20)]
        [SerializeField] private PlayableAsset timeline;
        [SerializeField] private bool state = true;
        public void CallFunction()
        {
            TestChannel.RaiseEvent(timeline, state);
        }
    }
    
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
}

#endif