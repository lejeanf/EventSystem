#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace jeanf.EventSystem
{
    public class SendDecimalEventOnClick : MonoBehaviour
    {
#if UNITY_EDITOR
        [Header("Broadcasting on:")] [SerializeField]
        private DecimalEventChannelSO TestChannel;

        [Space(20)]
        [SerializeField] private decimal messageToSend = 1;
        public void CallFunction()
        {
            TestChannel.RaiseEvent(messageToSend);
        }
        #endif
    }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(SendDecimalEventOnClick))]
    public class DecimalEventOnClickEditor : Editor {
        override public void  OnInspectorGUI () {
            DrawDefaultInspector();
            var eventToSend = (SendDecimalEventOnClick)target;
            if(GUILayout.Button("Send int", GUILayout.Height(30))) {
                eventToSend.CallFunction(); // how do i call this?
            }
            GUILayout.Space(10);
        }
    }
    #endif
}