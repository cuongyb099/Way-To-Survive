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
	Knife,
}
public abstract class WeaponBase : MonoBehaviour, IEquatable<WeaponBase>
{
	public GunSO GunData;
	public bool ShootAble { get; set; } = true;
	
	protected PlayerController playerController;
	protected bool repeatAble = true;
	protected bool trigger;
	protected virtual void Awake()
	{
		playerController = GetComponentInParent<PlayerController>();
	}
	public virtual void Initialize()
	{
		
	}
	protected virtual void OnEnable()
	{
		InputEvent.OnShootStickCanceled += Rotate_canceled;
	}

	protected virtual void OnDisable()
	{
		InputEvent.OnShootStickCanceled -= Rotate_canceled;
	}

	public virtual void OnSwitchOut()
	{
		
	}
	public virtual void OnSwitchIn()
	{
		
	}
	protected virtual void Update()
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
	private void Rotate_canceled()
	{
		if (GunData.ReleaseToShoot && trigger) Shoot();
	}
	public virtual void Shoot()
	{
		if (!ShootAble ||
		    !repeatAble ) return;
		repeatAble = false;
		PlayerEvent.OnShoot?.Invoke();
		DOVirtual.DelayedCall(GunData.ShootingSpeed/playerController.Stats.GetStat(StatType.ShootSpeed).Value, () => { repeatAble = true;});
		WeaponSoundPlay();
	}

	protected void WeaponSoundPlay()
	{
		AudioManager.Instance.PlaySound(GunData.ShootingSounds.ToArray(),volumeType: SoundVolumeType.SOUNDFX_VOLUME);
		if(GunData.TailSound)
			AudioManager.Instance.PlaySound(GunData.TailSound,volumeType: SoundVolumeType.SOUNDFX_VOLUME);
	}
	

	public bool Equals(WeaponBase other)
	{
		return GunData == other.GunData;
	}
	
	public override int GetHashCode()
	{
		return HashCode.Combine(base.GetHashCode(), GunData);
	}
}
