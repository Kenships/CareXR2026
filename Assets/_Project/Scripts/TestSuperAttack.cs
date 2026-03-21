using UnityEngine;

namespace _Project.Scripts
{
    public class TestSuperAttack : MonoBehaviour
    {
        public class SuperAttack : MonoBehaviour
        {
            [SerializeField] private SuperAttack superAttackPrefab;
        
            [SerializeField] private Transform target;

            public void Shoot()
            {
                SuperAttack superAttack= Instantiate(superAttackPrefab, transform.position, transform.rotation);
            }
        }
    }
}
