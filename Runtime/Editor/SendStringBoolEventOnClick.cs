#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace jeanf.EventSystem
{
    public class SendStringBoolEventOnClick : MonoBehaviour
    {
        #if UNITY_EDITOR
        [Header("Broadcasting on:")]
        [SerializeField]
        private StringBoolEventChannelSO TestChannel;

        [Space(20)]
        [SerializeField] private bool valueToSend = false;
        [SerializeField] private string textToSend;

        public void CallFunction()
        {
            TestChannel.RaiseEvent(textToSend, valueToSend);
        }
        #endif
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(SendStringBoolEventOnClick))]
    public class StringBoolEventOnClickEditor : Editor
    {
        override public void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var eventToSend = (SendStringBoolEventOnClick)target;
            if (GUILayout.Button("Send bool", GUILayout.Height(30)))
            {
                eventToSend.CallFunction(); // how do i call this?
            }
            GUILayout.Space(10);
        }
    }
    #endif
}
