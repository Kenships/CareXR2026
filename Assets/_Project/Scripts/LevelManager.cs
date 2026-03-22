using System;
using _Project.Scripts;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors.Casters;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool skipTutorial;
    
    [SerializeField] private GameObject calibrationTutorial;
    [SerializeField] private PunchController punchController;
    [SerializeField] private AsteroidSpawn asteroidSpawn;
    [SerializeField] private StarPositioner starPositioner;
    
    [SerializeField] private ScriptableEventNoParam tutorialComplete;
    
    [SerializeField] private CurveInteractionCaster left;
    [SerializeField] private CurveInteractionCaster right;
    
    private void Start()
    {
        if (skipTutorial)
        {
            TutorialCompleteOnRaised();
            return;
        }
        
        punchController.enabled = false;
        calibrationTutorial.SetActive(true);
        starPositioner.gameObject.SetActive(false);

        left.castDistance = 10f;
        right.castDistance = 10f;
        
        tutorialComplete.OnRaised += TutorialCompleteOnRaised;
    }

    private void TutorialCompleteOnRaised()
    {
        calibrationTutorial.SetActive(false);
        punchController.enabled = true;
        asteroidSpawn.StartSpawning();
    }
}
