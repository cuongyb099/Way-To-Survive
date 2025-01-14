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
    public ItemMiniUI GunMiniPrefab;

    public List<ItemMiniUI> GunEquipedSlots;

    public ItemMiniUI Selected { get; private set; }
    public List<ItemMiniUI> GunsMiniUI { get; private set; }
    private PlayerController player;

    private void Awake()
    {
        GunsMiniUI = new List<ItemMiniUI>();
        player = GameManager.Instance.Player;
    }

    private void Start()
    {
        for (int i = 0; i < GunEquipedSlots.Count; i++)
        {
            var x = i;
            GunEquipedSlots[i].ItemButton.onClick.AddListener(()=>InitEquippedSlots(x));
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
            ItemMiniUI g = GunsMiniUI[i];
            g.ItemButton.onClick.RemoveAllListeners();
            GunsMiniUI.RemoveAt(i);
            Destroy(g.gameObject);
        }
        //instantiate gun panel
        foreach (var x in player.OwnedWeapons)
        {
            ItemMiniUI temp = Instantiate(GunMiniPrefab, GunsPanel.transform);
            temp.Initialize(x.WeaponData);
            temp.ItemButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() => { ChangeGun(temp);}));
            GunsMiniUI.Add(temp);
        }

        for (int i = 0; i < player.Weapons.Length; i++)
        {
            GunEquipedSlots[i].Initialize(player.Weapons[i] != null?player.Weapons[i].WeaponData:null);
        }
        ChangeGun(GunsMiniUI[0]);
    }
    
    private void ChangeGun(ItemMiniUI gunUI)
    {
        Selected = gunUI;
        GunBaseSo weapon = (GunBaseSo)gunUI.ItemBaseSoHolder;
        gunUI.ItemButton.Select();
        GunDataUI.ChangeGun(weapon);
    }
    private void InitEquippedSlots(int index)
    {
        WeaponBase weapon = ((GunBaseSo)Selected.ItemBaseSoHolder).WeaponPrefab ;
        if (!player.ContainsWeapon(weapon))
        {
            player.InstantiateWeapon(weapon,index);
            GunEquipedSlots[index].Initialize(Selected.ItemBaseSoHolder);
        }

    }
}
