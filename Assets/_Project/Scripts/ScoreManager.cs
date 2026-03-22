using System;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private ScriptableEventNoParam destroyEvent;
    [SerializeField] private ScriptableEventNoParam onStartEvent;
    [SerializeField] private ScriptableEventNoParam endEvent; 
    [SerializeField] private IntVariable score;
    [SerializeField] private ScriptableEventNoParam hitEvent;
    [SerializeField] private IntVariable multiplier;
    [SerializeField] private int baseScore = 100; 
    [SerializeField] private int baseMultiplier = 1;
    [SerializeField] private int secondMultiplier = 2; 
    [SerializeField] private int thirdMultiplier = 3;
    [SerializeField] private IntVariable asteroidCount; 
    [SerializeField] private IntVariable hitAsteroidCount;
    

    private void Start()
    {
        destroyEvent.OnRaised += DestroyEventOnRaised;
        hitEvent.OnRaised += HitEventOnRaised;
        onStartEvent.OnRaised += OnStartEventOnRaised;
        
        asteroidCount.OnValueChanged += AsteroidCountOnOnValueChanged;
    }

    private void AsteroidCountOnOnValueChanged(int obj)
    {
        if (asteroidCount.Value == 30)
        {
            endEvent.Raise(); 
        }
    }

    private void Update()
    {
        
        
    }
    private void OnStartEventOnRaised()
    {
        multiplier.Value = baseMultiplier;
        score.Value = 0; 
    }

    private void HitEventOnRaised()
    {
        score.Value += baseScore * multiplier;
        multiplier.Value = Mathf.Min(multiplier.Value + 1, 3);
        asteroidCount.Value++;
        hitAsteroidCount.Value++; 
    }

    private void OnDestroy()
    {
        destroyEvent.OnRaised -= DestroyEventOnRaised;
        hitEvent.OnRaised -= HitEventOnRaised;
        onStartEvent.OnRaised -= OnStartEventOnRaised;
        asteroidCount.Value++; 
        
    }
    
    private void DestroyEventOnRaised()
    {
        multiplier.Value = baseMultiplier;
        Debug.Log("destroyEvent raised"); 
    }
}
