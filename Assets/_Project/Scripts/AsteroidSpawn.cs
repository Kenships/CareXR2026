using UnityEngine;
using System.Collections.Generic;

public class AsteroidSpawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private List<GameObject> prefab;
    [SerializeField] private float secs_before_new_spawn;
    [SerializeField] private bool straight;
    private float last; 
    private int which_asteroid; 
    private float radius;
//    private Vector3 areaSize; 

    void Start(){
        last=Time.time;
    }

    void Update(){
        if (Time.time-last >= secs_before_new_spawn){
            Spawn();
            last=Time.time;
        } 
        if (straight){
            radius=2;
            //areaSize = new Vector3(5f, 0.5f, 0f); 
        } else {
            radius=15;
            //areaSize = new Vector3(10f, 4f, 0f);
        }
    }

    void Spawn()
    {
        //Vector3 randomPos = transform.position + new Vector3(0f,0f,60f) + new Vector3(
        //    Random.Range(-areaSize.x /2, areaSize.x/2),Random.Range(0,areaSize.y),Random.Range(0,areaSize.z)
        //);


        Vector2 circle = Random.insideUnitCircle * radius;
        float spawnDistance = 40f;
        Vector3 randomPos =
            transform.position
            + transform.forward * spawnDistance
            + transform.right * circle.x
            + transform.up * circle.y;

        GameObject obj = Instantiate(prefab[which_asteroid], randomPos, Quaternion.identity);
        AsteroidTowardsViewer script = obj.GetComponent<AsteroidTowardsViewer>();
        script.viewer = Camera.main.transform;
        script.fly_straight = straight;

        which_asteroid=(which_asteroid+1) % prefab.Count;
    }
}
