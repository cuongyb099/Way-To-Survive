using UnityEngine;
using UnityEngine.UI;

public class PlayerAmmo : MonoBehaviour
{
    public int maxAmmo = 30;           // Số đạn tối đa
    private int currentAmmo;           // Số đạn hiện tại
    public Text ammoCountText;         // Text hiển thị số đạn
    public Image ammoBar;              // Thanh hiển thị số đạn

    // Khởi tạo giá trị
    void Start()
    {
        currentAmmo = maxAmmo;         // Gán số đạn hiện tại bằng số đạn tối đa
        UpdateAmmoUI();                // Cập nhật UI
    }

    // Phương thức sử dụng đạn
    public void UseAmmo(int amount)
    {
        currentAmmo -= amount;          // Giảm số đạn
        if (currentAmmo < 0) currentAmmo = 0; // Không cho số đạn nhỏ hơn 0
        UpdateAmmoUI();                 // Cập nhật UI
    }

    // Phương thức nạp đạn
    public void Reload(int amount)
    {
        currentAmmo += amount;          // Tăng số đạn
        if (currentAmmo > maxAmmo) currentAmmo = maxAmmo; // Không cho số đạn vượt quá tối đa
        UpdateAmmoUI();                 // Cập nhật UI
    }

    // Cập nhật UI cho số đạn
    private void UpdateAmmoUI()
    {
        ammoCountText.text = "Đạn: " + currentAmmo.ToString(); // Cập nhật text
        ammoBar.fillAmount = (float)currentAmmo / maxAmmo;     // Cập nhật thanh đạn
    }
}