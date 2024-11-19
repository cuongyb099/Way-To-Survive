using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerAmmo : MonoBehaviour
{
    [Header("Ammo Settings")]
    [SerializeField] private int maxAmmo = 30;  // Số đạn tối đa
    private int currentAmmo;                     // Số đạn hiện tại

    [Header("UI References")]
    [SerializeField] private Text ammoCountText; // Text hiển thị số đạn
    [SerializeField] private Image ammoBar;      // Thanh hiển thị số đạn

    [Header("Ammo Types")]
    [SerializeField] private List<string> ammoTypes; // Danh sách các loại đạn
    private int currentAmmoTypeIndex = 0;          // Chỉ số loại đạn hiện tại

    private void Start()
    {
        currentAmmo = maxAmmo;                   // Khởi tạo số đạn
        UpdateAmmoUI();                          // Cập nhật UI
    }

    public void UseAmmo(int amount)
    {
        ChangeAmmo(-amount);                     // Giảm số đạn
    }

    public void Reload(int amount)
    {
        ChangeAmmo(amount);                      // Tăng số đạn
    }

    private void ChangeAmmo(int amount)
    {
        currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, maxAmmo); // Cập nhật số đạn
        UpdateAmmoUI();                          // Cập nhật UI
    }

    private void UpdateAmmoUI()
    {
        ammoCountText.text = $"Đạn: {currentAmmo} ({ammoTypes[currentAmmoTypeIndex]})"; // Cập nhật text với loại đạn
        ammoBar.fillAmount = (float)currentAmmo / maxAmmo; // Cập nhật thanh đạn
    }

    public void SwitchAmmoType()
    {
        currentAmmoTypeIndex = (currentAmmoTypeIndex + 1) % ammoTypes.Count; // Chuyển đổi loại đạn
        UpdateAmmoUI(); // Cập nhật UI với loại đạn mới
    }
}