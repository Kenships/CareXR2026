using System;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Events;

public class CalibrationUI : MonoBehaviour
{
    public UnityEvent DoSomething;
    
    [SerializeField] private GameObject completeUI;
    [SerializeField] private GameObject countdownUI;
    [SerializeField] private GameObject button;
    
    [SerializeField] private ScriptableEventNoParam calibrationComplete;

    private void OnEnable()
    {
        completeUI.SetActive(false);
        countdownUI.SetActive(false);
        button.SetActive(true);
    }

    private void Start()
    {
        calibrationComplete.OnRaised += CalibrationCompleteOnRaised;
    }

    private void OnDestroy()
    {
        calibrationComplete.OnRaised -= CalibrationCompleteOnRaised;
    }

    private void CalibrationCompleteOnRaised()
    {
        completeUI.SetActive(true);
        countdownUI.SetActive(false);
        
        DoSomething?.Invoke();
    }
}
