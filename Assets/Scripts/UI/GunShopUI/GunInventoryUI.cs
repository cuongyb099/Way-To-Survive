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
            GunEquipedSlots[i].GunButton.onClick.AddListener(()=>InitEquippedSlots(x));
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Initialize();
    }

    private void Initialize()
    {
        //delete gun panel
        for(int i = GunsMiniUI.Count - 1; i >= 0; i--)
        {
            GunMiniUI g = GunsMiniUI[i];
            g.GunButton.onClick.RemoveAllListeners();
            GunsMiniUI.RemoveAt(i);
            Destroy(g.gameObject);
        }
        //instantiate gun panel
        foreach (var x in player.OwnedWeapons)
        {
            GunMiniUI temp = Instantiate(GunMiniPrefab, GunsPanel.transform);
            temp.Initialize(x);
            temp.GunButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() => { ChangeGun(temp);}));
            GunsMiniUI.Add(temp);
        }

        for (int i = 0; i < player.Weapons.Length; i++)
        {
            GunEquipedSlots[i].Initialize(player.Weapons[i]);
        }
        ChangeGun(GunsMiniUI[0]);
    }
    
    private void ChangeGun(GunMiniUI gunUI)
    {
        Selected = gunUI;
        gunUI.GunButton.Select();
        GunDataUI.ChangeGun(gunUI.GunHolder.GunData);
    }
    private void InitEquippedSlots(int index)
    {
        if (!player.ContainsWeapon(Selected.GunHolder))
        {
            player.InstantiateGun(Selected.GunHolder,index);
            GunEquipedSlots[index].Initialize(Selected.GunHolder);
        }

    }
}
