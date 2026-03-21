using UnityEngine;

namespace _Project.Scripts
{
    public class TransformDebug : MonoBehaviour
    {
        private void FixedUpdate()
        {
            Debug.Log($"Position: {transform.position}, Rotation: {transform.rotation}");
        }
    }
}
