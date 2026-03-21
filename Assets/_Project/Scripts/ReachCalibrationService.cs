using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Obvious.Soap;
using Sisus.Init;
using UnityEngine;

namespace _Project.Scripts
{
    public class ReachCalibrationService : MonoBehaviour
    {
        [SerializeField] private GameObject leftHand;
        [SerializeField] private GameObject rightHand;
        
        [SerializeField] private ScriptableEventNoParam calibrate;
        [SerializeField] private float calibrationTime;
        
        private List<Vector3> _positions = new();
        
        private bool _isCalibrating;
        private float _timer;
        
        private void Start()
        {
            calibrate.OnRaised += StartCalibration;
        }

        private void Update()
        {
            if (!_isCalibrating)
                return;
            
            _timer += Time.deltaTime;

            if (_timer >= calibrationTime)
            {
                _isCalibrating = false;
                return;
            }
            
            _positions.Add(transform.position);
        }

        private void StartCalibration()
        {
            _isCalibrating = true;
            _timer = 0;
            _positions.Clear();
        }
    }
}
