
using System;
using System.Collections.Generic;
using KatInventory;

[Serializable]
public class PlayerSaveData
{
    public string UID;
    public string Username;
    public float Money;
    public List<ItemData> Inventory ;

    public PlayerSaveData()
    {
        Inventory = new List<ItemData>();
    }
}
