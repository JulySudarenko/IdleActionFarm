using Code.Character;
using Code.Config;
using Code.Timer;
using Code.UserInput;
using UnityEngine;

namespace Code.Controllers
{
    public class LaunchingController : MonoBehaviour
    {
        [SerializeField] private UnionConfig _unionConfig;

        private Controllers _controllers;

        private void Awake()
        {
            var camera = Camera.main;
            _controllers = new Controllers();
            var input = new InputInitialization(_unionConfig.InputConfig);
            var inputController = new InputController(input);

            var character = new CharacterModel(_unionConfig.CharacterConfig);
            var joystick = new JoystickController(_unionConfig.UIConfig, input.InputWalk);
            
            var moveController =
                new CharacterMoveController(character, joystick, input.InputWalk, _unionConfig.CharacterConfig, camera);
            var animatorController = new CharacterAnimatorController(character, moveController);

            _controllers.Add(inputController);
            _controllers.Add(moveController);
            _controllers.Add(animatorController);

            _controllers.Add(new TimeRemainingController());
        }

        private void Start()
        {
            _controllers.Initialize();
        }

        private void Update()
        {
            _controllers.Execute(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _controllers.FixedExecute(Time.fixedDeltaTime);
        }

        private void LateUpdate()
        {
            _controllers.LateExecute(Time.deltaTime);
        }
    }
}
