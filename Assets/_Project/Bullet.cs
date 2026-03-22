using System;
using _Project.Scripts.Core.AudioPooling;
using _Project.Scripts.Core.AudioPooling.Interface;
using Sisus.Init;
using UnityEngine;
using UnityEngine.Serialization;
using AudioType = _Project.Scripts.Core.AudioPooling.Interface.AudioType;
using Random = UnityEngine.Random;

namespace _Project
{
    public class Bullet : MonoBehaviour<AudioPooler> 
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private GameObject[] explosionParticles;
        [SerializeField] private AudioClip particleSound;
        [SerializeField] private AudioClip collideSound; 
        private AudioPooler _audioPooler; 
        private IAudioPlayer _audioPlayer;

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
           _audioPlayer = _audioPooler.New3DAudio(particleSound)
                .OnChannel(AudioType.Sfx)
                .AtPosition(transform.position)
                .Play(); 
            Destroy(gameObject, lifeTime);
            _rigidbody.angularVelocity = Random.insideUnitCircle;
        }
        
        private void Update()
        {
            
            Vector3 dir = _target ? (_target.position + _offset)  - transform.position : transform.forward;
            
            
            _rigidbody.linearVelocity = dir.normalized * speed;
        }

        private void OnCollisionEnter(Collision other)
        {
            _audioPooler.New3DAudio(collideSound)
                .OnChannel(AudioType.Sfx)
                .AtPosition(transform.position)
                .Play(); 
            Debug.Log("explodes"); 
            if (explosionParticles.Length > 0)
            {
                int randomIndex = Random.Range(0, explosionParticles.Length);
                
                GameObject randomExplosion = explosionParticles[randomIndex];
                
                Instantiate(randomExplosion, transform.position, transform.rotation);
                
                Debug.Log("exploded");
            }
            
            Destroy(gameObject);
            
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
