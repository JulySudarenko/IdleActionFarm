using UnityEngine;

namespace Code.Config
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configs/CharacterConfig", order = 0)]
    public sealed class CharacterConfig : ScriptableObject
    {
        public Transform FemalePrefab;
        public Transform SpawnPoint;
        public Transform FeetCollider;
        public Transform Scythe;

        [SerializeField] private float _speed = 10.0f;
        [SerializeField] private float _height = 1.7f;
        [SerializeField] private float _jumpHeight = 5.0f;

        public float Speed => _speed;

        public float Height => _height;

        public float JumpHeight => _jumpHeight;
    }
}
