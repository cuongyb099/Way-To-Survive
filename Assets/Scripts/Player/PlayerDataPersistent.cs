
using System;
using KatInventory;
using Tech.Singleton;
using UnityEngine;

public class PlayerDataPersistent : SingletonPersistent<PlayerDataPersistent>
{
    [field:SerializeField] public string Username { get;private set; }
    
    //
    [field:SerializeField] public WeaponBaseSO[] StartingWeapons { get;private set; }
    [field:SerializeField] public BaseBuffSO[] StartingBuffs{ get;private set; }
    protected override void Awake()
    {
        base.Awake();
        StartingWeapons = new WeaponBaseSO[3];
        StartingBuffs = new BaseBuffSO[4];
        
        ItemDataBase.OnLoadDone += () => Inventory.Instance.Load();
    }

    private void Start()
    {
        
    }

    public void ChangeStartingWeapons(WeaponBaseSO[] weaponBaseSo)
    {
        StartingWeapons = weaponBaseSo;
    }
    public void ChangeStartingWeapons(WeaponBaseSO weapon1,WeaponBaseSO weapon2,WeaponBaseSO weapon3)
    {
        StartingWeapons[0] = weapon1;
        StartingWeapons[1] = weapon2;
        StartingWeapons[2] = weapon3;
    }
    public void ChangeStartingBuffs(BaseBuffSO[] buffBaseSo)
    {
        StartingBuffs = buffBaseSo;
    }

    public void ApplyToPlayer(PlayerController playerController)
    {
        int i;
        for (i = 0; i < StartingWeapons.Length; i++)
        {
            if (StartingWeapons[i] != null){
                playerController.InstantiateWeapon(StartingWeapons[i],i);
            }
        }
        for (i = 0; i < StartingBuffs.Length; i++)
        {
            if (StartingBuffs[i] != null)
                playerController.AddBuffToPlayer(StartingBuffs[i]);
        }
    }
}