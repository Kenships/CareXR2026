using System.Collections.Generic;
using _Project.Scripts.Core.AudioPooling;
using _Project.Scripts.Util.ExtensionMethods;
using _Project.Scripts.Util.Timer.Timers;
using Obvious.Soap;
using PrimeTween;
using Sisus.Init;
using UnityEngine;
using AudioType = _Project.Scripts.Core.AudioPooling.Interface.AudioType;

namespace _Project.Scripts
{
    public class PunchController : MonoBehaviour <AudioPooler>
    {
        [SerializeField] private ReachCalibrationService reachCalibrationService;
        [SerializeField] private FloatVariable superAttackCharge;
        [SerializeField] private GameObject ChargeEffect;
        
        [SerializeField] private GameObject superAttackPrefab;
        [SerializeField] private float threshold = 0.2f;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private ColliderEnterEventTrigger close;
        [SerializeField] private ColliderEnterEventTrigger far;
        [SerializeField] private float chargeTime = 0.5f;
        [SerializeField] private AudioClip chargeSound;

        [SerializeField] private Bullet bullet;
        
        private AudioPooler _audioPooler;
        
        private CountdownTimer _timer;
        
        private GameObject _chargeEffect;
        
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

            if (_chargeEffect != null)
            {
                superAttackCharge.Value = 0f;
                Destroy(_chargeEffect);
                _chargeEffect = null;
                _timer = null;
            }
        }

        private void OnCloseEnter(Collider obj)
        {
            if (!obj.IsOnLayer(layerMask))
            {
                return;
            }
            Debug.Log("Close");

            if (!_close.Contains(obj))
            {
                _close.Add(obj);
            }
            
            if (_close.Count == 2 && _chargeEffect == null)
            {
                _chargeEffect = Instantiate(ChargeEffect, transform.position, transform.rotation, transform);
                _audioPooler.New3DAudio(chargeSound)
                    .OnChannel(AudioType.Sfx)
                    .AtPosition(transform.position)
                    .Play();
                Tween.Scale(
                    target: _chargeEffect.transform,
                    endValue: ChargeEffect.transform.localScale,
                    duration: chargeTime * 1.5f,
                    ease: Ease.InExpo
                );
                
                
                _timer = new CountdownTimer(chargeTime);
                _timer.Start();
            }
        }

        private void Update()
        {
            far.SetReach(Mathf.Min(reachCalibrationService.MaxLeftReach, reachCalibrationService.MaxRightReach) * 0.9f);
            close.SetReach(Mathf.Min(reachCalibrationService.MaxLeftReach, reachCalibrationService.MaxRightReach) * 0.67f);
            
            if (_timer is { IsRunning: true })
            {
                Vector3 middle = (_close[0].transform.position + _close[1].transform.position) * 0.5f;
                superAttackCharge.Value = 100f - _timer.Progress * 100f;
                
                if (_chargeEffect == null)
                    return;
                
                _chargeEffect.transform.position = middle;
            }
        }

        private void Shoot(Transform objTransform)
        {
            if (_timer is { IsFinished: true })
            {
                superAttackCharge.Value = 0f;
                
                _timer = null;
                Destroy(_chargeEffect);
                _chargeEffect = null;
                SuperAttack();
                _close.Clear();
                return;
            }
            
            int count = Physics.OverlapSphereNonAlloc(objTransform.position, 100f, _buffer, targetMask);
            
            List<Collider> colliders = new List<Collider>();

            for (int i = 0; i < count; i++)
            {
                if (Vector3.Dot(objTransform.forward, _buffer[i].transform.position - objTransform.position) >
                    threshold)
                {
                    colliders.Add(_buffer[i]);
                }
            }
            
            Transform target = null;
            
            if (colliders.Count > 0)
            {
                float bestDistance = float.MaxValue;
                int bestIndex = -1;

                for (int i = 0; i < count; i++)
                {
                    var distance = Vector3.Distance(objTransform.position, _buffer[i].transform.position);
                    
                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
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

        protected override void Init(AudioPooler argument)
        {
            _audioPooler = argument; 
        }
    }
}
