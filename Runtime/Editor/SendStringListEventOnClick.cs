#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace jeanf.EventSystem
{
    public class SendStringListEventOnClick : MonoBehaviour
    {
        [Header("Broadcasting On:")]
        [SerializeField] private StringListEventChannelSO testChannel;

        [Space(20)]
        [SerializeField] private List<string> valueToSend;

        public void CallFunction()
        {
            testChannel.RaiseEvent(valueToSend);
        }
    }

    [CustomEditor(typeof(SendStringListEventOnClick))]
    public class SendStringEventOnClickEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var eventToSend = (BoolEventOnClick)target;
            if(GUILayout.Button("Send String List", GUILayout.Height(30)))
            {
                eventToSend.CallFunction();
            }
            GUILayout.Space(10);
        }
    }
}
#endif

