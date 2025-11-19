#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace jeanf.EventSystem
{
    public class SendVoidEventOnClick : MonoBehaviour
    {
        #if UNITY_EDITOR
        [Header("Broadcasting on:")] [SerializeField]
        private VoidEventChannelSO TestChannel;

        public void CallFunction()
        {
            TestChannel.RaiseEvent();
        }
        #endif
    }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(SendVoidEventOnClick))]
    public class VoidEventOnClickEditor : Editor {
        override public void  OnInspectorGUI () {
            DrawDefaultInspector();
            var eventToSend = (SendVoidEventOnClick)target;
            if(GUILayout.Button("Send void", GUILayout.Height(30))) {
                eventToSend.CallFunction(); // how do i call this?
            }
            GUILayout.Space(10);
        }
    }
    #endif
}