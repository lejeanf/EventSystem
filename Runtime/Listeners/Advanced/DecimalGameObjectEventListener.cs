using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// To use a generic UnityEvent type you must override the generic type.
/// </summary>
namespace jeanf.EventSystem
{
    [System.Serializable]
    public class DecimalGameObjectEvent : UnityEvent<decimal, GameObject>
    {

    }

    /// <summary>
    /// A flexible handler for int events in the form of a MonoBehaviour. Responses can be connected directly from the Unity Inspector.
    /// </summary>
    public class DecimalGameObjectEventListener : MonoBehaviour, IDebugBehaviour
    {
        public bool isDebug
        { 
            get => _isDebug;
            set => _isDebug = value; 
        }
        [SerializeField] private bool _isDebug = false;
		
        [SerializeField] private DecimalGameObjectEventChannelSO _channel = default;

        public DecimalGameObjectEvent OnEventRaised;

        private void OnEnable()
        {
            if (_channel != null)
                _channel.OnEventRaised += Respond;
        }

        private void OnDisable()
        {
            if (_channel != null)
                _channel.OnEventRaised -= Respond;
        }

        private void Respond(decimal nb, GameObject value)
        {
            OnEventRaised?.Invoke(nb, value);
            if(isDebug) Debug.Log($" decimal-gameObject event raised: <{nb},{value}>");
        }
    }
}