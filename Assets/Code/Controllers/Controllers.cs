using System;
using System.Collections.Generic;
using Code.Character;
using Code.Interfaces;

namespace Code.Controllers
{
    public sealed class Controllers : IInitialization, IFixedExecute, IExecute
    {
        private readonly List<IInitialization> _initializeControllers;
        private readonly List<IFixedExecute> _fixedControllers;
        private readonly List<IExecute> _executeControllers;
        private readonly List<ILateExecute> _lateExecuteControllers;

        internal Controllers()
        {
            _initializeControllers = new List<IInitialization>();
            _fixedControllers = new List<IFixedExecute>();
            _executeControllers = new List<IExecute>();
            _lateExecuteControllers = new List<ILateExecute>();
        }

        internal Controllers Add(IController controller)
        {
            if (controller is IInitialization initializeController)
            {
                _initializeControllers.Add(initializeController);
            }

            if (controller is IFixedExecute fixedController)
            {
                _fixedControllers.Add(fixedController);
            }

            if (controller is IExecute executeController)
            {
                _executeControllers.Add(executeController);
            }

            if (controller is ILateExecute lateController)
            {
                _lateExecuteControllers.Add(lateController);
            }

            return this;
        }

        public void Initialize()
        {
            for (var index = 0; index < _initializeControllers.Count; ++index)
            {
                _initializeControllers[index].Initialize();
            }
        }

        public void FixedExecute(float deltaTime)
        {
            for (var index = 0; index < _fixedControllers.Count; ++index)
            {
                _fixedControllers[index].FixedExecute(deltaTime);
            }
        }

        public void Execute(float deltaTime)
        {
            for (var index = 0; index < _executeControllers.Count; ++index)
            {
                _executeControllers[index].Execute(deltaTime);
            }
        }

        public void LateExecute(float deltaTime)
        {
            for (var index = 0; index < _lateExecuteControllers.Count; ++index)
            {
                _lateExecuteControllers[index].LateExecute(deltaTime);
            }
        }
    }
}
