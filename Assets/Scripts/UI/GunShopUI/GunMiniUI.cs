using System;
using System.Collections;
using System.Collections.Generic;
using KatInventory;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemMiniUI : MonoBehaviour
{
    public TextMeshProUGUI ItemName;
    public Image ItemImage;
    public Button ItemButton { get; private set; }
    public ItemBaseSO ItemBaseSoHolder { get;private set; }

    public void Initialize(ItemBaseSO itemBaseSo)
    {
        if(ItemButton == null)
            ItemButton = GetComponent<Button>();
        if (itemBaseSo == null)
        {
            ItemBaseSoHolder = null;
            ItemName.text = "";
            ItemImage.sprite = null;
            ItemImage.color = new Color(0, 0, 0, 0);
            return;
        }
        ItemBaseSoHolder = itemBaseSo;
        ItemName.text = itemBaseSo.Name.GetLocalizedString();
        ItemImage.sprite = itemBaseSo.Icon;
        ItemImage.color = new Color(1, 1, 1, 1);
    }
}
