using System;
using BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : BasicController, IInteractable
{
    public GameObject ShopCanvas;
    public GameObject SelectedUI;
    public Transform ObjTransform => transform;
    public Action OnKill { get; set; }
    public int Cash { get; set; }
	protected override void Awake()
    {
        base.Awake();
        PlayerEvent.OnCashRecieve += IncreaseCash;
    }
    private void OnDestroy()
    {
        PlayerEvent.OnCashRecieve -= IncreaseCash;
        OnKill?.Invoke();
    }

    public void Interact(PlayerController source)
    {
        ShopCanvas.SetActive(true);
    }

    public void OnSelect()
    {
        SelectedUI.gameObject.SetActive(true);
    }

    public void OnDeselect()
    {
        SelectedUI.gameObject.SetActive(false);
    }

    public void IncreaseCash(int value)
    {
        Cash += value;
    }
    public void DecreaseCash(int value)
    {
        Cash -= value;
        if (Cash < 0) Cash = 0;
    }
}
