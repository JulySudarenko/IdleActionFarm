using System;
using Code.Config;
using Code.Controllers;
using Code.Hit;
using Code.Interfaces;
using Code.UserInput;
using UnityEngine;

namespace Code.Character
{
    public class CharacterMoveController : IInitialization, IFixedExecute, IDisposable
    {
        public bool IsWalk { get; private set; }

        // private bool IsJump { get; set; }
        private readonly JoystickController _joystick;
        private readonly CharacterModel _character;
        private readonly Camera _camera;
        private readonly IUserInputButtonProxy _walkInput;
        private readonly CharacterConfig _config;
        private readonly TriggerHandler _feetCollider;
        private Vector3 _newVelocity;
        private Vector3 _firstTouchPosition;
        private Vector3 _mousePosition;
        private float _horizontal;

        private float _vertical;
        //private bool _isGrounded;

        public CharacterMoveController(CharacterModel characterModel, JoystickController joystick,
            IUserInputButtonProxy walkInput,
            CharacterConfig config, Camera camera)
        {
            _joystick = joystick;
            _walkInput = walkInput;
            _config = config;
            _character = characterModel;
            _camera = camera;
            
            _joystick.OnJoystickDirectionChange += OnPositionChange;
        }

        public void Initialize()
        {
            _joystick.OnJoystickDirectionChange += OnPositionChange;
            _walkInput.OnButtonDown += OnMouseButtonDown;
            _walkInput.OnButtonHold += OnMouseButtonHold;
            //_walkInput.OnButtonUp += OnMouseButtonUp;
            _walkInput.OnChangeMousePosition += OnChangeMousePosition;
        }

        private void OnMouseButtonDown(bool flag)
        {
            _firstTouchPosition = _mousePosition;
        }

        private void OnPositionChange(Vector3 position)
        {
            var direction = (position + _character.Transform.position);

            _horizontal = direction.x;
            _vertical = direction.z;

            Vector3 relativePos = _newVelocity;
            var angle = Vector3.Angle(Vector3.forward, relativePos);
            var axis = Vector3.Cross(Vector3.forward, relativePos);
            _character.Transform.rotation = Quaternion.AngleAxis(angle, axis);

            _newVelocity.Set(_horizontal, 0.0f, _vertical);
            _character.Rigidbody.AddForce(_newVelocity * _config.Speed);
        }

        private void OnMouseButtonHold(bool flag) => IsWalk = flag;

        private void OnChangeMousePosition(Vector3 position) => _mousePosition = position;

        //private void OnGround(int id, int selfID) => _isGrounded = true;

        public void FixedExecute(float deltaTime)
        {
            //ProcessInputs();
        }

        public void Execute(float deltaTime)
        {
            // if (IsJump && _isGrounded)
            // {
            //     _character.Rigidbody.velocity = new Vector3(0, _config.JumpHeight, 0);
            //     _isGrounded = false;
            // }
        }

        private void ProcessInputs()
        {
            if (IsWalk)
            {
                if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit))
                {
                    //var direction = (hit.point - _character.Transform.position).normalized;
                    var direction = (_mousePosition - _character.Transform.position).normalized;

                    _horizontal = direction.x;
                    _vertical = direction.z;

                    Vector3 relativePos = _newVelocity;
                    var angle = Vector3.Angle(Vector3.forward, relativePos);
                    var axis = Vector3.Cross(Vector3.forward, relativePos);
                    _character.Transform.rotation = Quaternion.AngleAxis(angle, axis);

                    _newVelocity.Set(_horizontal, 0.0f, _vertical);
                    _character.Rigidbody.AddForce(_newVelocity * _config.Speed);
                }
            }
        }

        public void Dispose()
        {
            _joystick.OnJoystickDirectionChange -= OnPositionChange;
        }
    }
}
