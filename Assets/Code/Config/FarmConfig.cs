using UnityEngine;

namespace Code.Config
{
    [CreateAssetMenu(fileName = "FarmConfig", menuName = "Configs/FarmConfig", order = 0)]
    public sealed class FarmConfig : ScriptableObject
    {
        public Transform Environment;
        public Transform Barn;
        public Transform Field;

        [SerializeField] private float _wheatGrowthTime = 10.0f;
        [SerializeField] private float _wheatMinHeight = 0.1f;
        [SerializeField] private float _wheatMaxHeight = 0.7f;
        [SerializeField] private int _wheatGrowthStages = 2;

        public int WheatGrowthStages => _wheatGrowthStages;

        public float WheatMinHeight => _wheatMinHeight;

        public float WheatMaxHeight => _wheatMaxHeight;

        public float WheatGrowthTime => _wheatGrowthTime;
    }
}
