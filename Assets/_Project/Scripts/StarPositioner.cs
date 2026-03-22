using System;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace _Project.Scripts
{
    public class StarPositioner : MonoBehaviour
    {
        [SerializeField] private ScriptableEventNoParam starCompleted;
        [SerializeField] ReachCalibrationService reachCalibrationService;
        [SerializeField] List<Transform> starPositions;
        [SerializeField] GameObject starPrefab;

        private List<GameObject> _stars = new();

        private bool first;
        
        private void OnEnable()
        {
            first = true;
            
            float radius = Mathf.Min(reachCalibrationService.MaxLeftReach, reachCalibrationService.MaxRightReach);
            
            foreach (Transform starPosition in starPositions)
            {
                var obj = Instantiate(starPrefab, starPosition.position + new Vector3(0, radius + 0.1f, 0), starPosition.rotation);
                _stars.Add(obj);
            }
        }

        private void Update()
        {
            for (int i = _stars.Count - 1; i >= 0; i--)
            {
                if (_stars[i] == null)
                {
                    _stars.RemoveAt(i);
                }
            }
            
            if (first && _stars.Count == 0)
            {
                starCompleted.Raise();
                first = false;
            }
        }
    }
}
