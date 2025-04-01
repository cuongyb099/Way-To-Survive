using System.Collections;
using System.Collections.Generic;
using KatInventory;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItemUI : MonoBehaviour
{
    [SerializeField] private Image ItemImage;
    [SerializeField] private Image BackgroundImage;
    public WeaponBaseSO ItemBaseSoHolder { get; private set; }
    
    public void Initialize(WeaponBaseSO itemBaseSo)
    {
        if (!itemBaseSo)
        {
            ItemBaseSoHolder = null;
            ItemImage.sprite = null;
            ItemImage.color = new Color(0, 0, 0, 0);
            BackgroundImage.sprite = GameDataManager.Instance.ItemRarityBackground[Rarity.Common];
            return;
        }
        ItemBaseSoHolder = itemBaseSo;
        ItemImage.sprite = itemBaseSo.Icon;
        ItemImage.color = new Color(1, 1, 1, 1);
        BackgroundImage.sprite = GameDataManager.Instance.ItemRarityBackground[itemBaseSo.Rarity];
    }
}
