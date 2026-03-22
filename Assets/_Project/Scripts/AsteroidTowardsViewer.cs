using UnityEngine;
using System.Collections.Generic;


public class AsteroidTowardsViewer : MonoBehaviour
{
    private Transform _target;
    public Transform asteroid;
    private Vector3 straight;
    public bool fly_straight;
    [SerializeField] private float distance; 
    [SerializeField] private float speed; 
    
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (fly_straight){
            straight=new Vector3(0, 0, -1);
        }
        
        if (!fly_straight) {
            Vector3 dir = (asteroid.position - _target.position).normalized;
            rb.linearVelocity = dir * speed;
        }
        else {
            rb.linearVelocity = straight*speed;
        }
    }

    public void Init(Transform target)
    {
        this._target=target;
    }
}
