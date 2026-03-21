using UnityEngine;

namespace _Project.Scripts
{
    public class SuperAttack : MonoBehaviour
    {
        [SerializeField] private float lifeTIme = 10f;
        [SerializeField] private GameObject[] explosionParticles;
        [SerializeField] private float speedMultiplier = 0.5f; 
        
        private void Start()
        {if (explosionParticles.Length > 0)
            {
                int randomIndex = Random.Range(0, explosionParticles.Length);
                
                GameObject randomExplosion = explosionParticles[randomIndex];
                
                Instantiate(randomExplosion, transform.position, Quaternion.AngleAxis(90f, Vector3.up) * transform.rotation);
                randomExplosion.transform.localScale = new Vector3(4f, 4f, 4f);
            }
            Destroy(gameObject, lifeTIme);
        }
    }
}
