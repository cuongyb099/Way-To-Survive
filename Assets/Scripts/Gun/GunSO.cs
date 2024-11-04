using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GunSO",menuName ="Item/Gun/new GunSO")]
public class GunSO : ScriptableObject
{
	public GameObject BulletPrefab;
	public WeaponType WeaponType;
	public string GunName;
	[TextArea]
	public string GunDescription;
	public Sprite Icon;
	public int GunPrice;
	public float ShootingSpeed;
	public float Damage;
	public float Aim;
	public float SpreadMax;
	public float Recoil;
    public float RecoilResetTime;
	public float MaxCapacity;
    public bool ReleaseToShoot;
}
