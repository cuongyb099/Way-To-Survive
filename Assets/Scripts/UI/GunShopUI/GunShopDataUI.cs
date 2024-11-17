using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunShopDataUI : MonoBehaviour
{
    public TextMeshProUGUI GunName;
    public TextMeshProUGUI GunPrice;
    public TextMeshProUGUI Capacity;
    public Image GunImage;
    public Slider DamageSlider;
    public Slider RecoilSlider;
    public Slider AimSlider;
    public Slider RPMSlider;
    public Slider WeightSlider;
    public Slider CapacitySlider;

    public float AnimTime = 0.25f;
    public void ChangeGun(GunSO gun)
    {
        GunName.text = gun.GunName;
        GunPrice.text = $"<color=#1BDF00>{gun.GunPrice}$</color>";
        Capacity.text = gun.MaxCapacity.ToString();
        GunImage.sprite = gun.Icon;
        DOVirtual.Float(DamageSlider.value, gun.Damage / 10f, AnimTime, (x) => { DamageSlider.value = x;}).SetUpdate(true);
        //15f is the maximum spread angle
        DOVirtual.Float(RecoilSlider.value, gun.Recoil/gun.ShootingSpeed*gun.SpreadMax/15f, AnimTime, (x) => { RecoilSlider.value = x;}).SetUpdate(true);
        DOVirtual.Float(AimSlider.value, gun.Aim/30f, AnimTime, (x) => { AimSlider.value = x;}).SetUpdate(true);
        //1min:1000RPM=0.06
        DOVirtual.Float(RPMSlider.value, 0.06f/gun.ShootingSpeed, AnimTime, (x) => { RPMSlider.value = x;}).SetUpdate(true);
        DOVirtual.Float(WeightSlider.value, gun.Weight/3f, AnimTime, (x) => { WeightSlider.value = x;}).SetUpdate(true);
        DOVirtual.Float(CapacitySlider.value, gun.MaxCapacity / 100f, AnimTime, (x) => { CapacitySlider.value = x;}).SetUpdate(true);
    }
}
