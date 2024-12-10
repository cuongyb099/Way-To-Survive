using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public PlayerController PlayerController { get; private set; }
	private void Awake()
	{
		PlayerController = GetComponentInParent<PlayerController>();
	}
	//Weapons
	public void EnableShooting()
	{
		PlayerController.Animator.SetBool("SwitchWeapon", false);
		PlayerController.AfterSwitching();
	}
	public void DisableShooting()
	{
		PlayerController.Animator.SetBool("SwitchWeapon", true);
		PlayerController?.BeforeSwitching();
	}
	//Gun
	public void AfterReload()
	{
		PlayerController.Animator.SetBool("ReloadGun", false);
		PlayerController.AfterReload();
	}
	public void BeforeReload()
	{
		PlayerController.Animator.SetBool("ReloadGun", true);
		PlayerController?.BeforeSwitching();
	}
	public void DropMagazine()
	{
		GunBase gun = (GunBase)PlayerController.CurrentWeapon;
		gun.DropMagazine();
	}

	public void DropShell()
	{
		GunBase gun = (GunBase)PlayerController.CurrentWeapon;
		gun.DropShell();
	}

	public void TakeMagazine()
	{
		GunBase gun = (GunBase)PlayerController.CurrentWeapon;
		gun.TakeMagazine();
	}

	public void PutInMagazine()
	{
		GunBase gun = (GunBase)PlayerController.CurrentWeapon;
		gun.PutInMagazine();
	}
	//Sounds
	public void CockingSound()
	{
		AudioManager.Instance.PlaySound(PlayerController.CurrentWeapon.GunData.CockingSound,volumeType: SoundVolumeType.SOUNDFX_VOLUME);
	}
	public void MagSoundIn()
	{
		AudioManager.Instance.PlaySound(PlayerController.CurrentWeapon.GunData.MagSoundIn,volumeType: SoundVolumeType.SOUNDFX_VOLUME);
	}
	public void MagSoundOut()
	{
		AudioManager.Instance.PlaySound(PlayerController.CurrentWeapon.GunData.MagSoundOut,volumeType: SoundVolumeType.SOUNDFX_VOLUME);
	}
}
