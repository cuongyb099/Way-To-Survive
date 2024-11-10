using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class GunShopUI : CanvasUIHandler
{

    [Header("UI Elements")] 
    public GameObject GunsPanel;
    public GunShopDataUI GunDataUI;
    public Button BuyButton;
    public TextMeshProUGUI BuyButtonText;
    [Header("Data")] 
    public GunListSO GunListSO;
    public GunMiniUI GunMiniPrefab;

    public GunMiniUI Selected { get; private set; }
    public List<GunMiniUI> GunsMiniUI { get; private set; }
    private void Awake()
    {
        Initialize();
    }
    
    private void Initialize()
    {
        GunsMiniUI = new List<GunMiniUI>();
        foreach (var x in GunListSO.Guns)
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
        if (player.OwnedGuns.Contains(Selected.GunHolder))
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
        player.OwnedGuns.Add(Selected.GunHolder);

        for(int i = 0; i< player.Guns.Length; i++)
        {
            if (player.Guns[i] != null) continue;
            player.InstantiateGun(Selected.GunHolder,i);
            break;
        }
        
        BuyButton.interactable = false;
        BuyButtonText.text = "Owned";
    }
}
