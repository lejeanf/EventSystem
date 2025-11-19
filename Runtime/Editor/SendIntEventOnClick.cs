#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace jeanf.EventSystem
{
    public class SendIntEventOnClick : MonoBehaviour
    {
        #if UNITY_EDITOR
        [Header("Broadcasting on:")] [SerializeField]
        private IntEventChannelSO TestChannel;

        [Space(20)]
        [SerializeField] private int messageToSend = 1;
        public void CallFunction()
        {
            TestChannel.RaiseEvent(messageToSend);
        }
        #endif
    }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(SendIntEventOnClick))]
    public class IntEventOnClickEditor : Editor {
        override public void  OnInspectorGUI () {
            DrawDefaultInspector();
            var eventToSend = (SendIntEventOnClick)target;
            if(GUILayout.Button("Send int", GUILayout.Height(30))) {
                eventToSend.CallFunction(); // how do i call this?
            }
            GUILayout.Space(10);
        }
    }
    #endif
}