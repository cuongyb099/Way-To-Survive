using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunSO",menuName ="Item/Gun/new GunSO")]
public class GunSO : ScriptableObject
{
	public GameObject BulletPrefab;
	public float ShootingSpeed;
	public float Damage;
	public float Aim;
	public float Accuracy;
	public bool ReleaseToShoot;
}
