using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private Transform itemGrid;
    [SerializeField] private Text itemInfoText;

    private List<Item> inventory = new List<Item>();

    private void Start()
    {
        inventoryPanel.SetActive(false); // Ẩn inventory ban đầu
        PopulateInventory();
        UpdateInventoryUI();
    }

    private void PopulateInventory()
    {
        // Thêm một số vật phẩm mẫu vào kho
        inventory.Add(new Item("Healing Potion", "Restores 50 health", null));
        inventory.Add(new Item("Mana Potion", "Restores 30 mana", null));
        inventory.Add(new Item("Sword", "A basic sword", null));
        // Thêm nhiều vật phẩm hơn nếu cần
    }

    private void UpdateInventoryUI()
    {
        // Xóa tất cả slot hiện tại
        foreach (Transform child in itemGrid)
        {
            Destroy(child.gameObject);
        }

        // Tạo slot cho từng vật phẩm trong inventory
        foreach (var item in inventory)
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemGrid);
            Button button = slot.GetComponent<Button>();
            button.onClick.AddListener(() => ShowItemInfo(item));
            slot.GetComponentInChildren<Image>().sprite = item.icon; // Cập nhật hình ảnh nếu có
            slot.GetComponentInChildren<Text>().text = item.name; // Cập nhật tên vật phẩm
        }
    }

    private void ShowItemInfo(Item item)
    {
        itemInfoText.text = $"{item.name}\n\n{item.description}";
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}

// Lớp đại diện cho vật phẩm
[System.Serializable]
public class Item
{
    public string name;
    public string description;
    public Sprite icon; // Hình ảnh vật phẩm (có thể null nếu không có)

    public Item(string name, string description, Sprite icon)
    {
        this.name = name;
        this.description = description;
        this.icon = icon;
    }
}