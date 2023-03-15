
namespace Code.UserInput
{
    public sealed class InputInitialization
    {
        private readonly IUserInputButton _inputWork;
        private readonly IUserInputButton _inputWalk;


        public InputInitialization(InputConfig config)
        {
            _inputWork = new ButtonInput(config.Work);
            _inputWalk  = new ButtonInput(config.Walk);
        }

        public IUserInputButton InputWork => _inputWork;
        public IUserInputButton InputWalk => _inputWalk;
    }
}
