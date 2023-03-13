using UnityEngine;

namespace Code.UserInput
{
    [CreateAssetMenu(fileName = "InputSettings", menuName = "Configs/InputSettings", order = 0)]
    public sealed class InputConfig : ScriptableObject
    {
        public string Walk = "Walk";
        public string Work = "Work";
        
    }
}
