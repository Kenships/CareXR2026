using _Project.Scripts.Core.AudioPooling;
using _Project.Scripts.Core.AudioPooling.Interface;
using Sisus.Init;
using UnityEngine;
using AudioType = _Project.Scripts.Core.AudioPooling.Interface.AudioType;

namespace _Project.Scripts
{
    public class SuperAttack : MonoBehaviour<AudioPooler> 
    {
        [SerializeField] private float lifeTIme = 10f;
        [SerializeField] private GameObject[] explosionParticles;
        [SerializeField] private float speedMultiplier = 0.5f; 
        [SerializeField] private AudioClip particleSound;
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
                
                Instantiate(randomExplosion, transform.position, Quaternion.AngleAxis(90f, Vector3.up) * transform.rotation);
                randomExplosion.transform.localScale = new Vector3(4f, 4f, 4f);
            }
            Destroy(gameObject, lifeTIme);
        }
        protected override void Init(AudioPooler argument)
        {
            _audioPooler = argument; 
        }
    }
}
