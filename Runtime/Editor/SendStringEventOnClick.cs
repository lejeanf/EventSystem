#if UNITY_EDITOR

using jeanf.EventSystem;
using UnityEditor;
using UnityEngine;

namespace jeanf.EventSystem
{
    public class SendStringEventOnClick : MonoBehaviour
    {
        
        [Header("Broadcasting on:")] [SerializeField]
        private StringEventChannelSO TestChannel;

        [Space(20)]
        [SerializeField] private string messageToSend = "Test123";

        public void CallFunction()
        {
            TestChannel.RaiseEvent(messageToSend);
        }
    }
    
    [CustomEditor(typeof(SendStringEventOnClick))]
    public class StringEventOnClickEditor : Editor {
        override public void  OnInspectorGUI () {
            DrawDefaultInspector();
            var eventToSend = (SendStringEventOnClick)target;
            if(GUILayout.Button("Send string", GUILayout.Height(30))) {
                eventToSend.CallFunction(); // how do i call this?
            }
            GUILayout.Space(10);
        }
    }
}

#endif