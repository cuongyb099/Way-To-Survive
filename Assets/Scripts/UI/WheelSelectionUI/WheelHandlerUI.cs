using System;
using System.Collections.Generic;
using UnityEngine;

public class WheelHandlerUI : MonoBehaviour
{
    public ItemWheelUI Inventory;
    public List<ItemWheelUI> Items;

    private void Awake()
    {

    }

    private void OnDestroy()
    {
        GameEvent.OnStartCombatState -= DeactivateInventory;
        GameEvent.OnStopCombatState -= ActivateInventory;
    }

    private void Start()
    {
       InitalizeItems();
        
       for (int i = 0; i < 4; i++)
       {
           ItemWheelUI item = Items[i];
           item.ItemButton.onClick.AddListener(()=>
           {
               HandleWheelSelection(item.ID);
               gameObject.SetActive(false);
           });
       }
       GameEvent.OnStartCombatState += DeactivateInventory;
       GameEvent.OnStopCombatState += ActivateInventory;
    }

    public void InitalizeItems()
    {
        WeaponBase[] guns = GameManager.Instance.Player.Weapons;
        //0->2 is for guns
        for (int i = 0; i < 3; i++)
        {
            Items[i].Initialize(i, guns[i] != null ? guns[i].GunData.GunName : null,
                guns[i] != null ? guns[i].GunData.Icon : null);
        }

        Items[3].Initialize(3,"Inventory",null);
    }

    public void HandleWheelSelection(int wheelIndex)
    {
        switch (wheelIndex)
        {
            case 0:
                GameManager.Instance.Player.SwitchWeapon(0);
                break;
            case 1:
                GameManager.Instance.Player.SwitchWeapon(1);
                break;
            case 2:
                GameManager.Instance.Player.SwitchWeapon(2);
                break;
        }
    }
    private void ActivateInventory()
    {
        Inventory.ItemButton.interactable = true;
    }
    private void DeactivateInventory()
    {
        Inventory.ItemButton.interactable = false;
    }
}
