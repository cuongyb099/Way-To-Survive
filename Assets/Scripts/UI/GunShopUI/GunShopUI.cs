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

public class GunShopUI : CanvasUIHandler
{

    [Header("UI Elements")] 
    public GameObject GunsPanel;
    public GunShopDataUI GunDataUI;
    public Button BuyButton;
    public TextMeshProUGUI BuyButtonText;
    public TextMeshProUGUI CashText;
    [FormerlySerializedAs("GunListSO")] [Header("Data")] 
    public WeaponListSO WeaponListSo;
    public GunMiniUI GunMiniPrefab;

    public GunMiniUI Selected { get; private set; }
    public List<GunMiniUI> GunsMiniUI { get; private set; }
    private void Awake()
    {
        PlayerEvent.OnCashChange += ChangeCashText;
        Initialize();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        PlayerController player = GameManager.Instance.Player;
        ChangeCashText(player.Cash);
    }

    private void OnDestroy()
    {
        PlayerEvent.OnCashChange -= ChangeCashText;
    }

    private void Initialize()
    {
        GunsMiniUI = new List<GunMiniUI>();
        foreach (var x in WeaponListSo.Weapons)
        {
            GunMiniUI temp = Instantiate(GunMiniPrefab, GunsPanel.transform);
            temp.Initialize(x);
            temp.GunButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() => { ChangeGun(temp);}));
            GunsMiniUI.Add(temp);
        }
        ChangeGun(GunsMiniUI[0]);
    }
    
    private void ChangeGun(GunMiniUI gunUI)
    {
        Selected = gunUI;
        gunUI.GunButton.Select();
        GunDataUI.ChangeGun(gunUI.GunHolder.GunData);
        
        PlayerController player = GameManager.Instance.Player;
        if (player.OwnedWeapons.Contains(Selected.GunHolder))
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
        if (player.Cash < Selected.GunHolder.GunData.GunPrice) return;

        player.Cash -= Selected.GunHolder.GunData.GunPrice;
        player.OwnedWeapons.Add(Selected.GunHolder);

        for(int i = 0; i< player.Weapons.Length; i++)
        {
            if (player.Weapons[i] != null) continue;
            player.InstantiateGun(Selected.GunHolder,i);
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
