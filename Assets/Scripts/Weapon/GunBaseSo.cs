using System.Collections.Generic;
using KatInventory;
using UnityEngine;

[CreateAssetMenu(fileName = "GunSO",menuName ="Item/Gun/new GunSO")]
public class GunBaseSO : ItemBaseSO
{
	[Header("GunData")]
	public WeaponBase WeaponPrefab;
	public GameObject BulletPrefab;
	public GameObject ShellPrefab;
	public GameObject MagPrefab;
	public WeaponType WeaponType;
	public float ShootingSpeed;
	public float Damage;
	public float Aim;
	public float SpreadMax;
	public float Recoil;
    public float RecoilResetTime;
    public float Weight;
	public float MaxCapacity;
    public bool ReleaseToShoot;
    [Header("Sounds")]
    public List<AudioClip> ShootingSounds;

    public AudioClip TailSound;
    public AudioClip CockingSound;
    public AudioClip MagSoundIn;
    public AudioClip MagSoundOut;
    public override ItemType GetItemType() => ItemType.Weapon;
}
