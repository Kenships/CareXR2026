using System;
using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(SphereCollider))]
    public class ColliderEnterEventTrigger : MonoBehaviour
    {
        public Action<Collider> OnEnter;
        public Action<Collider> OnExit;
        
        private SphereCollider _collider;
        
        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
            _collider.isTrigger = true;
        }

        public void SetReach(float reach)
        {
            _collider.radius = reach;
        }

        private void OnTriggerEnter(Collider other)
        {
            OnEnter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnExit?.Invoke(other);
        }
    }
}
