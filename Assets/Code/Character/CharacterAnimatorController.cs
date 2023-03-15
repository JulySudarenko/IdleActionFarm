using System;
using Code.Interfaces;
using UnityEngine;

namespace Code.Character
{
    internal class CharacterAnimatorController : ICharacterState// IExecute, 
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
        //
        // public void Execute(float deltaTime)
        // {
        //     // switch (_state)
        //     // {
        //     //     case CharacterState.Stay:
        //     //         _animator.SetTrigger(Stay);
        //     //         _animator.SetBool(Work, false);
        //     //         break;
        //     //     case CharacterState.Walk:
        //     //         _animator.SetTrigger(Walk);
        //     //         _animator.SetBool(Work, false);
        //     //         break;
        //     //     case CharacterState.Work:
        //     //         _animator.SetBool(Work, true);
        //     //         break;
        //     //     default:
        //     //         throw new ArgumentOutOfRangeException();
        //     // }
        //     // if (_moveController.IsWalk)
        //     // {
        //     //     _character.Animator.SetTrigger(Walk);
        //     // }
        //     // else
        //     // {
        //     //     _character.Animator.SetTrigger(Stay);
        //     // }
        //
        //     // if ()
        //     // {
        //     //     _character.Animator.SetBool("Work", true);
        //     // }
        //     // else
        //     // {
        //     //     _character.Animator.SetBool("Work", false);
        //     // }
        // }


        public void ChangeState(CharacterState newState)
        {
            _state = newState;
            
            switch (_state)
            {
                case CharacterState.Stay:
                    _animator.SetTrigger(Stay);
                    _animator.SetBool(Work, false);
                    break;
                case CharacterState.Walk:
                    _animator.SetTrigger(Walk);
                    _animator.SetBool(Work, false);
                    break;
                case CharacterState.Work:
                    _animator.SetBool(Work, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
