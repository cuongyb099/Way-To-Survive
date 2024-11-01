using UnityEngine;
using UnityEngine.UI;

public class PlayerAmmo : MonoBehaviour
{
    public int maxAmmo = 30;
    private int currentAmmo;
    public Image ammoBar;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoBar();
    }

    public void UseAmmo(int amount)
    {
        currentAmmo -= amount;
        if (currentAmmo < 0) currentAmmo = 0;
        UpdateAmmoBar();
    }

    public void Reload(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > maxAmmo) currentAmmo = maxAmmo;
        UpdateAmmoBar();
    }

    private void UpdateAmmoBar()
    {
        ammoBar.fillAmount = (float)currentAmmo / maxAmmo;
    }
}