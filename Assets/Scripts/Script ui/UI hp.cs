using UnityEngine;
using UnityEngine.UI;

public class PlayerAmmo : MonoBehaviour
{
    public int maxAmmo = 30;
    private int currentAmmo;
    public Text ammoCountText;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoCount();
    }

    public void UseAmmo(int amount)
    {
        currentAmmo -= amount;
        if (currentAmmo < 0) currentAmmo = 0;
        UpdateAmmoCount();
    }

    public void Reload(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > maxAmmo) currentAmmo = maxAmmo;
        UpdateAmmoCount();
    }

    private void UpdateAmmoCount()
    {
        ammoCountText.text = currentAmmo.ToString();
    }
}