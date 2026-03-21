using UnityEngine;
using System.Collections.Generic;
public class PositionSave : MonoBehaviour
{
    
    private List<string> names = new List<string>();
    private List<Vector3> positions = new List<Vector3>();
    private float timer = 0f;
    [SerializeField]
    private float interval_timer = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= interval_timer)
        {
            string objName = gameObject.name;
            Vector3 objPosition = transform.position;

            names.Add(objName);
            positions.Add(objPosition);
            Debug.Log("Name: " + objName + "Position: "+ objPosition);

            timer -= interval_timer;
        }
        
    }
}
