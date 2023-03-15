using System.Collections.Generic;
using Code.Assistance;
using Code.Config;
using Code.Hit;
using UnityEngine;

namespace Code.Farm
{
    internal class FarmModel
    {
        private FarmConfig _config;

        private readonly Transform _barn;
        private Transform _field;
        
        public FarmModel(FarmConfig config)
        {
            _config = config;

            Object.Instantiate(_config.Environment);
            _barn = Object.Instantiate(_config.Barn);
            _field = Object.Instantiate(_config.Field);
           
        }

        public int BarnID => _barn.gameObject.GetInstanceID();

        public int[] GetFieldID()
        {
            List<int> _fieldSegments = new List<int>();
            
            foreach (Transform segment in _field)
            {
                _fieldSegments.Add(segment.gameObject.GetOrAddComponent<Collider>().GetInstanceID());
            }

            return _fieldSegments.ToArray();
        }
    }
}
