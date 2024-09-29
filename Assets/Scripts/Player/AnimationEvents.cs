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
		PlayerController.EnableShooting();
	}
	public void DisableShooting()
	{
		PlayerController?.DisableShooting();
	}
}
