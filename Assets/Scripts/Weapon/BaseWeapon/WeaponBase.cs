using DG.Tweening;
using System;
using KatInventory;
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
public abstract class WeaponBase : ItemBase, IEquatable<WeaponBase>
{
	public WeaponBaseSO WeaponData => (WeaponBaseSO)Data.StaticData ;
	public bool ShootAble { get; set; } = true;
	
	protected PlayerController playerController;
	protected bool repeatAble = true;
	protected bool trigger;
	protected virtual void Awake()
	{
		playerController = GameManager.Instance.Player;
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
			if (!WeaponData.ReleaseToShoot) { Shoot(); }
			trigger = true;
		}
		else
		{
			trigger = false;
		}
	}
	private void Rotate_canceled()
	{
		if (WeaponData.ReleaseToShoot && trigger) Shoot();
	}
	public virtual void Shoot()
	{
		if (!ShootAble ||
		    !repeatAble ) return;
		repeatAble = false;
		PlayerEvent.OnAttack?.Invoke();
		DOVirtual.DelayedCall(WeaponData.ShootingSpeed/playerController.Stats.GetStat(StatType.ATKSpeed).Value, () => { repeatAble = true;});
		WeaponSoundPlay();
	}

	protected virtual void WeaponSoundPlay()
	{
		AudioManager.Instance.PlaySound(WeaponData.AttackSounds.ToArray(),volumeType: SoundVolumeType.SOUNDFX_VOLUME);
	}
	

	public bool Equals(WeaponBase other)
	{
		return WeaponData == other.WeaponData;
	}
	
	public override int GetHashCode()
	{
		return HashCode.Combine(base.GetHashCode(), WeaponData);
	}
}
