using Code.Interfaces;
using UnityEngine;

namespace Code.UserInput
{
    internal sealed class InputController : IExecute
    {
        private readonly IUserInputButton _inputWalk;
        private readonly IUserInputButton _inputWork;
        
        public InputController(InputInitialization input)
        {
            _inputWalk = input.InputWalk;
            _inputWork = input.InputWork;
        }

        public void Execute(float deltaTime)
        {
            _inputWalk.GetButtonDown();
            _inputWalk.GetButtonHold();
            _inputWalk.GetButtonUp();
            _inputWork.GetButtonDown();
            _inputWork.GetButtonHold();
            _inputWork.GetButtonUp();
        }
    }
}
