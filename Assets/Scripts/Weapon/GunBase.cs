using DG.Tweening;
using ResilientCore;
using System;
using System.Collections;
using Tech.Pooling;
using UnityEditor.Rendering;
using UnityEngine;

public class GunBase : WeaponBase
{
	public Transform ShootPoint;
	public bool IsFullCap { get { return Stats.GetAttribute(AttributeType.Bullets).Value == Stats.GetStat(StatType.MaxBulletCap).Value; } }
	public bool IsEmpty { get { return Stats.GetAttribute(AttributeType.Bullets).Value == 0; } }
	public float GunRecoil { get; protected set; } = 0f;
	public StatsController Stats { get; protected set; }
	public TriggerHandler GunOverlap { get; protected set; }
	
	protected bool gunReloadable = true;
	protected override void Awake()
	{
		base.Awake();
		Stats = GetComponent<StatsController>();
		GunOverlap = GetComponent<TriggerHandler>();
	}
	public override void Initialize()
	{
		SetBulletCap();
		Stats.GetAttribute(AttributeType.Bullets).SetValueToMax();
	}
	protected override void OnEnable()
	{
		base.OnEnable();
		InputEvent.OnInputReloadGun += ReloadGun;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		InputEvent.OnInputReloadGun -= ReloadGun;
	}
	public override void OnSwitchOut()
	{
		ResetRecoil();
		playerController.DisableLineRenderer();
		gunReloadable = false;
	}
	public override void OnSwitchIn()
	{
		playerController.EnableLineRenderer();
		gunReloadable = true;
	}

	private Tween temp;
	protected override void Update()
	{
		Vector2 rotateInput = PlayerInput.Instance.ShootStickInput;
		//Controller
		if (PlayerInput.Instance.IsAttackInput)
		{
			Shoot();
			return;
		}
		//Mobile
		if (rotateInput.magnitude > 0.875f)
		{
			if (!GunData.ReleaseToShoot) { Shoot(); }
			trigger = true;
		}
		else
		{
			if (IsEmpty)
			{
				ReloadGun();
			}

			trigger = false;
		}
	}
	public virtual void ResetRecoil()
	{
		temp.Kill();
		temp = DOVirtual.Float(GunRecoil, 0, GunData.RecoilResetTime, (x) => { GunRecoil = x; });
	}
	private void Rotate_canceled()
	{
		if (GunData.ReleaseToShoot && trigger) Shoot();
	}
	public override void Shoot()
	{
		if (!ShootAble ||
			Stats.GetAttribute(AttributeType.Bullets).Value <= 0 ||
			!repeatAble ||
			GunOverlap.IsTriggered) return;
		repeatAble = false;
		temp.Kill();
		Stats.GetAttribute(AttributeType.Bullets).Value--;
		PlayerEvent.OnShoot?.Invoke();
		DOVirtual.DelayedCall(GunData.ShootingSpeed/playerController.Stats.GetStat(StatType.ShootSpeed).Value, () => { repeatAble = true; ResetRecoil(); });
		GunSoundPlay();
		BulletInstantiate();
		GunRecoilUpdate();
	}
	public void ReloadGun()
	{
		if (!gunReloadable || IsFullCap) return;
		playerController.DisableLineRenderer();
		ShootAble = false;
		playerController.Animator.SetBool("ReloadGun", true);
		ResetRecoil();
	}

	private void GunSoundPlay()
	{
		SoundFXManager.Instance.PlayRandomSound(GunData.ShootingSounds,SoundType.Game);
		if(GunData.TailSound)
			SoundFXManager.Instance.PlaySound(GunData.TailSound,SoundType.Game);
	}

	public virtual void GunRecoilUpdate()
	{
		GunRecoil += GunData.Recoil;
		if (GunRecoil >= 1) { GunRecoil = 1f; }
	}
	public virtual void BulletInstantiate()
	{
		GameObject a = ObjectPool.Instance.SpawnObject(GunData.BulletPrefab, ShootPoint.position, transform.rotation, PoolType.GameObject);
		Bullet bullet = a.GetComponent<Bullet>();

		bullet.InitBullet(ShootPoint.position, GunData.SpreadMax * GunRecoil, DamageInfo.GetDamageInfo(GunData.Damage,playerController.Stats,playerController.gameObject));
	}
	public void SetBulletCap(float mul=1)
	{
		Stats.GetStat(StatType.MaxBulletCap).BaseValue = (int)(GunData.MaxCapacity * mul);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(base.GetHashCode(), GunData);
	}
}
