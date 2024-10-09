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
		PlayerController.EnableShooting();
	}
	public void DisableShooting()
	{
		PlayerController.Animator.SetBool("SwitchWeapon", true);
		PlayerController?.DisableShooting();
	}
	public void AfterReload()
	{
		PlayerController.Animator.SetBool("SwitchWeapon", false);
		PlayerController.EnableShooting();
	}
	public void BeforeReload()
	{
		PlayerController.Animator.SetBool("SwitchWeapon", true);
		PlayerController?.DisableShooting();
	}
}
