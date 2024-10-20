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
        PlayerEvent.OnSwitchGuns += ChangeGun;
        PlayerEvent.OnShoot += UpdateGunAmmo;
		PlayerEvent.OnReload += UpdateGunAmmo;
	}
	private void OnDestroy()
	{
		PlayerEvent.OnSwitchGuns -= ChangeGun;
		PlayerEvent.OnShoot -= UpdateGunAmmo;
		PlayerEvent.OnReload -= UpdateGunAmmo;
	}
	public void UpdateGunAmmo()
    {
        if (gunAmmo == null) return;
        TextAmmo.text = $"{gunAmmo.Value} <size=70%><voffset=4.86135><color=#FFFFFF4C>/{gunAmmo.MaxValue}</color></voffset></size>";
    }
	public void ChangeGun(GunBase gun)
	{
		gunAmmo = gun.Stats.GetAttribute(AttributeType.Bullets);
        GunIcon.sprite = gun.GunData.Icon;
		TextGunName.text = gun.GunData.GunName;
        UpdateGunAmmo();
    }
}
