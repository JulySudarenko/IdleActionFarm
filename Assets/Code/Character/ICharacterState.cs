using System;

namespace Code.Character
{
    internal interface ICharacterState
    {
        event Action<CharacterState> ChangeCharacterState;
        void ChangeState(CharacterState newState);
    }
}
