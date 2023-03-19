using Code.Character;
using Code.Config;
using Code.Farm;
using Code.Hit;
using Code.Instruments;
using Code.Timer;
using Code.UserInput;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.Tilemaps;

namespace Code.Controllers
{
    public class LaunchingController : MonoBehaviour
    {
        [SerializeField] private UnionConfig _unionConfig;

        private Controllers _controllers;

        private void Awake()
        {
            _controllers = new Controllers();
            var input = new InputInitialization(_unionConfig.InputConfig);
            var inputController = new InputController(input);

            var farm = new FarmModel(_unionConfig.FarmConfig);
            var character = new CharacterModel(_unionConfig.CharacterConfig);
            var scythe = new ScytheModel(character.Transform);
            var characterController = new CharacterStateController(character, _unionConfig, input.InputWalk, farm);
            var scytheController = new ScytheController(scythe, characterController, farm.WheatCollidersIDs);

            var wheatCutController = new WheatCutController(farm, scytheController, _unionConfig.FarmConfig);

            _controllers.Add(new TimeRemainingController());
            _controllers.Add(inputController);
            _controllers.Add(scytheController);
            _controllers.Add(wheatCutController);
            _controllers.Add(characterController);
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
