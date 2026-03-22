using System;
using _Project.Scripts.Core.AudioPooling;
using _Project.Scripts.Core.AudioPooling.Interface;
using _Project.Scripts.Util.ExtensionMethods;
using Obvious.Soap;
using Sisus.Init;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Hands.OpenXR;
using AudioType = _Project.Scripts.Core.AudioPooling.Interface.AudioType;
using Random = UnityEngine.Random;

namespace _Project
{
    public class Bullet : MonoBehaviour<AudioPooler> 
    {
        [SerializeField] private LayerMask layerMask;
        
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private GameObject[] explosionParticles;
        [SerializeField] private AudioClip particleSound;
        [SerializeField] private AudioClip collideSound;
        [SerializeField] private ScriptableEventNoParam destroyEvent; 
        private AudioPooler _audioPooler; 
        private IAudioPlayer _audioPlayer;
        
        private Collider[] _buffer = new Collider[100];

        private Transform _target;
        
        private Vector3 _offset;
        
        private Rigidbody _rigidbody;
        
        public void Init(Transform target)
        {
            _offset = Random.insideUnitCircle * 0.5f;
            
            _target = target;
        }
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
           _audioPlayer = _audioPooler.New2DAudio(particleSound)
                .OnChannel(AudioType.Sfx)
                .Play(); 
            Destroy(gameObject, lifeTime);
            _rigidbody.angularVelocity = Random.insideUnitCircle;
        }
        
        private void Update()
        {
            
            Vector3 dir = _target ? (_target.position + _offset) - transform.position : transform.forward;
            
            
            _rigidbody.linearVelocity = dir.normalized * speed;
            
            int count = Physics.OverlapSphereNonAlloc(transform.position, 0.5f, _buffer, layerMask);

            if (count == 0)
                return;
            
            _audioPooler.New2DAudio(collideSound)
                .OnChannel(AudioType.Sfx)
                .Play(); 
            Debug.Log("explodes"); 
            if (explosionParticles.Length > 0)
            {
                int randomIndex = Random.Range(0, explosionParticles.Length);
                
                GameObject randomExplosion = explosionParticles[randomIndex];
                
                Instantiate(randomExplosion, transform.position, transform.rotation);
                
                Debug.Log("exploded");
            }

            
            Destroy(_buffer[0].gameObject);
            Destroy(gameObject);
            destroyEvent.Raise();
            
        }

        private void OnDestroy()
        {
            _audioPlayer?.Stop();
        }

        protected override void Init(AudioPooler argument)
        {
            _audioPooler = argument; 
        }
    }
}
