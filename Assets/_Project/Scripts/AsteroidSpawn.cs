using UnityEngine;
using System.Collections.Generic;

public class AsteroidSpawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private List<GameObject> prefab;
    [SerializeField] private float secs_before_new_spawn;
    [SerializeField] private bool straight;
    [SerializeField] private Transform target;
    [SerializeField] private int numAstroids = 30;
    private float timer; 
    private int which_asteroid; 
    private float radius;
    
    private int numberSpawned = 0;

    private bool spawning;
//    private Vector3 areaSize; 

    public void StartSpawning()
    {
        numberSpawned = 0;
        spawning = true;
    }

    void Update(){
        if (!spawning || numberSpawned >= numAstroids)
        {
            spawning = false;
            return;
        }
        
        
        if (timer >= secs_before_new_spawn){
            Spawn();
            timer=0;
        } 
        if (straight){
            radius=2;
            //areaSize = new Vector3(5f, 0.5f, 0f); 
        } else {
            radius=15;
            //areaSize = new Vector3(10f, 4f, 0f);
        }
        
        timer += Time.deltaTime;
    }

    void Spawn()
    {
        //Vector3 randomPos = transform.position + new Vector3(0f,0f,60f) + new Vector3(
        //    Random.Range(-areaSize.x /2, areaSize.x/2),Random.Range(0,areaSize.y),Random.Range(0,areaSize.z)
        //);
        numberSpawned++;

        Vector3 circle = Random.insideUnitCircle * radius;
        Vector3 randomPos = transform.position + circle;
        
        

        GameObject obj = Instantiate(prefab[which_asteroid], randomPos, Quaternion.identity);
        AsteroidTowardsViewer script = obj.GetComponent<AsteroidTowardsViewer>();
        script.Init(target);
        script.fly_straight = straight;

        which_asteroid=(which_asteroid+1) % prefab.Count;
    }
}
