using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tech.Json;
using Tech.Singleton;
using UnityEngine;

namespace KatInventory
{
    [RequireComponent(typeof(ItemDataBase))]
    public class Inventory : Singleton<Inventory>, ISaveable
    {
        [field: SerializeField, Range(1, 1000)]
        public int Capacity { get; private set;} = 99;
#if UNITY_EDITOR
        //I Don't Want this variable Can Get When Game Build It Not Compile If Access It Game Build Failure
        //Only Use For Debug
        [SerializeField]private List<ItemData> _inventory = new ();
        public List<ItemData> DataRuntime => _inventory;
        public static readonly string SavePath = "Assets/Save/Inventory.json";
#else
        public static readonly string SavePath = Application.persistentDataPath + "/Local1283012364.json";
#endif   
        

        public static Action OnInventoryChange;
        public static Action OnAddItem;
        private ItemData FindFirstItemNotFullStack(ItemBaseSO itemBase)
        {
            for (int i = 0; i < _inventory.Count; i++)
            {
                var data = _inventory[i];
                var staticData = data.StaticData;
                if (staticData != itemBase || data.Quantity == staticData.MaxStack) continue;
                    
                return data;
            }   
            
            return null;
        }
        public ItemData AddItem(ItemBaseSO itemBase, int quantity = 1)
        {
            if (!itemBase || quantity < 1) return null;
            
            var existItemData = FindFirstItemNotFullStack(itemBase);
            
            if (itemBase.Unique && existItemData != null)
            {
                return null;
            }

            if ((itemBase.Unique || existItemData == null) && _inventory.Count < Capacity)
            {
                return AddNewItem(itemBase, quantity);
            }

            if (existItemData == null)
            {
                return null;
            }
            
            var resultQuantity = quantity + existItemData.Quantity;
            if (resultQuantity > itemBase.MaxStack)
            {
                existItemData.Quantity = itemBase.MaxStack;
                OnInventoryChange?.Invoke();
                return existItemData;
            }
            
            existItemData.Quantity += quantity;
            OnInventoryChange?.Invoke();
            return existItemData;
        }
        private ItemData AddNewItem(ItemBaseSO itemBase, int quantity = 1)
        {
            var newItemData = itemBase.CreateItem(quantity);
            _inventory.Add(newItemData);
            OnInventoryChange?.Invoke();
            return newItemData;
        }
        public ItemData AddItem(string id, int quantity = 1)
        {
            if (id == string.Empty) return null;

            return AddItem(ItemDataBase.Instance.SearchItem(id), quantity);
        }
        public ItemData RemoveItem(ItemBaseSO itemBase, int quantity = 1)
        {
            if(!itemBase || quantity < 1) return null;

            
            for (int i = _inventory.Count - 1; i >= 0; i--)
            {
                var itemData = _inventory[i];
                
                if (itemData.StaticData != itemBase) continue;
                
                if (itemData.Quantity - quantity <= 0)
                {
                    _inventory.RemoveAt(i);
                    OnInventoryChange?.Invoke();
                    return itemData;
                }
                 
                itemData.Quantity -= quantity;
                OnInventoryChange?.Invoke();
                return itemData;
            }

            return null;
        }
        public ItemData RemoveItem(string id, int quantity = 1)
        {
            if (id == string.Empty) return null;
            
            return RemoveItem(ItemDataBase.Instance.SearchItem(id), quantity);
        }

        public void RemoveAllItemOfType<T>()
        {
            _inventory.RemoveAll(x => x is T);
            OnInventoryChange?.Invoke();
        }

        public void RemoveItems(ItemType type)
        {
            _inventory.RemoveAll(x => x.StaticData.GetItemType() == type);
            OnInventoryChange?.Invoke();
        }
        
        public bool Has(ItemBaseSO itemBaseSo)
        {
            for (int i = 0; i < _inventory.Count; i++)
            {
                var item = _inventory[i];
                if (item.StaticData == itemBaseSo)
                    return true;
            }

            return false;
        }
        
        public bool Has(string id)
        {
            for (int i = 0; i < _inventory.Count; i++)
            {
                var item = _inventory[i];
                if (item.StaticData.ID == id)
                    return true;
            }

            return false;
        }

        public List<ItemData> GetItems(ItemType itemType)
        {
            List<ItemData> result = new();
            for (int i = 0; i < _inventory.Count; i++)
            {
                var item = _inventory[i];
                
                if (item.StaticData.GetItemType() != itemType) continue;
                
                result.Add(item);
            }
            return result;
        }

        public List<T> GetItemsOfType<T>() where T : ItemData
        {
            return _inventory.OfType<T>().ToList();
        }

        public void Clear()
        {
            _inventory.Clear();
            OnInventoryChange?.Invoke();
        }
        
        public void Save()
        {
            _inventory.SaveJson(SavePath);
        }

        public void Load()
        {
            var json = Json.ReadAllText(SavePath);
           
            JArray itemsArray = JArray.Parse(json);
            
            if(itemsArray.Count == 0) return;
            
            _inventory.Clear();
            
            for (int i = 0; i < itemsArray.Count; i++)
            {
                var token = itemsArray[i];
                var itemData = ItemDataBase.Instance.SearchItem(token[ID]?.ToString()).CreateItem();
                
                JsonConvert.PopulateObject(itemsArray[i].ToString(), itemData);
                _inventory.Add(itemData);
            }
        }

        private static readonly string ID = "ID";
    }
}