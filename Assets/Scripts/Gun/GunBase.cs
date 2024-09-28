using DG.Tweening;
using System;
using System.Collections;
using Tech.Pooling;
using UnityEditor.Rendering;
using UnityEngine;

public enum WeaponType
{
	Rifle,
	Shotgun,
	Pistol,
	Sniper
}
public class GunBase : MonoBehaviour
{
	public GunSO GunData;
	public Transform ShootPoint;
	public Transform HandlePoint;
	public Action OnShoot { get; set; }

	private PlayerController playerController;
	private bool shootAble = true;
	private bool trigger = false;
	private bool isFocus = true;
	private float gunRecoil = 0f;
	private void Awake()
	{
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
	private Tween temp;
	private void Update()
	{
		Vector2 rotateInput = PlayerInput.Instance.RotationInput;
		if (rotateInput.magnitude > 0.875f)
		{
			temp.Kill();
			if (!GunData.ReleaseToShoot) { Shoot();}
			trigger = true;
		}
		else
		{
			if (trigger)
			{
				ResetRecoil();
			}
			trigger = false;
		}
	}

    public void ResetRecoil()
    {
        temp = DOVirtual.DelayedCall(GunData.RecoilResetTime, () => { gunRecoil = 0f; });
    }
    private void Rotate_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		if (GunData.ReleaseToShoot && trigger) Shoot();
	}
	public virtual void Shoot()
	{
		if (!shootAble) return;
		shootAble = false;

		OnShoot.Invoke();
		DOVirtual.DelayedCall(GunData.ShootingSpeed,() => { shootAble = true; });
        GameObject a = ObjectPool.Instance.SpawnObject(GunData.BulletPrefab, ShootPoint.position, transform.rotation,PoolType.GameObject);
		Bullet bullet = a.GetComponent<Bullet>();

		float dmg = GunData.Damage * playerController.Stats.GetStat(StatType.ATK).Value;

        bullet.InitBullet(ShootPoint.position,GunData.SpreadMax * gunRecoil, new DamageInfo(playerController.gameObject,dmg));
		if (gunRecoil >= 1) { gunRecoil = 1f; }
			gunRecoil += GunData.Recoil;
	}
}
