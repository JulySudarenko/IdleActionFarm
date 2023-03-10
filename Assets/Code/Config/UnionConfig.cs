using Code.Assistance;
using Code.UserInput;
using UnityEngine;

namespace Code.Config
{
    [CreateAssetMenu(fileName = "UnionResourcesConfig", menuName = "UnionResourcesConfig", order = 0)]
    public sealed class UnionConfig : ScriptableObject
    {
        [SerializeField] private string _inputSettingsPath = "InputConfig";
        [SerializeField] private string _characterSettingsPath = "CharacterConfig";

        private InputConfig _inputConfig;

        private CharacterConfig _characterConfig;

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

        // [SerializeField] private string _resourcesFolder = "ResConfigs";
        // [SerializeField] private string _questFolder = "QuestConfigs";
        // [SerializeField] private string _placeFolder = "PlacesConfigs";
        //
        // private ResourcesConfig[] _resourcesConfigs;
        // private QuestNpcConfig[] _questNpcConfigs;
        // private BuildingPlacesConfig[] _buildingPlacesConfigs;
        //
        // public ResourcesConfig[] AllResourcesConfigs
        // {
        //     get
        //     {
        //         _resourcesConfigs = Assistant.LoadAll<ResourcesConfig>(_resourcesFolder);
        //         return _resourcesConfigs;
        //     }
        // }
        //
        // public QuestNpcConfig[] AllQuestNpcConfigs
        // {
        //     get
        //     {
        //         _questNpcConfigs = Assistant.LoadAll<QuestNpcConfig>(_questFolder);
        //         return _questNpcConfigs;
        //     }
        // }
        //
        // public BuildingPlacesConfig[] AllBuildingPlacesConfigs
        // {
        //     get
        //     {
        //         _buildingPlacesConfigs = Assistant.LoadAll<BuildingPlacesConfig>(_placeFolder);
        //         return _buildingPlacesConfigs;
        //     }
        // }
    }
}
