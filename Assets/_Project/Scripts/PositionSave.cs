using UnityEngine;
using System.Collections.Generic;
public class PositionSave : MonoBehaviour
{
    public List<(string, Vector3)> records = new List<(string, Vector3)>(); 
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
            records.Add((gameObject.name, transform.position));
            Debug.Log("Test: " + gameObject.name + ", " + transform.position);
            timer -= interval_timer;
        }
        
    }
}
