using System;
using System.Collections.Generic;
using _Project.Scripts.Util.ExtensionMethods;
using _Project.Scripts.Util.Timer.Timers;
using UnityEngine;

namespace _Project.Scripts
{
    public class PunchController : MonoBehaviour
    {
        
        [SerializeField] private float threshold = 0.2f;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private ColliderEnterEventTrigger close;
        [SerializeField] private ColliderEnterEventTrigger far;
        [SerializeField] private float chargeTime = 0.5f;

        [SerializeField] private Bullet bullet;
        
        private CountdownTimer _timer;
        
        List<Collider> _close = new();
        private Collider[] _buffer = new Collider[100];

        private void Start()
        {
            close.OnEnter += OnCloseEnter;
            
            far.OnExit += OnFarExit;
        }

        private void OnFarExit(Collider obj)
        {
            if (!obj.IsOnLayer(layerMask) || !_close.Contains(obj))
            {
                return;
            }

            Shoot(obj.transform);
            _close.Remove(obj);
        }

        private void OnCloseEnter(Collider obj)
        {
            if (!obj.IsOnLayer(layerMask))
            {
                return;
            }
            Debug.Log("Close");
            _close.Add(obj);
            
            if (_close.Count == 2)
            {
                
            }
        }
        
        private void Shoot(Transform objTransform)
        {
            int count = Physics.OverlapSphereNonAlloc(objTransform.position, 100f, _buffer, targetMask);

            Transform target = null;
            
            if (count > 0)
            {
                float bestDot = float.MinValue;
                int bestIndex = -1;

                for (int i = 0; i < count; i++)
                {
                    var dot = Vector3.Dot(objTransform.forward, (_buffer[i].transform.position - objTransform.position).normalized);
                    
                    Debug.Log(dot);
                    
                    if (dot > bestDot && dot > threshold)
                    {
                        bestDot = dot;
                        bestIndex = i;
                    }
                }
                
                target = bestIndex == -1 ? null : _buffer[bestIndex].transform;
            }
            
            
            Bullet b = Instantiate(bullet, objTransform.position, objTransform.rotation);
            b.Init(target);
        }
    }
}
