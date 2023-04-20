using System.Collections.Generic;
using UnityEngine;

namespace jeanf.EventSystem
{
    [CreateAssetMenu(menuName = "Events/Filters/DefaultFilter")]
    public class FilterSO : ScriptableObject
    {
        public List<string> filters;
    }
}
