using System;
using System.Collections.Generic;
using _Project.Scripts.Util.ExtensionMethods;
using _Project.Scripts.Util.Timer.Timers;
using UnityEngine;

namespace _Project.Scripts
{
    public class PunchController : MonoBehaviour
    {
        [SerializeField] private ReachCalibrationService reachCalibrationService;
        
        [SerializeField] private GameObject superAttackPrefab;
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
            _timer = null;
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
                _timer = new CountdownTimer(chargeTime);
                _timer.Start();
            }
        }

        private void Update()
        {
            far.SetReach(Mathf.Min(reachCalibrationService.MaxLeftReach, reachCalibrationService.MaxRightReach) * 0.9f);
            close.SetReach(Mathf.Min(reachCalibrationService.MaxLeftReach, reachCalibrationService.MaxRightReach) * 0.5f);
            
            if (_timer is { IsRunning: true })
            {
                Debug.Log(100 - _timer.Progress * 100f);
            }
        }

        private void Shoot(Transform objTransform)
        {
            if (_timer is { IsFinished: true })
            {
                _timer = null;
                SuperAttack();
                _close.Clear();
                return;
            }
            
            int count = Physics.OverlapSphereNonAlloc(objTransform.position, 100f, _buffer, targetMask);

            Transform target = null;
            
            if (count > 0)
            {
                float bestDot = float.MinValue;
                int bestIndex = -1;

                for (int i = 0; i < count; i++)
                {
                    var dot = Vector3.Dot(objTransform.forward, (_buffer[i].transform.position - objTransform.position).normalized);
                    
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

        private void SuperAttack()
        {
            Instantiate(superAttackPrefab, transform.position, transform.rotation);
        }
    }
}
