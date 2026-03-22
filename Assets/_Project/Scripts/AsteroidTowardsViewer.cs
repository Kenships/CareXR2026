using UnityEngine;
using System.Collections.Generic;


public class AsteroidTowardsViewer : MonoBehaviour
{
    public Transform viewer;
    public Transform asteroid;
    private Vector3 initial;
    private Vector3 straight;
    public bool fly_straight;
    [SerializeField] private float distance; 
    [SerializeField] private float speed; 
    
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initial=asteroid.position;
        viewer ??= Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        if (fly_straight){
            straight=new Vector3(0, 0, -1);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!fly_straight) {
            Vector3 dir = (viewer.position-asteroid.position).normalized;
            rb.MovePosition(rb.position + dir*speed*(Time.deltaTime));

            if ((rb.position-viewer.position).magnitude<= distance){
                Destroy(gameObject); 
            }
        }
        else {
            rb.MovePosition(rb.position + straight*speed*(Time.deltaTime));
             
            if ((rb.position.z) <= 0.65){
                Destroy(gameObject); 
            }

        }
    }

    void OnDisable(){
        asteroid.position=initial;
    }
}
