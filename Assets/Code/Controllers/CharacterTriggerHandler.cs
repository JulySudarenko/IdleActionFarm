using System;
using Code.Character;
using Code.Hit;

namespace Code.Controllers
{
    internal class CharacterTriggerHandler : ICharacterState, IDisposable
    {
        public event Action<CharacterState> ChangeCharacterState;
        private readonly IHit _triggerDetector;
        private readonly int[] _wheatIDs;

        public CharacterTriggerHandler(IHit trigger, int[] wheatIDs)
        {
            _triggerDetector = trigger;
            _wheatIDs = wheatIDs;
            _triggerDetector.OnHitEnter += OnTrigger;
            _triggerDetector.OnHitExit += OnTriggerLeft;
        }

        private void OnTrigger(int id, int selfId)
        {
            for (int i = 0; i < _wheatIDs.Length; i++)
            {
                if (_wheatIDs[i] == id)
                {
                    ChangeCharacterState?.Invoke(CharacterState.Work);
                }
            }
        }

        private void OnTriggerLeft(int id, int selfId)
        {
            for (int i = 0; i < _wheatIDs.Length; i++)
            {
                if (_wheatIDs[i] == id)
                {
                    ChangeCharacterState?.Invoke(CharacterState.Walk);
                }
            }
        }

        public void ChangeState(CharacterState newState)
        {
        }

        public void Dispose()
        {
            _triggerDetector.OnHitEnter -= OnTrigger;
            _triggerDetector.OnHitExit -= OnTriggerLeft;
        }
    }
}
