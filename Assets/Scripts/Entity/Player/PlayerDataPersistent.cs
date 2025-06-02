
using System;
using System.IO;
using KatInventory;
using Tech.Json;
using Tech.Singleton;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerDataPersistent : SingletonPersistent<PlayerDataPersistent>,ISaveable
{
    public static readonly string DefaultPath = System.IO.Path.Combine(Application.streamingAssetsPath, "SaveFile/BaseData.json");
    public static readonly string SavePath = "Assets/Save/PData.json";
    public PlayerSaveData PlayerData
    {
        get => _playerData;
        set => _playerData = value;
    }
    [SerializeField]private PlayerSaveData _playerData = new PlayerSaveData();
    [field:SerializeField] public int StartingResin { get;private set; }
    //
    [field:SerializeField] public WeaponBaseSO[] StartingWeapons { get;private set; }
    [field:SerializeField] public BaseBuffSO[] StartingBuffs{ get;private set; }
    public Action OnSavePlayerData,OnLoadPlayerData;
    protected override void Awake()
    {
        base.Awake();
        StartingWeapons = new WeaponBaseSO[3];
        StartingBuffs = new BaseBuffSO[4];
        ItemDataBase.OnLoadDone += Load;
    }
    
    [ContextMenu("Save")]
    public void Save()
    {
        OnSavePlayerData?.Invoke();
        Inventory.Instance.Save(_playerData);
        PlayerData.SaveJson(SavePath);
    }
    [ContextMenu("Load")]
    public void Load()
    {
        //AndroidRequest();
        //Json.LoadJson(Path.Combine(Application.streamingAssetsPath, "SaveFile/BaseData.json"), out _playerData);
        //Inventory.Instance.Load(_playerData);
        OnLoadPlayerData?.Invoke();
    }

    private void AndroidRequest()
    {
        var loadingRequest = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "SaveFile/BaseData.json"));
        loadingRequest.SendWebRequest();
        while (!loadingRequest.isDone) {
            if (loadingRequest.isNetworkError || loadingRequest.isHttpError) {
                break;
            }
        }
        if (loadingRequest.isNetworkError || loadingRequest.isHttpError) {

        } else {
            File.WriteAllBytes(Path.Combine(Application.streamingAssetsPath, "SaveFile/BaseData.json"), loadingRequest.downloadHandler.data);
        }
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

        playerController.Resin = StartingResin;
    }
}