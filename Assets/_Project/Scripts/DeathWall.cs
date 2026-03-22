using System;
using _Project.Scripts.Core.AudioPooling;
using _Project.Scripts.Util.ExtensionMethods;
using Obvious.Soap;
using Sisus.Init;
using UnityEngine;
using UnityEngine.Audio;
using AudioType = _Project.Scripts.Core.AudioPooling.Interface.AudioType;

public class DeathWall : MonoBehaviour <AudioPooler>
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private ScriptableEventNoParam destroyEvent; 
    [SerializeField] private AudioClip audioSource;
    
    private AudioPooler _audioPooler;
    

    private void OnTriggerEnter(Collider other)
    {
        if (!other.IsOnLayer(mask))
        {
            return;
        }
        Destroy(other.gameObject);
        _audioPooler.New2DAudio(audioSource)
            .OnChannel(AudioType.Sfx)
            .Play();
        destroyEvent?.Raise();
    }

    protected override void Init(AudioPooler argument)
    {
        _audioPooler = argument;
    }
}
