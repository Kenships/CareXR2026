using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class BindToTransform : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Update()
        {
            if (!target) return;
            
            transform.position = target.position;
        }
    }
}
