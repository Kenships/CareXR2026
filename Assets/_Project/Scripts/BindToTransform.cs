using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

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
