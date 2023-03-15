using System;
using Code.Character;
using Code.Config;
using Code.UserInput;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers
{
    public class JoystickController : ICharacterState, IDisposable
    {
        public event Action<CharacterState> ChangeCharacterState;
        public event Action<Vector3> OnJoystickDirectionChange;
        private readonly IUserInputButton _walkInput;
        private readonly UIConfig _config;
        private Transform _joystick;
        private Transform _touchCircle;
        private Vector3 _mousePosition;
        private Vector3 _targetVector;
        private bool _isTouch;

        public JoystickController(UIConfig config, IUserInputButton walkInput)
        {
            _walkInput = walkInput;
            _config = config;
            _walkInput.OnButtonDown += OnMouseButtonDown;
            _walkInput.OnButtonHold += OnMouseButtonHold;
            _walkInput.OnButtonUp += OnMouseButtonUp;
            _walkInput.OnChangeMousePosition += OnChangeMousePosition;

            CreateJoystick();
        }

        private void OnChangeMousePosition(Vector3 position) => _mousePosition = position;

        private void CreateJoystick()
        {
            _joystick = Object.Instantiate(_config.Joystick, _config.Canvas.transform);
            _joystick.gameObject.SetActive(false);
            _touchCircle = Object.Instantiate(_config.TouchCircle, _config.Canvas.transform);
            _touchCircle.gameObject.SetActive(false);
        }

        private void OnMouseButtonDown(bool flag)
        {
            if (flag)
            {
                _joystick.gameObject.SetActive(true);
                _touchCircle.gameObject.SetActive(true);
                _joystick.position = _mousePosition;
                _touchCircle.position = _mousePosition;

                ChangeCharacterState?.Invoke(CharacterState.Walk);
            }
        }

        private void OnMouseButtonHold(bool flag)
        {
            if (flag)
            {
                _targetVector = _mousePosition - _joystick.position;

                if (_targetVector.magnitude < _config.TouchCircleMaxPosition)
                {
                    _touchCircle.position = _mousePosition;
                    OnJoystickDirectionChange?.Invoke(_targetVector);
                }
                else
                {
                    _touchCircle.position = Vector3.Lerp(_touchCircle.position, _joystick.position, 0.2f);
                    OnJoystickDirectionChange?.Invoke(Vector3.zero);
                }
            }
        }

        private void OnMouseButtonUp(bool flag)
        {
            if (flag)
            {
                _joystick.gameObject.SetActive(false);
                _touchCircle.gameObject.SetActive(false);
                
                ChangeCharacterState?.Invoke(CharacterState.Stay);
            }
        }

        public void ChangeState(CharacterState newState)
        {
        }

        public void Dispose()
        {
            _walkInput.OnButtonDown -= OnMouseButtonDown;
            _walkInput.OnButtonHold -= OnMouseButtonHold;
            _walkInput.OnButtonUp -= OnMouseButtonUp;
            _walkInput.OnChangeMousePosition -= OnChangeMousePosition;
        }
    }
}
