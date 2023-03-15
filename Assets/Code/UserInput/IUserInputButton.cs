using System;
using UnityEngine;

namespace Code.UserInput
{
    public interface IUserInputButton
    {
        event Action<Vector3> OnChangeMousePosition;
        event Action<bool> OnButtonDown;
        event Action<bool> OnButtonHold;
        event Action<bool> OnButtonUp;

        void GetButtonDown();
        void GetButtonHold();
        void GetButtonUp();
    }
}
