using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemMiniUI : MonoBehaviour
{
    public TextMeshProUGUI ItemName;
    public Image ItemImage;
    public Button ItemButton { get; private set; }
    public Item ItemHolder { get;private set; }

    private void Awake()
    {
        ItemButton = GetComponent<Button>();
    }

    public void Initialize(Item item)
    {
        if (item == null)
        {
            ItemHolder = null;
            ItemName.text = "";
            ItemImage.sprite = null;
            ItemImage.color = new Color(0, 0, 0, 0);
            return;
        }
        ItemHolder = item;
        ItemName.text = item.Name.GetLocalizedString();
        ItemImage.sprite = item.Icon;
        ItemImage.color = new Color(1, 1, 1, 1);
    }
}
