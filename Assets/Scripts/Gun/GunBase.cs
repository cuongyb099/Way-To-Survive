using ResilientCore;
using System.Collections;
using System.Collections.Generic;
using Tech.Pooling;
using UnityEngine;

public class GunBase : MonoBehaviour
{
	public GunSO GunData;
	public Transform ShootPoint;
	public Transform HandlePoint;
	public LineRendererHelper RendererHelper;
	private bool shootAble = true;
	private bool trigger = false;
	private PlayerController playerController;
	private void Awake()
	{
		RendererHelper.AimAmmount = GunData.Aim;
		playerController = GetComponentInParent<PlayerController>();
	}
	private void OnEnable()
	{
		PlayerInput.Instance.PlayerControlActions.Rotate.canceled += Rotate_canceled;
	}
	private void OnDisable()
	{
		if (PlayerInput.Instance == null) return;
		PlayerInput.Instance.PlayerControlActions.Rotate.canceled -= Rotate_canceled;
	}

	private void Update()
	{
		Vector2 rotateInput = PlayerInput.Instance.RotationInput;
		if (rotateInput.magnitude > 0.875f)
		{
			if (!GunData.ReleaseToShoot) { Shoot(); return; }
			trigger = true;
		}
		else 
			trigger = false;
	}

	private void Rotate_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		if (GunData.ReleaseToShoot && trigger) Shoot();
	}

	public virtual void Shoot()
	{
		if (!shootAble) return;
		Debug.Log("shoot");
		StartCoroutine(shootCoolDown());
		GameObject a = ObjectPool.Instance.SpawnObject(GunData.BulletPrefab, ShootPoint.position, transform.rotation);
		Bullet bullet = a.GetComponent<Bullet>();
		bullet.InitBullet(ShootPoint.position,GunData.Accuracy,new DamageInfo(playerController.gameObject,GunData.Damage));
	}
	private IEnumerator shootCoolDown()
	{
		shootAble = false;
		yield return new WaitForSeconds(GunData.ShootingSpeed);
		shootAble = true;
	}
}
