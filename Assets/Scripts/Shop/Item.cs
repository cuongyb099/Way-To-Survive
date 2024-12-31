
using UnityEngine;
using UnityEngine.Localization;

public abstract class Item : ScriptableObject
{
    public int ID => GetInstanceID();
    public LocalizedString Name;
    public LocalizedString Description;
    public Sprite Icon;
    public int BuyPrice;
    public int SellPrice;

    public abstract void Use(PlayerController player);
}
