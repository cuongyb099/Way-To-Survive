using UnityEngine;

public class WheelHandlerUI : MonoBehaviour
{
    public GameObject InventoryUI;
    public ItemWheelUI[] Items;
    private void Start()
    {
       InitalizeItems();
               
       for (int i = 0; i < Items.Length; i++)
       {
           ItemWheelUI item = Items[i];
           item.ItemButton.onClick.AddListener(()=>
           {
               HandleWheelSelection(item.ID);
               gameObject.SetActive(false);
           });
       }
    }

    public void InitalizeItems()
    {
        GunBase[] guns = GameManager.Instance.Player.Guns;
        //0->2 is for guns
        for (int i = 0; i < 3; i++)
        {
            Items[i].Initialize(i, guns[i]!=null?guns[i].GunData.GunName : null,guns[i]!=null?guns[i].GunData.Icon : null);
        }
        Items[3].Initialize(3,"Inventory",null);
    }

    public void HandleWheelSelection(int wheelIndex)
    {
        switch (wheelIndex)
        {
            case 0:
                GameManager.Instance.Player.SwitchGun(0);
                break;
            case 1:
                GameManager.Instance.Player.SwitchGun(1);
                break;
            case 2:
                GameManager.Instance.Player.SwitchGun(2);
                break;
            case 3:
                InventoryUI.gameObject.SetActive(true);
                break;
        }
    }
}
