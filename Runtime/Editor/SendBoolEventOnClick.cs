#if UNITY_EDITOR

using jeanf.EventSystem;
using UnityEditor;
using UnityEngine;

namespace uvs.tools
{
    public class BoolEventOnClick : MonoBehaviour
    {
        
        [Header("Broadcasting on:")] [SerializeField]
        private BoolEventChannelSO TestChannel;
        
        [Space(20)]
        [SerializeField] private bool valueToSend = false;
        
        public void CallFunction()
        {
            TestChannel.RaiseEvent(valueToSend);
        }
    }
    
    [CustomEditor(typeof(BoolEventOnClick))]
    public class BoolEventOnClickEditor : Editor {
        override public void  OnInspectorGUI () {
            DrawDefaultInspector();
            var eventToSend = (BoolEventOnClick)target;
            if(GUILayout.Button("Send bool", GUILayout.Height(30))) {
                eventToSend.CallFunction(); // how do i call this?
            }
            GUILayout.Space(10);
        }
    }
}

#endif