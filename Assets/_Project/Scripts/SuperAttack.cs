using System;
using System.Collections.Generic;
using _Project.Scripts.Core.AudioPooling;
using _Project.Scripts.Core.AudioPooling.Interface;
using _Project.Scripts.Util.ExtensionMethods;
using Obvious.Soap;
using Sisus.Init;
using UnityEngine;
using AudioType = _Project.Scripts.Core.AudioPooling.Interface.AudioType;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class SuperAttack : MonoBehaviour<AudioPooler>
    {
        [SerializeField] private ScriptableEventNoParam destroyEvent;
        [SerializeField] private List<ColliderEnterEventTrigger> colliderTriggers;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float lifeTIme = 10f;
        [SerializeField] private GameObject[] explosionParticles;
        [SerializeField] private float speedMultiplier = 0.5f; 
        [SerializeField] private AudioClip particleSound;
        [SerializeField] private AudioClip chargeSound;
        private AudioPooler _audioPooler; 
        private void Start()
        { 
            _audioPooler.New3DAudio(particleSound)
                .OnChannel(AudioType.Sfx)
                .AtPosition(transform.position)
                .Play(); 
            if (explosionParticles.Length > 0)
            {
                int randomIndex = Random.Range(0, explosionParticles.Length);
                
                GameObject randomExplosion = explosionParticles[randomIndex];
                
                Instantiate(randomExplosion, transform.position, Quaternion.AngleAxis(90f, transform.up) * transform.rotation, transform);
                randomExplosion.transform.localScale = new Vector3(4f, 4f, 4f);
            }
            Destroy(gameObject, lifeTIme);
        }
        protected override void Init(AudioPooler argument)
        {
            _audioPooler = argument; 
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.collider.IsOnLayer(layerMask))
            {
                return;
            }
            destroyEvent?.Raise();
            Destroy(other.gameObject);
        }
    }
}
