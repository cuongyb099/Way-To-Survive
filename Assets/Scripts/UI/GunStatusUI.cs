using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GunStatusUI : MonoBehaviour
{
    private Attribute gunAmmo;
    private Attribute holdingAmmo;
    public Image GunIcon;
	public TextMeshProUGUI TextAmmo;
	public TextMeshProUGUI TextGunName;
	private LocalizedString CurrentGunName;
	private void Awake()
	{
        PlayerEvent.OnEquipWeapon += ChangeGun;
        PlayerEvent.OnAttack += UpdateGunAmmo;
		PlayerEvent.OnReload += UpdateGunAmmo;
		PlayerEvent.OnChangeCap += UpdateGunAmmo;
		LocalizationSettings.SelectedLocaleChanged += UpdateGunName;
	}

	private void Start()
	{
		ChangeGun(GameManager.Instance.Player.CurrentWeapon);
	}

	private void OnDestroy()
	{
		PlayerEvent.OnEquipWeapon -= ChangeGun;
		PlayerEvent.OnAttack -= UpdateGunAmmo;
		PlayerEvent.OnReload -= UpdateGunAmmo;
		PlayerEvent.OnChangeCap -= UpdateGunAmmo;
		LocalizationSettings.SelectedLocaleChanged -= UpdateGunName;
	}
	public void UpdateGunAmmo()
    {
	    if (gunAmmo == null)
		    TextAmmo.text = string.Empty;
        else 
		    TextAmmo.text = $"{gunAmmo.Value} <size=70%><voffset=4.86135><color=#FFFFFF8C>/{holdingAmmo.Value}</color></voffset></size>";
    }
	public void ChangeGun(WeaponBase weapon)
	{
		if (weapon.WeaponData.WeaponType != WeaponType.Knife)
		{
			GunBase gun = (GunBase)weapon;
			gunAmmo = gun.Stats.GetAttribute(AttributeType.Bullets);
			holdingAmmo = GameManager.Instance.Player.Stats.GetAttribute(AttributeType.HoldingBullets);
		}
		else
			gunAmmo = null;
        GunIcon.sprite = weapon.WeaponData.Icon;
        CurrentGunName = weapon.WeaponData.Name;
		TextGunName.text = CurrentGunName.GetLocalizedString();
        UpdateGunAmmo();
    }

	private void UpdateGunName(Locale locale)
	{
		TextGunName.text = CurrentGunName.GetLocalizedString();
	}
}
