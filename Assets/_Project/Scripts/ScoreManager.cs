using System;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private ScriptableEventNoParam destroyEvent;
    [SerializeField] private ScriptableEventNoParam onStartEvent; 
    [SerializeField] private IntVariable score;
    [SerializeField] private ScriptableEventNoParam hitEvent;
    [SerializeField] private IntVariable multiplier;
    [SerializeField] private int baseScore = 100; 
    [SerializeField] private int baseMultiplier = 1;
    [SerializeField] private int secondMultiplier = 2; 
    [SerializeField] private int thirdMultiplier = 3;
    

    private void Start()
    {
        destroyEvent.OnRaised += DestroyEventOnRaised;
        hitEvent.OnRaised += HitEventOnRaised;
        onStartEvent.OnRaised += OnStartEventOnRaised;
    }

    private void Update()
    {
        Debug.Log(score.Value+" score");
        Debug.Log(multiplier.Value +"is the multiplier" );
        
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
    }

    private void OnDestroy()
    {
        destroyEvent.OnRaised -= DestroyEventOnRaised;
        hitEvent.OnRaised -= HitEventOnRaised;
        onStartEvent.OnRaised -= OnStartEventOnRaised;
    }
    
    private void DestroyEventOnRaised()
    {
        multiplier.Value = baseMultiplier;
    }
}
