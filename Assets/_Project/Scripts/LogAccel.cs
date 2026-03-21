using UnityEngine;
using System.Collections.Generic;

public class LogAccel : MonoBehaviour
{
    public Transform handTransform; //connect to unity object that stores the position/velocity

    private Vector3 prevPos;
    private Vector3 prevVel;

    private float real_start; 
    private float curr_time;

    //thresholding for accleration values
    private bool isMoving;
    private float start_accel_cut = 0.5f; 
    private float end_accel_cut = 0.2f;

    private List<float> acceleration_max = new List<float>();
    private List<float> start = new List<float>();
    private List<float> end = new List<float>();
    private float timer;
    void OnEnable(){
        Debug.Log("Start Logging");
        if (handTransform == null) {
        Debug.LogError("Hand Transform not assigned!");
        this.enabled = false; // prevent further null errors
        return;
    }
        prevPos=handTransform.position;
        prevVel=Vector3.zero;

        acceleration_max.Clear();
        start.Clear();
        end.Clear();

        isMoving=false;
        curr_time=Time.time;
        real_start=Time.time;
    }

    void Update(){
    
        float delta = Time.deltaTime;
        timer+=delta;
        if (timer >= 10f){
            gameObject.SetActive(false);
        }
        if (delta==0) {
            return;
        }

        Vector3 curr_pos = handTransform.position;
        Vector3 curr_vel = (curr_pos-prevPos) / delta;
        Vector3 curr_accel = (curr_vel-prevVel) / delta;
        
        if (!isMoving) {
            if (curr_accel.magnitude>=start_accel_cut) {
                isMoving=true;
            }
        }
        else {
            acceleration_max.Add(curr_accel.magnitude);
            start.Add(curr_time);
            end.Add(curr_time+delta);
            if (curr_accel.magnitude<=end_accel_cut) {
                isMoving=false;
            }
        }
        curr_time=Time.time;

        prevPos=curr_pos;
        prevVel=curr_vel;
    }

    void OnDisable(){
        float compressby = (Time.time-real_start)/100f;
        float time_counter = real_start; 
        int num = 0; 
        int count = Mathf.Min(start.Count, end.Count, acceleration_max.Count);
        for (int i = 0; i < count; i++){
            if (start[i]>=time_counter && num<100){
                Debug.Log(start[i] + "," + end[i] + "," + acceleration_max[i]);
                time_counter+=compressby; 
                num++;
            }
        }
        Debug.Log("End Logging");

    }
}
