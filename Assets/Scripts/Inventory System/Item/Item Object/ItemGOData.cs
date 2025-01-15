using Newtonsoft.Json;
using UnityEngine;

namespace KatInventory
{
    public class ItemGOData : ItemData
    {
        public ItemGOData(ItemBaseSO staticData, int quantity, GameObject go) : base(staticData, quantity)
        {
            GoReference = go;
        }

        [JsonIgnore]
        public GameObject GoReference { get; private set; }
    }
}