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
    [SerializeField] private GameObject scoreCanvas;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private ScriptableEventNoParam endEvent;
    [SerializeField] private IntVariable score;
    [SerializeField] private ScriptableEventNoParam tutorialComplete;
    [SerializeField] private ScriptableEventNoParam reCalibrate;
    [SerializeField] private ScriptableEventNoParam replayGame;
    [SerializeField] private CurveInteractionCaster left;
    [SerializeField] private CurveInteractionCaster right;

    private void Start()
    {
        if (skipTutorial)
        {
            TutorialCompleteOnRaised();
            return;
        }

        scoreCanvas.SetActive(false);
        punchController.enabled = false;
        calibrationTutorial.SetActive(true);
        starPositioner.gameObject.SetActive(false);

        left.castDistance = 10f;
        right.castDistance = 10f;
        replayGame.OnRaised += TutorialCompleteOnRaised;
        tutorialComplete.OnRaised += TutorialCompleteOnRaised;
        endEvent.OnRaised += EndEventOnRaised;
    }
    

    private void EndEventOnRaised()
    {
        punchController.enabled = false;
        endScreen.SetActive(true);
    }

    private void TutorialCompleteOnRaised()
    {
        calibrationTutorial.SetActive(false);
        punchController.enabled = true;
        asteroidSpawn.StartSpawning();
        scoreCanvas.SetActive(true);
        score.Value = 0;
    }
}
