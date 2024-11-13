using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class GunInventoryUI : CanvasUIHandler
{

    [Header("UI Elements")] 
    public GameObject GunsPanel;
    public GunShopDataUI GunDataUI;
    public Button BuyButton;
    public TextMeshProUGUI BuyButtonText;
    [Header("Data")]
    public GunMiniUI GunMiniPrefab;

    public List<GunMiniUI> GunEquipedSlots;

    public GunMiniUI Selected { get; private set; }
    public List<GunMiniUI> GunsMiniUI { get; private set; }
    private PlayerController player;

    private void Awake()
    {
        GunsMiniUI = new List<GunMiniUI>();
        player = GameManager.Instance.Player;
    }

    private void Start()
    {
        for (int i = 0; i < GunEquipedSlots.Count; i++)
        {
            var x = i;
            GunEquipedSlots[i].GunButton.onClick.AddListener(() =>
            {
                if(!player.Guns.Contains(Selected.GunHolder))
                    player.InstantiateGun(Selected.GunHolder,x);
            });
        }
        Initialize();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Initialize();
    }

    private void Initialize()
    {
        foreach (var x in player.OwnedGuns)
        {
            GunMiniUI temp = Instantiate(GunMiniPrefab, GunsPanel.transform);
            temp.Initialize(x);
            temp.GunButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() => { ChangeGun(temp);}));
            GunsMiniUI.Add(temp);
        }

        for (int i = 0; i < player.Guns.Length; i++)
        {
            GunEquipedSlots[i].Initialize(player.Guns[i]);
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
