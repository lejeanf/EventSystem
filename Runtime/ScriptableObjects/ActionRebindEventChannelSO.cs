using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Rebind/ActionRebind Event Channel")]

    public class ActionRebindEventChannelSO : DescriptionBaseSO
    {
        public UnityAction<InputAction, int> OnEventRaised;

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
            OnEventRaised?.Invoke(action, index);
        }

        public void RaiseEvent(InputAction action, int index)
        {
            
            OnEventRaised?.Invoke(action, index);
        }
    }


}

