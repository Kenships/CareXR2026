using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

namespace _Project.Scripts
{
    public class ReachCalibrationService : MonoBehaviour
    {
        public float MaxLeftReach { get; private set; }
        public float MaxRightReach { get; private set; }
        
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform rightHand;
        
        [SerializeField] private ScriptableEventNoParam calibrate;
        
        [SerializeField] private float calibrationTime = 5f;
        
        private List<Vector3> _leftPositions = new();
        private List<Vector3> _rightPositions = new();
        
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

                FinishCalibration();
                
                return;
            }

            _leftPositions.Add(leftHand.position);
            _rightPositions.Add(rightHand.position);
        }

        private void FinishCalibration()
        {
            float maxLeft = 0;
            
            foreach (var position in _leftPositions)
            {
                if (position.y - transform.position.y > maxLeft)
                {
                    maxLeft = position.y - transform.position.y;
                }
            }
            
            float maxRight = 0;
            
            foreach (var position in _rightPositions)
            {
                if (position.y - transform.position.y > maxRight)
                {
                    maxRight = position.y - transform.position.y;
                }
            }
            
            MaxLeftReach = maxLeft;
            MaxRightReach = maxRight;
            
            Debug.Log($"Max left reach: {maxLeft}");
            Debug.Log($"Max right reach: {maxRight}");
        }

        private void StartCalibration()
        {
            _isCalibrating = true;
            _timer = 0;
            _leftPositions.Clear();
            _rightPositions.Clear();
        }
    }
}
