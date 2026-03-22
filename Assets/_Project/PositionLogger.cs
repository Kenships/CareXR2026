using System;
using System.Collections.Generic;
using System.IO;
using Obvious.Soap;
using UnityEngine;

namespace _Project
{
    
    
    public class PositionLogger : MonoBehaviour
    {
        [SerializeField] private ScriptableEventNoParam calibrationEvent;
        [SerializeField] private ScriptableEventNoParam startLogging;
        [SerializeField] private ScriptableEventNoParam endLogging;
        
        [SerializeField] private GameObject left;
        [SerializeField] private GameObject right;
        
        
        private List<(Vector3 pos, float time)> _leftPositions = new();
        private List<(Vector3 pos, float time)> _rightPositions = new();
        private StreamWriter _sw;
        private bool isLogging;

        private string _name = "info";

        private void Start()
        {
            startLogging.OnRaised += StartLogging;
            endLogging.OnRaised += StopLogging;
            calibrationEvent.OnRaised += CalibrationEventOnOnRaised;
        }

        private void CalibrationEventOnOnRaised()
        {
            _name = "calibration";
        }

        private void StartLogging()
        {
            isLogging = true;
        }

        private void Update()
        {
            if (!isLogging) return;
            
            _leftPositions.Add((left.transform.position, Time.realtimeSinceStartup));
            _rightPositions.Add((right.transform.position, Time.realtimeSinceStartup));
        }

        private void StopLogging()
        {
            float initialTime = _leftPositions[3].time;
            
            List<(Vector3 lVel, float lVTime)> leftVelocity = new();
            List<(Vector3 rVel, float rVTime)> rightVelocity = new();
            
            List<(Vector3 rAcc, float aRTime)> rightAcc = new();
            List<(Vector3 lAcc, float aLTime)> leftAcc = new();

            for (int i = 1; i < _leftPositions.Count; i++)
            {
                float deltaTime = _leftPositions[i].time - _leftPositions[i - 1].time;
                Vector3 velocity = (_leftPositions[i].pos - _leftPositions[i - 1].pos)/deltaTime;
                
                leftVelocity.Add((velocity, _leftPositions[i].time - initialTime));
            }

            for (int i = 1; i < _rightPositions.Count; i++)
            {
                float deltaTime = _rightPositions[i].time - _rightPositions[i - 1].time;
                Vector3 velocity = (_rightPositions[i].pos - _rightPositions[i - 1].pos)/deltaTime;
                
                rightVelocity.Add((velocity, _rightPositions[i].time - initialTime));
            }

            for (int i = 1; i < leftVelocity.Count; i++)
            {
                float deltaTime = leftVelocity[i].lVTime - leftVelocity[i - 1].lVTime;
                Vector3 acc = (leftVelocity[i].lVel - leftVelocity[i - 1].lVel) / deltaTime;

                leftAcc.Add((acc, leftVelocity[i].lVTime));
            }

            for (int i = 1; i < rightVelocity.Count; i++)
            {
                float deltaTime = rightVelocity[i].rVTime - rightVelocity[i - 1].rVTime;
                Vector3 acc = (rightVelocity[i].rVel - rightVelocity[i - 1].rVel) / deltaTime;
                
                rightAcc.Add((acc, rightVelocity[i].rVTime));
            }
            
            
            string fileName = $"/{_name}-{DateTime.Now:yy-MM-dd-hh-mm-ss}.csv";
            _sw =  new StreamWriter(Application.dataPath + fileName);
            
            Debug.Log(Application.dataPath + fileName);
            
            _sw.WriteLine("L/R,p_x,p_y,p_z,v_x,v_y,v_z,a_x,a_y,a_z,t,");

            for (int i = 0; i < leftAcc.Count; i++)
            {
                var (pos, pTime) = _leftPositions[i + 2];
                var (vel, vTime) = leftVelocity[i + 1];
                var (acc, aTime) = leftAcc[i];

                _sw.WriteLine($"L,{pos.x},{pos.y},{pos.z},{vel.x},{vel.y},{vel.z},{acc.x},{acc.y},{acc.z},{aTime}");
            }

            for (int i = 0; i < rightAcc.Count; i++)
            {
                var (pos, rTime) = _rightPositions[i + 2];
                var (vel, vTime) =  rightVelocity[i + 1];
                var (acc, aTime) = rightAcc[i];
                
                _sw.WriteLine($"R,{pos.x},{pos.y},{pos.z},{vel.x},{vel.y},{vel.z},{acc.x},{acc.y},{acc.z},{aTime}");
            }
            _sw.Flush();
            _sw.Close();

            _name = "info";
            
            isLogging = false;
        }
    }
}
