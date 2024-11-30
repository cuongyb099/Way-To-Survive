using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableController : BasicController, IInteractable
{
    public GameObject BuffCanvas;
    public GameObject SelectedUI;
    public TextMeshProUGUI SpeakingText;
    public Transform ObjTransform => transform;
    public Action OnKill { get; set; }
    
    private void OnDestroy()
    {
        OnKill?.Invoke();
    }

    public virtual void Interact(PlayerController source)
    {
        BuffCanvas.SetActive(true);
    }

    public void OnSelect()
    {
        SelectedUI.gameObject.SetActive(true);
    }

    public void OnDeselect()
    {
        SelectedUI.gameObject.SetActive(false);
    }
    
}
