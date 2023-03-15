using System;
using Code.Assistance;
using Code.Character;
using Code.Hit;
using UnityEngine;

namespace Code.Instruments
{
    internal class ScytheController : IDisposable
    {
        private readonly ICharacterState _characterState;
        private readonly Transform _scythe;
        private IHit _hit;
        private CharacterState _state;

        public ScytheController(Transform character, ICharacterState state)
        {
            var scytheView = character.GetComponentInChildren<ScytheView>();
            _scythe = scytheView.transform;
            _hit = _scythe.gameObject.GetOrAddComponent<CollisionDetector>();
            _characterState = state;
            _characterState.ChangeCharacterState += TakeScythe;
            _scythe.gameObject.SetActive(false);
        }

        private void TakeScythe(CharacterState newState)
        {
            switch (newState)
            {
                case CharacterState.Stay:
                    if (_state == CharacterState.Work)
                    {
                        _scythe.gameObject.SetActive(false);
                        _state = newState;
                    }
                    break;
                case CharacterState.Walk:
                    if (_state == CharacterState.Work)
                    {
                        _scythe.gameObject.SetActive(false);
                        _state = newState;
                    }
                    break;
                case CharacterState.Work:
                    if (_state == CharacterState.Walk || _state == CharacterState.Stay)
                    {
                        _scythe.gameObject.SetActive(true);
                        _state = newState;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
            _state = newState;
        }

        public void Dispose()
        {
            _characterState.ChangeCharacterState -= TakeScythe;
        }
    }
}
