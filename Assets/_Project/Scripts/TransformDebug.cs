using UnityEngine;

namespace _Project.Scripts
{
    public class TransformDebug : MonoBehaviour
    {
        private void FixedUpdate()
        {
            Vector3 offset = new Vector3(0, 1.2f, 0);
            Debug.Log($"{gameObject.name} has Position: {(transform.position - offset).magnitude}, Rotation: {transform.rotation}");
        }
    }
}
