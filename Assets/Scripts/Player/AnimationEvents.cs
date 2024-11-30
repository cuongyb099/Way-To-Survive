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
