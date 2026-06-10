using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Rebind/ActionRebind Event Channel")]

    public class ActionRebindEventChannelSO : DescriptionBaseSO
    {
        [NonSerialized] private UnityAction<InputAction, int> _onEventRaised;

        public event UnityAction<InputAction, int> OnEventRaised
        {
            add { CanonicalChannelResolver.GetCanonical(this)._onEventRaised += value; }
            remove { CanonicalChannelResolver.GetCanonical(this)._onEventRaised -= value; }
        }

        public void RaiseEvent(InputAction action, string bindingPath)
        {
            int index = 0;
            foreach (var binding in action.bindings)
            {
                InputBinding bindingToRebind = new InputBinding { path = bindingPath };
                if (!bindingToRebind.Matches(binding))
                {
                    continue;
                }
                index = action.GetBindingIndex(bindingToRebind);
            }
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(action, index);
        }

        public void RaiseEvent(InputAction action, int index)
        {
            CanonicalChannelResolver.GetCanonical(this)._onEventRaised?.Invoke(action, index);
        }
    }


}
