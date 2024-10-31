using BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : BasicController, IInteractable
{
    public GameObject ShopCanvas;
    public int Cash { get; private set; }
    private void Awake()
    {
        base.Awake();
        PlayerEvent.OnCashRecieve += IncreaseCash;
    }
    private void OnDestroy()
    {
        PlayerEvent.OnCashRecieve += IncreaseCash;
    }

    public void Interact(GameObject source)
    {
        Debug.Log("cc");
        ShopCanvas.SetActive(true);
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
