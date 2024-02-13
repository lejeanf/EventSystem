
#if UNITY_EDITOR

using jeanf.EventSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
namespace jeanf.EventSystem
{
    public class SendActionRebindEventOnClick : MonoBehaviour
    {

        [Header("Broadcasting on:")]
        [SerializeField]
        private ActionRebindEventChannelSO TestChannel;

        [Space(20)]
        [SerializeField] private InputActionReference actionToSend;
        [SerializeField] private string bindingPath;
        public void CallFunction()
        {
            TestChannel.RaiseEvent(actionToSend, bindingPath);
        }
    }

    [CustomEditor(typeof(SendActionRebindEventOnClick))]
    public class ActionRebindEventOnClickEditor : Editor
    {
        override public void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var eventToSend = (SendActionRebindEventOnClick)target;
            if (GUILayout.Button("Send ActionInt", GUILayout.Height(30)))
            {
                eventToSend.CallFunction(); // how do i call this?
            }
            GUILayout.Space(10);
        }
    }
}

#endif

