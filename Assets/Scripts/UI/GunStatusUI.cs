using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GunStatusUI : MonoBehaviour
{
    private Attribute gunAmmo;
    public Image GunIcon;
	public TextMeshProUGUI TextAmmo;
	public TextMeshProUGUI TextGunName;

	private void Awake()
	{
        PlayerEvent.OnEquipWeapon += ChangeGun;
        PlayerEvent.OnShoot += UpdateGunAmmo;
		PlayerEvent.OnReload += UpdateGunAmmo;
		PlayerEvent.OnChangeCap += UpdateGunAmmo;
	}
	private void OnDestroy()
	{
		PlayerEvent.OnEquipWeapon -= ChangeGun;
		PlayerEvent.OnShoot -= UpdateGunAmmo;
		PlayerEvent.OnReload -= UpdateGunAmmo;
		PlayerEvent.OnChangeCap -= UpdateGunAmmo;

	}
	public void UpdateGunAmmo()
    {
	    if (gunAmmo == null)
		    TextAmmo.text = string.Empty;
        else 
		    TextAmmo.text = $"{gunAmmo.Value} <size=70%><voffset=4.86135><color=#FFFFFF4C>/{gunAmmo.MaxValue}</color></voffset></size>";
    }
	public void ChangeGun(WeaponBase weapon)
	{
		if (weapon.GunData.WeaponType != WeaponType.Knife)
		{
			GunBase gun = (GunBase)weapon;
			gunAmmo = gun.Stats.GetAttribute(AttributeType.Bullets);
		}
		else
			gunAmmo = null;
        GunIcon.sprite = weapon.GunData.Icon;
		TextGunName.text = weapon.GunData.GunName;
        UpdateGunAmmo();
    }
}
