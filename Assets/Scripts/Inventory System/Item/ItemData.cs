using Newtonsoft.Json;
using UnityEngine;

namespace KatInventory
{
    [System.Serializable]
    public class ItemData
    {
        public ItemData()
        {
            
        }
        public ItemData(ItemBaseSO staticData, int quantity)
        {
            Quantity = quantity;
            StaticData = staticData;
        }

        [field: SerializeField]
        [JsonIgnore]
        public ItemBaseSO StaticData { get; private set; }
        [JsonProperty("ID", Order = 0)]
        public string ID => StaticData.ID;

        [JsonProperty("Quantity", Order = 1)] 
        public int Quantity;
    }
}
