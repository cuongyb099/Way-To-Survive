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
		PlayerController.EnableAfterSwitching();
	}
	public void DisableShooting()
	{
		PlayerController.Animator.SetBool("SwitchWeapon", true);
		PlayerController?.DisableBeforeSwitching();
	}
	public void AfterReload()
	{
		PlayerController.Animator.SetBool("ReloadGun", false);
		PlayerController.AfterReload();
	}
	public void BeforeReload()
	{
		PlayerController.Animator.SetBool("ReloadGun", true);
		PlayerController?.DisableBeforeSwitching();
	}
	//Sounds
	public void CockingSound()
	{
		SoundFXManager.Instance.PlaySound(PlayerController.CurrentGun.GunData.CockingSound,SoundType.Game);
	}
	public void MagSoundIn()
	{
		SoundFXManager.Instance.PlaySound(PlayerController.CurrentGun.GunData.MagSoundIn,SoundType.Game);
	}
	public void MagSoundOut()
	{
		SoundFXManager.Instance.PlaySound(PlayerController.CurrentGun.GunData.MagSoundOut,SoundType.Game);
	}
}
