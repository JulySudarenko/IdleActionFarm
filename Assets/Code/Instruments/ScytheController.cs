using System;
using Code.Character;
using Code.Hit;
using Code.Interfaces;
using UnityEngine;

namespace Code.Instruments
{
    internal class ScytheController : IInitialization, IDisposable
    {
        public event Action<int> CutWheat;
        private readonly int[] _wheatIds;
        private readonly ICharacterState _characterState;
        private readonly IHit _hitScythe;
        private readonly Transform _scythe;

        public ScytheController(ScytheModel scythe, ICharacterState state, int[] wheatIds)
        {
            _wheatIds = wheatIds;
            _scythe = scythe.Scythe;
            _characterState = state;
            _hitScythe = scythe.Hit;
        }

        public void Initialize()
        {
            _characterState.ChangeCharacterState += TakeScythe;
            _hitScythe.OnHitEnter += WheatHit;
            _scythe.gameObject.SetActive(false);
        }

        private void WheatHit(int id, int selfId)
        {
            for (int i = 0; i < _wheatIds.Length; i++)
            {
                if (_wheatIds[i] == id)
                {
                    CutWheat?.Invoke(id);
                }
            }
        }

        private void TakeScythe(CharacterState newState)
        {
            switch (newState)
            {
                case CharacterState.Stay:
                    break;
                case CharacterState.Walk:
                    break;
                case CharacterState.Work:
                    _scythe.gameObject.SetActive(true);
                    break;
                case CharacterState.StopWork:
                    _scythe.gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        public void Dispose()
        {
            _characterState.ChangeCharacterState -= TakeScythe;
        }
    }
}
