using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class GrabToHand : MonoBehaviour
{
    [SerializeField] private GameObject hoverVisual;
    private XRGrabInteractable _grabInteractable;

    private void Awake()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _grabInteractable.hoverEntered.AddListener(ShowHover);
        _grabInteractable.hoverExited.AddListener(HideHover);
    }

    private void Start()
    {
        hoverVisual.SetActive(false);
    }

    private void ShowHover(HoverEnterEventArgs arg0)
    {
        hoverVisual.SetActive(true);
    }
    
    private void HideHover(HoverExitEventArgs arg0)
    {
        hoverVisual.SetActive(false);
    }
}
