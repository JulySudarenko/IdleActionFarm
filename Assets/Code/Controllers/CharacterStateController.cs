using System;
using System.Collections.Generic;
using Code.Character;
using Code.Config;
using Code.Farm;
using Code.Interfaces;
using Code.UserInput;

namespace Code.Controllers
{
    internal class CharacterStateController : IInitialization, ICharacterState, IDisposable // IExecute,
    {
        public event Action<CharacterState> ChangeCharacterState;
        private readonly List<ICharacterState> _stateControllersList = new List<ICharacterState>();
        private readonly JoystickController _joystick;
        private readonly CharacterMoveController _moveController;
        private readonly CharacterAnimatorController _animatorController;
        private readonly CharacterCollisionHandler _collisionHandler;
        private readonly CharacterTriggerHandler _triggerHandler;
        private CharacterState _state;

        public CharacterStateController(CharacterModel character, UnionConfig config, IUserInputButton input,
            FarmModel farm)
        {
            _joystick = new JoystickController(config.UIConfig, input);
            _moveController = new CharacterMoveController(character, _joystick, config.CharacterConfig);
            _animatorController = new CharacterAnimatorController(character.Animator);
            _collisionHandler = new CharacterCollisionHandler(character.CollisionDetector, farm.BarnID);
            _triggerHandler = new CharacterTriggerHandler(character.TriggerDetector, farm.GetFieldID());
        }

        public void Initialize()
        {
            _stateControllersList.Add(_joystick);
            _stateControllersList.Add(_animatorController);
            _stateControllersList.Add(_triggerHandler);

            for (int i = 0; i < _stateControllersList.Count; i++)
            {
                _stateControllersList[i].ChangeCharacterState += ChangeState;
            }

            _moveController.Initialize();
        }

        // public void Execute(float deltaTime)
        // {
        //     _animatorController.Execute(Time.deltaTime);
        // }

        public void ChangeState(CharacterState newState)
        {
            for (int i = 0; i < _stateControllersList.Count; i++)
            {
                _stateControllersList[i].ChangeState(newState);
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < _stateControllersList.Count; i++)
            {
                _stateControllersList[i].ChangeCharacterState -= ChangeState;
            }
        }
    }
}
