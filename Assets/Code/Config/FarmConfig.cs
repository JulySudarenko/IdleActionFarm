using UnityEngine;

namespace Code.Config
{
    [CreateAssetMenu(fileName = "FarmConfig", menuName = "Configs/FarmConfig", order = 0)]
    public sealed class FarmConfig : ScriptableObject
    {
        public Transform Environment;
        public Transform Barn;
        public Transform Field;
    }
}
