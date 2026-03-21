using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class BindToTransform : MonoBehaviour
    {
        [SerializeField] private bool bindRotation;
        [SerializeField] private Transform target;
        
        [SerializeField] private Vector3 offset;

        private void Update()
        {
            if (!target) return;
            
            transform.position = target.position + offset;
            
            if (bindRotation)
                transform.rotation = target.rotation;
        }
    }
}
