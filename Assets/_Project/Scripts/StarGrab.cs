using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace _Project.Scripts
{
    public class StarGrab : MonoBehaviour
    {
        public UnityEvent OnGrab;

        [SerializeField] private float grabDuration = 3f;
        [SerializeField] private Transform visual;
        private Vector3 startPosition;
        private Quaternion startRotation;
        private Vector3 startScale;
        
        private XRGrabInteractable _grabInteractable;

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
            Tween.Scale(
                target: visual,
                endValue: 0f,
                duration: grabDuration
            ).OnComplete(() =>
                         {
                             Destroy(gameObject); 
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
    }
}
