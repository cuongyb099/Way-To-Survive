using KatInventory;
using UnityEngine;

public class SwordData : ItemGOData
{
    public SwordData(ItemBaseSO staticData, int quantity, GameObject go) : base(staticData, quantity, go)
    {
    }

    //This Value Need Save So I Put Here
    //If Value Don't Need Save You Can Put It In MonoBehavior
    public float Durability = 100f;
}