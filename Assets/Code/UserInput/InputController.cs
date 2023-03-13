using Code.Interfaces;
using UnityEngine;

namespace Code.UserInput
{
    internal sealed class InputController : IExecute
    {
        // private readonly IUserInputProxy _inputHorizontal;
        // private readonly IUserInputProxy _inputVertical;
        private readonly IUserInputButtonProxy _inputWalk;
        // private readonly IUserInputButtonProxy _inputJump;
        private readonly IUserInputButtonProxy _inputWork;
        
        public InputController(InputInitialization input)
        {
            // _inputHorizontal = input.InputHorizontal;
            // _inputVertical = input.InputVertical;
            _inputWalk = input.InputWalk;
            // _inputJump = input.InputJump;
            _inputWork = input.InputWork;
        }

        public void Execute(float deltaTime)
        {
            // _inputHorizontal.GetAxis();
            // _inputVertical.GetAxis();
            _inputWalk.GetButtonDown();
            _inputWalk.GetButtonHold();
            _inputWalk.GetButtonUp();
            _inputWork.GetButtonDown();
            _inputWork.GetButtonHold();
            _inputWork.GetButtonUp();
            

            // _inputJump.GetButtonDown();
            // _inputJump.GetButtonHold();
            // _inputJump.GetButtonUp();
        }
    }
}
