#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace jeanf.EventSystem
{
    public class SendStringEventOnClick : MonoBehaviour
    {
        #if UNITY_EDITOR   
        [Header("Broadcasting on:")] [SerializeField]
        private StringEventChannelSO TestChannel;

        [Space(20)]
        [SerializeField] private string messageToSend = "Test123";

        public void CallFunction()
        {
            TestChannel.RaiseEvent(messageToSend);
        }
        #endif
    }
    
    #if UNITY_EDITOR
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
    #endif
}
