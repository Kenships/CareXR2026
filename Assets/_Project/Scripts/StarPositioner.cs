using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace _Project.Scripts
{
    public class StarPositioner : MonoBehaviour
    {
        [SerializeField] ReachCalibrationService reachCalibrationService;
        [SerializeField] List<Transform> starPositions;
        [SerializeField] GameObject starPrefab;

        private void OnEnable()
        {
            float radius = Mathf.Min(reachCalibrationService.MaxLeftReach, reachCalibrationService.MaxRightReach);
            
            foreach (Transform starPosition in starPositions)
            {
                Instantiate(starPrefab, starPosition.position + new Vector3(0, radius + 0.1f, 0), starPosition.rotation);
            }
        }
    }
}
