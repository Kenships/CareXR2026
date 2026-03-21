using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private GameObject[] explosionParticles;

        private Transform _target;
        
        private Vector3 _offset;
        
        public void Init(Transform target)
        {
            _offset = Random.insideUnitCircle * 0.5f;
            
            _target = target;
        }
        
        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }
        
        private void Update()
        {
            
            Vector3 dir = _target ? (_target.position + _offset)  - transform.position : transform.forward;
            
            
            transform.position += dir.normalized * speed * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision other)
        {
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
    }
}
