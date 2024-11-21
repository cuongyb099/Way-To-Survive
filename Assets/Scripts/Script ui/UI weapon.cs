using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Text weaponNameText; // Text hiển thị tên vũ khí
    [SerializeField] private Text ammoCountText;   // Text hiển thị số đạn
    [SerializeField] private Image weaponIcon;      // Hình ảnh biểu tượng vũ khí

    [Header("Weapon Settings")]
    [SerializeField] private string weaponName = "Pistol"; // Tên vũ khí
    [SerializeField] private int currentAmmo = 30;          // Số đạn hiện tại
    [SerializeField] private int maxAmmo = 30;              // Số đạn tối đa
    [SerializeField] private Sprite weaponSprite;           // Biểu tượng vũ khí

    private void Start()
    {
        UpdateWeaponUI(); // Cập nhật giao diện khi bắt đầu
    }

    // Cập nhật UI với thông tin vũ khí
    public void UpdateWeaponUI()
    {
        weaponNameText.text = weaponName; // Cập nhật tên vũ khí
        ammoCountText.text = $"{currentAmmo}/{maxAmmo}"; // Cập nhật số đạn
        weaponIcon.sprite = weaponSprite; // Cập nhật biểu tượng vũ khí
    }

    // Phương thức để sử dụng đạn
    public void UseAmmo(int amount)
    {
        currentAmmo = Mathf.Max(currentAmmo - amount, 0); // Giảm số đạn
        UpdateWeaponUI(); // Cập nhật giao diện
    }

    // Phương thức để nạp đạn
    public void Reload(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo); // Tăng số đạn
        UpdateWeaponUI(); // Cập nhật giao diện
    }

    // Phương thức để thay đổi vũ khí
    public void ChangeWeapon(string newName, int newMaxAmmo, Sprite newSprite)
    {
        weaponName = newName;
        maxAmmo = newMaxAmmo;
        currentAmmo = maxAmmo; // Đặt lại số đạn khi thay đổi vũ khí
        weaponSprite = newSprite;
        UpdateWeaponUI(); // Cập nhật giao diện
    }
}