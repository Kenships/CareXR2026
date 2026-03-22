using System;
using _Project.Scripts.Core.AudioPooling;
using PrimeTween;
using Sisus.Init;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using AudioType = _Project.Scripts.Core.AudioPooling.Interface.AudioType;

namespace _Project.Scripts
{
    public class StarGrab : MonoBehaviour <AudioPooler> 
    {
        public bool ignoreGrab = false;
        public UnityEvent OnGrab;

        [SerializeField] private float grabDuration = 3f;
        [SerializeField] private Transform visual;
        [SerializeField] private AudioClip particleSound;
        private Vector3 startPosition;
        private Quaternion startRotation;
        private Vector3 startScale;
        
        private XRGrabInteractable _grabInteractable;
        private AudioPooler _audioPooler; 

        private void Start()
        {
            startPosition = transform.position;
            startRotation = transform.rotation;
            startScale = visual.localScale;
            
            _grabInteractable = GetComponent<XRGrabInteractable>();
            
            _grabInteractable.selectExited.AddListener(SelectExit);
            _grabInteractable.selectEntered.AddListener(SelectEnter);
        }

        private void SelectEnter(SelectEnterEventArgs arg0)
        {
            if (ignoreGrab)
            {
                OnGrab?.Invoke();
                return;
            }
            
            Tween.Scale(
                target: visual,
                endValue: 0f,
                duration: grabDuration
            ).OnComplete(() =>
                         {
                             Destroy(gameObject); 
                             _audioPooler.New3DAudio(particleSound)
                                 .OnChannel(AudioType.Sfx)
                                 .AtPosition(transform.position)
                                 .Play(); 
                             OnGrab?.Invoke();
                         }
            );
        }

        private void SelectExit(SelectExitEventArgs arg0)
        { 
            Tween.StopAll(visual);
            transform.position = startPosition;
            transform.rotation = startRotation;
            visual.localScale = startScale;
        }

        protected override void Init(AudioPooler argument)
        {
            _audioPooler = argument;
        }
    }
}
