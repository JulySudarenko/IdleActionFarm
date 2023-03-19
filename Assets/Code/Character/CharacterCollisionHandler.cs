using System;
using Code.Hit;
using Code.Interfaces;

namespace Code.Character
{
    internal class CharacterCollisionHandler : IInitialization, IDisposable
    {
        public event Action BarnDetected;
        private readonly IHit _colliderDetector;
        private readonly int _barnID;

        public CharacterCollisionHandler(IHit character, int barnID)
        {
            _colliderDetector = character;
            _barnID = barnID;
        }
        
        public void Initialize()
        {
            _colliderDetector.OnHitEnter += OnCollision;
        }

        private void OnCollision(int id, int selfId)
        {
            if (_barnID == id)
            {
                BarnDetected?.Invoke();
            }
        }

        public void Dispose()
        {
            _colliderDetector.OnHitEnter -= OnCollision;
        }
    }
}
