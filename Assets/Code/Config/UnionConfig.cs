using Code.Assistance;
using Code.UserInput;
using UnityEngine;

namespace Code.Config
{
    [CreateAssetMenu(fileName = "UnionConfig", menuName = "UnionConfig", order = 0)]
    public sealed class UnionConfig : ScriptableObject
    {
        [SerializeField] private string _inputSettingsPath = "Configs/InputConfig";
        [SerializeField] private string _characterSettingsPath = "Configs/CharacterConfig";
        [SerializeField] private string _uiSettingsPath = "Configs/UIConfig";

        private InputConfig _inputConfig;
        private CharacterConfig _characterConfig;
        private UIConfig _uiConfig;

        public InputConfig InputConfig
        {
            get
            {
                if (_inputConfig == null)
                {
                    _inputConfig = Assistant.Load<InputConfig>(_inputSettingsPath);
                }

                return _inputConfig;
            }
        }

        public CharacterConfig CharacterConfig
        {
            get
            {
                if (_characterConfig == null)
                {
                    _characterConfig = Assistant.Load<CharacterConfig>(_characterSettingsPath);
                }

                return _characterConfig;
            }
        }

        public UIConfig UIConfig
        {
            get
            {
                if (_uiConfig == null)
                {
                    _uiConfig = Assistant.Load<UIConfig>(_uiSettingsPath);
                }
                return _uiConfig;
            }
        }
    }
}
