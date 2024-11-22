using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image weaponImage;    // Hình ảnh súng
    [SerializeField] private Text ammoCountText;    // Hiển thị số đạn
    [SerializeField] private Button reloadButton;     // Nút thay đạn

    private int currentAmmo = 30; // Số đạn hiện tại
    private int maxAmmo = 30;      // Số đạn tối đa

    private void Start()
    {
        UpdateAmmoCount();
        reloadButton.onClick.AddListener(Reload);
    }

    private void UpdateAmmoCount()
    {
        ammoCountText.text = $"{currentAmmo}/{maxAmmo}"; // Cập nhật hiển thị số đạn
    }

    private void Reload()
    {
        currentAmmo = maxAmmo; // Nạp đạn đầy
        UpdateAmmoCount();
    }

    public void UseAmmo(int amount)
    {
        currentAmmo = Mathf.Max(currentAmmo - amount, 0); // Giảm số đạn
        UpdateAmmoCount();
    }

    public void SetWeaponImage(Sprite newWeaponSprite)
    {
        weaponImage.sprite = newWeaponSprite; // Cập nhật hình ảnh súng
    }
}