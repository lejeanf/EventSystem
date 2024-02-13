#if UNITY_EDITOR

using jeanf.EventSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
namespace jeanf.EventSystem
{
    public class SendInputActionIntEventOnClick : MonoBehaviour
    {

        [Header("Broadcasting on:")]
        [SerializeField]
        private InputActionIntEventChannelSO TestChannel;

        [Space(20)]
        [SerializeField] private InputActionReference actionToSend;
        [SerializeField] private int intToSend;
        public void CallFunction()
        {
            TestChannel.RaiseEvent(actionToSend, intToSend);
        }
    }

    [CustomEditor(typeof(SendInputActionIntEventOnClick))]
    public class InputActionIntEventOnClickEditor : Editor
    {
        override public void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var eventToSend = (SendInputActionIntEventOnClick)target;
            if (GUILayout.Button("Send ActionInt", GUILayout.Height(30)))
            {
                eventToSend.CallFunction(); // how do i call this?
            }
            GUILayout.Space(10);
        }
    }
}

#endif
