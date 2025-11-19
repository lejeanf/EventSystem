#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;
namespace jeanf.EventSystem
{
    public class SendInputEventOnClick : MonoBehaviour
    {
        #if UNITY_EDITOR
        [Header("Broadcasting on:")]
        [SerializeField]
        private InputActionEventChannelSO TestChannel;

        [Space(20)]
        [SerializeField] private InputAction messageToSend;
        public void CallFunction()
        {
            TestChannel.RaiseEvent(messageToSend);
        }
        #endif
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(SendInputEventOnClick))]
    public class InputActionEventOnClickEditor : Editor
    {
        override public void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var eventToSend = (SendInputEventOnClick)target;
            if (GUILayout.Button("Send Action", GUILayout.Height(30)))
            {
                eventToSend.CallFunction(); // how do i call this?
            }
            GUILayout.Space(10);
        }
    }
    #endif
}