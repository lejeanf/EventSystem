#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace jeanf.EventSystem
{
    public class BoolEventOnClick : MonoBehaviour
    {
#if UNITY_EDITOR
        [Header("Broadcasting on:")] [SerializeField]
        private BoolEventChannelSO TestChannel;
        
        [Space(20)]
        [SerializeField] private bool valueToSend = false;
        
        public void CallFunction()
        {
            TestChannel.RaiseEvent(valueToSend);
        }
        #endif
    }
    
#if UNITY_EDITOR
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
    #endif
}
