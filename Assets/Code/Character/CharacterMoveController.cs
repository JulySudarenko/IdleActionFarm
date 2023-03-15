using System;
using Code.Config;
using Code.Controllers;
using Code.Hit;
using Code.Interfaces;
using UnityEngine;

namespace Code.Character
{
    public class CharacterMoveController : IInitialization, IDisposable
    {
        private readonly JoystickController _joystick;
        private readonly CharacterModel _character;
        private readonly CharacterConfig _config;
        private readonly TriggerDetector _feetCollider;
        private Vector3 _newVelocity;
        private Vector3 _firstTouchPosition;
        private Vector3 _mousePosition;
        private float _horizontal;
        private float _vertical;

        public CharacterMoveController(CharacterModel characterModel, JoystickController joystick,
            CharacterConfig config)
        {
            _joystick = joystick;
            _config = config;
            _character = characterModel;
        }

        public void Initialize()
        {
            _joystick.OnJoystickDirectionChange += OnPositionChange;
        }

        private void OnPositionChange(Vector3 position)
        {
            var direction = position.normalized;

            _horizontal = direction.x;
            _vertical = direction.y;

            Vector3 relativePos = _newVelocity;
            var angle = Vector3.Angle(Vector3.forward, relativePos);
            var axis = Vector3.Cross(Vector3.forward, relativePos);
            _character.Transform.rotation = Quaternion.AngleAxis(angle, axis);

            _newVelocity.Set(_horizontal, 0.0f, _vertical);
            _character.Rigidbody.AddForce(_newVelocity * _config.Speed);
        }

        public void Dispose()
        {
            _joystick.OnJoystickDirectionChange -= OnPositionChange;
        }
    }
}
