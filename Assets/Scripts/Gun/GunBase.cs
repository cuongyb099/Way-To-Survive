using DG.Tweening;
using ResilientCore;
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
	Sniper,
	SMG,
}
public class GunBase : MonoBehaviour
{
	public GunSO GunData;
	public Transform ShootPoint;
	public bool IsFullCap { get { return Stats.GetAttribute(AttributeType.Bullets).Value == Stats.GetStat(StatType.MaxBulletCap).Value; } }
	public bool ShootAble { get; set; } = true;
	public float GunRecoil { get; protected set; } = 0f;
	public StatsController Stats { get; protected set; }
	public TriggerHandler GunOverlap { get; protected set; }

	protected PlayerController playerController;
	protected bool repeatAble = true;
	protected bool trigger = false;
	private void Awake()
	{
		Stats = GetComponent<StatsController>();
		GunOverlap = GetComponent<TriggerHandler>();
		playerController = GetComponentInParent<PlayerController>();
	}
	public void Initialize()
	{
		SetBulletCap();
		Stats.GetAttribute(AttributeType.Bullets).SetValueToMax();
	}
	private void OnEnable()
	{
		InputEvent.OnShootStickCanceled += Rotate_canceled;
	}

	private void OnDisable()
	{
		InputEvent.OnShootStickCanceled -= Rotate_canceled;
	}

	private Tween temp;
	private void Update()
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
	public virtual void Shoot()
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
		BulletInstantiate();
		GunRecoilUpdate();
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

		float dmg = GunData.Damage * playerController.Stats.GetStat(StatType.ATK).Value;

		bool doesCrit = UnityEngine.Random.value < playerController.Stats.GetStat(StatType.CritRate).Value;
		float critDMG = playerController.Stats.GetStat(StatType.CritDamage).Value;
		bullet.InitBullet(ShootPoint.position, GunData.SpreadMax * GunRecoil, new DamageInfo(playerController.gameObject, dmg * (1f + (doesCrit ? critDMG : 0f)), doesCrit));
	}
	public void SetBulletCap(float mul=1)
	{
		Stats.GetStat(StatType.MaxBulletCap).BaseValue = (int)(GunData.MaxCapacity * mul);
	}
}
