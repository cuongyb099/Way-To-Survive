using Tech.Logger;
using UnityEngine;

namespace KatInventory
{
    public abstract class ItemGOBaseSO : ItemBaseSO
    {
        [field: SerializeField]
        public ItemBase Prefab  { get; private set; }
        public override ItemData CreateItem(int quantity = 1)
        {
            if (!Prefab)
            {
                LogCommon.LogError("Prefab Is Null");
                return null;
            }
            var item = Instantiate(Prefab, Inventory.Instance.transform);
            var data = CreateItemData(quantity, item.gameObject);
            Prefab.SetData(data);
            item.gameObject.SetActive(false);
            return data;
        }

        public abstract ItemGOData CreateItemData(int quantity, GameObject prefab);
    }
}