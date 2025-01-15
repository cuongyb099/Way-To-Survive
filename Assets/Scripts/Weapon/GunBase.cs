using DG.Tweening;
using ResilientCore;
using System;
using System.Collections;
using Tech.Pooling;
using UnityEditor.Rendering;
using UnityEngine;

public class GunBase : WeaponBase
{
	[field: SerializeField] public Transform ShootPoint { get; private set; }
	[field: SerializeField] public Transform ShellDropPoint { get; private set; }
	[field: SerializeField] public GameObject MagObject { get; private set; }

	public bool IsFullCap { get { return Stats.GetAttribute(AttributeType.Bullets).Value == Stats.GetStat(StatType.MaxBulletCap).Value; } }
	public bool IsEmpty { get { return Stats.GetAttribute(AttributeType.Bullets).Value == 0; } }
	public float GunAccuracy
	{
		get { return GunRecoil * WeaponData.SpreadMax * playerController.Stats.GetStat(StatType.MaxSpreadReduce).Value; }
	}
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
		MagObject.SetActive(true);
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
			if (!WeaponData.ReleaseToShoot) { Shoot(); }
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
		temp = DOVirtual.Float(GunRecoil, 0, WeaponData.RecoilResetTime, (x) => { GunRecoil = x; });
	}
	private void Rotate_canceled()
	{
		if (WeaponData.ReleaseToShoot && trigger) Shoot();
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
		PlayerEvent.OnAttack?.Invoke();
		DOVirtual.DelayedCall(WeaponData.ShootingSpeed/playerController.Stats.GetStat(StatType.ATKSpeed).Value, () => { repeatAble = true; ResetRecoil(); });
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
		AudioManager.Instance.PlaySound(WeaponData.ShootingSounds.ToArray(),volumeType: SoundVolumeType.SOUNDFX_VOLUME);
		if(WeaponData.TailSound)
			AudioManager.Instance.PlaySound(WeaponData.TailSound,volumeType: SoundVolumeType.SOUNDFX_VOLUME);
	}
	
	public virtual void GunRecoilUpdate()
	{
		GunRecoil += WeaponData.Recoil* playerController.Stats.GetStat(StatType.RecoilReduce).Value;
		if (GunRecoil >= 1) { GunRecoil = 1f; return;}
		if (GunRecoil < 0) { GunRecoil = 0f; return;}
	}
	public virtual void BulletInstantiate()
	{
		GameObject a = ObjectPool.Instance.SpawnObject(WeaponData.BulletPrefab, ShootPoint.position, transform.rotation, PoolType.GameObject);
		Bullet bullet = a.GetComponent<Bullet>();

		bullet.InitBullet(ShootPoint.position, GunAccuracy, DamageInfo.GetDamageInfo(WeaponData.Damage,playerController.Stats, DamageType.Bullet));
	}
	public void SetBulletCap(float mul=1)
	{
		Stats.GetStat(StatType.MaxBulletCap).BaseValue = (int)(WeaponData.MaxCapacity * mul);
	}

	public void SetBulletToMax()
	{
		Stats.GetAttribute(AttributeType.Bullets).SetValueToMax();
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(base.GetHashCode(), WeaponData);
	}
	//Animation Methods
	public void DropMagazine()
	{
		GameObject mag = ObjectPool.Instance.SpawnObject(WeaponData.MagPrefab,MagObject.transform.position,MagObject.transform.rotation, PoolType.GameObject);
		Rigidbody rb = mag.GetComponentInChildren<Rigidbody>();
		rb.velocity = playerController.Rigidbody.velocity;
		MagObject.SetActive(false);
	}

	public void DropShell()
	{
		GameObject shell = ObjectPool.Instance.SpawnObject(WeaponData.ShellPrefab,ShellDropPoint.transform.position,transform.rotation* Quaternion.Euler(UnityEngine.Random.Range(0,60),10,0), PoolType.GameObject);
		Rigidbody rb = shell.GetComponent<Rigidbody>();
		rb.velocity = playerController.Rigidbody.velocity;
		rb.AddForce(Quaternion.Euler(0,-90,0)*shell.transform.forward*2f, ForceMode.VelocityChange);
	}

	public void TakeMagazine()
	{
		
	}

	public void PutInMagazine()
	{
		MagObject.SetActive(true);
	}
}
