using Code.Assistance;
using Code.Hit;
using UnityEngine;

namespace Code.Instruments
{
    internal class ScytheModel : MonoBehaviour
    {
        public Transform Scythe { get; }
        public int ScytheColliderID { get; }
        public IHit Hit { get; }

        public ScytheModel(Transform character)
        {
            var scytheView = character.GetComponentInChildren<ScytheView>();
            Scythe = scytheView.transform;
            ScytheColliderID = Scythe.GetComponent<Collider>().GetInstanceID();
            Hit = Scythe.gameObject.GetOrAddComponent<TriggerDetector>();
        }
    }
}
