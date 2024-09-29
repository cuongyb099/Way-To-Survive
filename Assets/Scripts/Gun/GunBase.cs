using DG.Tweening;
using System;
using System.Collections;
using Tech.Pooling;
using UnityEditor.Rendering;
using UnityEngine;

public enum WeaponType
{
	Pistol,
	Rifle,
	Shotgun,
	Sniper
}
public class GunBase : MonoBehaviour
{
	public GunSO GunData;
	public Transform ShootPoint;
	public Transform HandlePoint;
	public bool ShootAble { get; set; } = true;
	public Action OnShoot { get; set; }
	public float GunRecoil { get; private set; } = 0f;

	private PlayerController playerController;
	private bool repeatAble = true;
	private bool trigger = false;
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
		temp = DOVirtual.Float(GunRecoil, 0, GunData.RecoilResetTime, (x) => { GunRecoil = x; });
    }
    private void Rotate_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		if (GunData.ReleaseToShoot && trigger) Shoot();
	}
	public virtual void Shoot()
	{
		if (!ShootAble) return;
		if (!repeatAble) return;
		repeatAble = false;

		temp.Kill();
		OnShoot.Invoke();
		DOVirtual.DelayedCall(GunData.ShootingSpeed,() => { repeatAble = true; });
        GameObject a = ObjectPool.Instance.SpawnObject(GunData.BulletPrefab, ShootPoint.position, transform.rotation,PoolType.GameObject);
		Bullet bullet = a.GetComponent<Bullet>();

		float dmg = GunData.Damage * playerController.Stats.GetStat(StatType.ATK).Value;

        bullet.InitBullet(ShootPoint.position,GunData.SpreadMax * GunRecoil, new DamageInfo(playerController.gameObject,dmg));
		if (GunRecoil >= 1) { GunRecoil = 1f; }
			GunRecoil += GunData.Recoil;
	}
}
