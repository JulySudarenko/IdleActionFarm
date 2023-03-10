﻿using System;
using Code.Config;
using Code.Factory;
using Code.Hit;
using Code.Interfaces;
using Code.UserInput;
using UnityEngine;

namespace Code.Character
{
    public class CharacterMoveController : IInitialization, IExecute, IDisposable
    {
        public bool IsWalk { get; private set; }
        private bool IsJump { get; set; }
        private readonly CharacterModel _character;
        private readonly Camera _camera;
        private readonly InputInitialization _input;
        private readonly CharacterConfig _config;
        private readonly TriggerHandler _feetCollider;
        private Vector3 _newVelocity;
        private float _horizontal;
        private float _vertical;
        private bool _isGrounded;

        public CharacterMoveController(CharacterModel characterModel, InputInitialization input, CharacterConfig config,
            Camera camera)
        {
            _input = input;
            _config = config;
            _character = characterModel;
            _camera = camera;
            _feetCollider = characterModel.FeetCollider;
        }

        public void Initialize()
        {
            _input.InputMouseLeft.OnButtonHold += OnMouseButtonHold;
            _input.InputJump.OnButtonDown += OnJumpButtonDown;
            _feetCollider.OnHitEnter += OnGround;
        }

        private void OnMouseButtonHold(bool flag) => IsWalk = flag;
        private void OnJumpButtonDown(bool flag) => IsJump = flag;

        private void OnGround(int id, int selfID) => _isGrounded = true;

        public void FixedExecute(float deltaTime)
        {
            ProcessInputs();
        }

        public void Execute(float deltaTime)
        {
            if (IsJump && _isGrounded)
            {
                _character.Rigidbody.velocity = new Vector3(0, _config.JumpHeight, 0);
                _isGrounded = false;
            }
        }

        private void ProcessInputs()
        {
            if (IsWalk)
            {
                if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit))
                {
                    var direction = (hit.point - _character.Transform.position).normalized;

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
            _input.InputMouseLeft.OnButtonHold -= OnMouseButtonHold;
        }
    }
}
