using UnityEngine;

namespace _Project.Scripts
{
    public class TestShooter : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        
        [SerializeField] private Transform target;

        public void Shoot()
        {
            Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.Init(target);
        }
        
    }
}
