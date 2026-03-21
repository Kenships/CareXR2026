using UnityEngine;

namespace _Project.Scripts
{
    public class SuperAttack : MonoBehaviour
    {
        [SerializeField] private float lifeTIme = 10f;
        
        private void Start()
        {
            Destroy(gameObject, lifeTIme);
        }
    }
}
