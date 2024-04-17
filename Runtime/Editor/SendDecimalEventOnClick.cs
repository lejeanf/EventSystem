#if UNITY_EDITOR

using jeanf.EventSystem;
using UnityEditor;
using UnityEngine;

namespace jeanf.EventSystem
{
    public class SendDecimalEventOnClick : MonoBehaviour
    {
        
        [Header("Broadcasting on:")] [SerializeField]
        private DecimalEventChannelSO TestChannel;

        [Space(20)]
        [SerializeField] private decimal messageToSend = 1;
        public void CallFunction()
        {
            TestChannel.RaiseEvent(messageToSend);
        }
    }
    
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
}

#endif