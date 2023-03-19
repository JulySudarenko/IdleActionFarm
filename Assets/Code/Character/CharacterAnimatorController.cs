using System;
using UnityEngine;

namespace Code.Character
{
    internal class CharacterAnimatorController : ICharacterState 
    {
        public event Action<CharacterState> ChangeCharacterState;
        private readonly Animator _animator;
        private readonly CharacterMoveController _moveController;
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Stay = Animator.StringToHash("Stay");
        private static readonly int Work = Animator.StringToHash("Work");
        private CharacterState _state;

        public CharacterAnimatorController(Animator animator)
        {
            _animator = animator;
        }

        public void ChangeState(CharacterState newState)
        {
            switch (newState)
            {
                case CharacterState.Stay:
                    _animator.SetTrigger(Stay);
                    break;
                case CharacterState.Walk:
                    _animator.SetTrigger(Walk);
                     break;
                case CharacterState.Work:
                    _animator.SetBool(Work, true);
                    // Debug.Log("Work");
                    break;
                case CharacterState.StopWork:
                    _animator.SetBool(Work, false);
                    // Debug.Log("StopWork");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _state = newState;
        }
    }
}
