using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GunShopUI : FadeBlurPanel
{

    [Header("UI Elements")] 
    public GameObject GunsPanel;
    public GunShopDataUI GunDataUI;
    public Button BuyButton;
    public TextMeshProUGUI BuyButtonText;
    public TextMeshProUGUI CashText;
    public Button BackButton;
    [Header("Data")] 
    public WeaponListSO WeaponListSo;
    public ItemMiniUI ItemMiniPrefab;

    public ItemMiniUI Selected { get; private set; }
    public List<ItemMiniUI> ItemsMiniUI { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        Initialize();
        PlayerEvent.OnCashChange += ChangeCashText;
        PlayerController player = GameManager.Instance.Player;
        ChangeCashText(player.Cash);
    }

    private void OnDestroy()
    {
        PlayerEvent.OnCashChange -= ChangeCashText;
    }

    private void Initialize()
    {
        ItemsMiniUI = new List<ItemMiniUI>();
        foreach (var x in WeaponListSo.Weapons)
        {
            ItemMiniUI temp = Instantiate(ItemMiniPrefab, GunsPanel.transform);
            temp.Initialize(x.WeaponData);
            temp.ItemButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() => { ChangeGun(temp);}));
            ItemsMiniUI.Add(temp);
        }
        ChangeGun(ItemsMiniUI[0]);
        
        BackButton.onClick.AddListener(() =>
        {
            Hide();
            UIManager.Instance.ShowPanel(UIConstant.MainGameplayPanel);
        });
    }
    
    private void ChangeGun(ItemMiniUI itemUI)
    {
        Selected = itemUI;
        itemUI.ItemButton.Select();
        GunBaseSO weapon = (GunBaseSO)itemUI.ItemBaseSoHolder;
        GunDataUI.ChangeGun(weapon);
        
        PlayerController player = GameManager.Instance.Player;
        if (player.OwnedWeapons.Contains(weapon.WeaponPrefab))
        {
            BuyButton.interactable = false;
            BuyButtonText.text = "Owned";
        }
        else
        {
            BuyButton.interactable = true;
            BuyButtonText.text = "Buy";
        }
    }

    public void OnBuyGun()
    {
        PlayerController player = GameManager.Instance.Player;
        GunBaseSO weapon = (GunBaseSO)Selected.ItemBaseSoHolder;
        if (player.Cash < weapon.BuyPrice) return;

        player.Cash -= weapon.BuyPrice;
        player.OwnedWeapons.Add(weapon.WeaponPrefab);

        for(int i = 0; i< player.Weapons.Length; i++)
        {
            if (player.Weapons[i] != null) continue;
            player.InstantiateWeapon(weapon.WeaponPrefab,i);
            break;
        }
        
        BuyButton.interactable = false;
        BuyButtonText.text = "Owned";
    }

    private void ChangeCashText(int value)
    {
        CashText.text = value+"$";
    }
}
