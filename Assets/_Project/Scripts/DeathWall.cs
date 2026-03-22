using System;
using _Project.Scripts.Util.ExtensionMethods;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Audio;

public class DeathWall : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private ScriptableEventNoParam destroyEvent; 
    
    

    private void OnTriggerEnter(Collider other)
    {
        if (!other.IsOnLayer(mask))
        {
            return;
        }
        Destroy(other.gameObject);
        destroyEvent?.Raise();
    }
}
