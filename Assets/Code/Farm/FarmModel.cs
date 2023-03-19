using System.Collections.Generic;
using Code.Assistance;
using Code.Config;
using Code.Hit;
using UnityEngine;

namespace Code.Farm
{
    internal class FarmModel
    {
        private readonly Transform _barn;
        private readonly Transform _field;
        public IHit[] WheatTriggers { get; private set; }
        public int[] WheatCollidersIDs { get; private set; }
        public Renderer[] WheatRenderers { get; private set; }
        public Transform[] WheatTransforms{ get; private set; }
        public Collider[] WheatTcColliders{ get; private set; }

        public FarmModel(FarmConfig config)
        {
            Object.Instantiate(config.Environment);
            _barn = Object.Instantiate(config.Barn);
            _field = Object.Instantiate(config.Field);

            GetWheatColliderID();
        }

        public int BarnID => _barn.gameObject.GetInstanceID();
        
        public int[] GetFieldID()
        {
            List<int> fieldSegments = new List<int>();
            
            foreach (Transform segment in _field)
            {
                fieldSegments.Add(segment.gameObject.GetOrAddComponent<Collider>().GetInstanceID());
            }

            return fieldSegments.ToArray();
        }

        private void GetWheatColliderID()
        {
            List<Renderer> allWheatRenderers = new List<Renderer>();
            List<Transform> allWheatTransforms = new List<Transform>();
            List<Collider> allWheatColliders = new List<Collider>();
            List<IHit> allWheatTriggers = new List<IHit>();
            List<int> allWheatIDs = new List<int>();

            foreach (Transform part in _field)
            {
                var partTrigger = part.gameObject.GetOrAddComponent<TriggerDetector>();
                var partCollider = part.gameObject.GetOrAddComponent<Collider>();
                var partID = part.gameObject.GetOrAddComponent<Collider>().GetInstanceID();
                var partRenderer = part.gameObject.GetOrAddComponent<Renderer>();

                allWheatTriggers.Add(partTrigger);
                allWheatIDs.Add(partID);
                allWheatTransforms.Add(part);
                allWheatRenderers.Add(partRenderer);
                allWheatColliders.Add(partCollider);
            }

            WheatTriggers = allWheatTriggers.ToArray();
            WheatCollidersIDs = allWheatIDs.ToArray();
            WheatRenderers = allWheatRenderers.ToArray();
            WheatTransforms = allWheatTransforms.ToArray();
            WheatTcColliders = allWheatColliders.ToArray();
        }
    }
}
