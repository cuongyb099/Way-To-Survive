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
    public bool IsFullCap { get { return Stats.GetAttribute(AttributeType.Bullets).Value == Stats.GetStat(StatType.MaxBulletCap).Value; } }
    public bool ShootAble { get; set; } = true;
	public Action OnShoot { get; set; }
	public float GunRecoil { get; private set; } = 0f;
	public StatsController Stats { get; private set; }

	private PlayerController playerController;
	private bool repeatAble = true;
	private bool trigger = false;
	private void Awake()
	{
		Stats = GetComponent<StatsController>();
		playerController = GetComponentInParent<PlayerController>();
	}
    public void Initialize()
    {
		Stats.GetStat(StatType.MaxBulletCap).BaseValue = GunData.MaxCapacity;
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
		if(PlayerInput.Instance.IsAttackInput)
		{
			Shoot();
			return;
		}
		//Mobile
		if (rotateInput.magnitude > 0.875f)
		{
			if (!GunData.ReleaseToShoot) { Shoot();}
			trigger = true;
		}
		else
		{
			trigger = false;
		}
	}

    public void ResetRecoil()
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
			Stats.GetAttribute(AttributeType.Bullets).Value<= 0 ||
            !repeatAble) return;
		repeatAble = false;

		temp.Kill();
		OnShoot.Invoke();
		Stats.GetAttribute(AttributeType.Bullets).Value--;
		DOVirtual.DelayedCall(GunData.ShootingSpeed,() => { repeatAble = true; ResetRecoil(); });
        GameObject a = ObjectPool.Instance.SpawnObject(GunData.BulletPrefab, ShootPoint.position, transform.rotation,PoolType.GameObject);
		Bullet bullet = a.GetComponent<Bullet>();

		float dmg = GunData.Damage * playerController.Stats.GetStat(StatType.ATK).Value;

        bullet.InitBullet(ShootPoint.position,GunData.SpreadMax * GunRecoil, new DamageInfo(playerController.gameObject,dmg));
		GunRecoil += GunData.Recoil;
		if (GunRecoil >= 1) { GunRecoil = 1f; }
	}
}
