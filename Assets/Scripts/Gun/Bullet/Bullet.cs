using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tech.Pooling;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
	[field:SerializeField] public int DamageTime { get; private set; } = 1;
	[field:SerializeField,Range(0f,1f)] public float DamageReduction { get; private set; }= 1f;
	public float LiveTime = 3f;
	public float Force = 10f;
	public GameObject HitEffectWall;
	public DamageInfo DamageInfo;
	public Rigidbody RB { get; private set; }
	public TrailRenderer TrailRenderer { get; private set; }
	private Tween seq;
	private int countDMG = 0;
	private void Awake()
	{
		RB = GetComponent<Rigidbody>();
		TrailRenderer = GetComponent<TrailRenderer>();
	}
	public void OnEnable()
	{
		seq = DOVirtual.DelayedCall(LiveTime, Deactivate).SetUpdate(false);
		countDMG = DamageTime;
	}
	
	private void OnCollisionEnter(Collision other)
	{
		
		ObjectPool.Instance.SpawnObject(HitEffectWall, other.contacts[0].point, Quaternion.identity, PoolType.ParticleSystem);
		seq.Kill();
		
		Collider otherCollider = other.collider;
		if (!otherCollider.CompareTag(DamageInfo.Dealer.tag))
		{
			if (otherCollider.TryGetComponent(out IDamagable damagable))
			{
				DamagePopUpGenerator.Instance.CreateDamagePopUp(other.contacts[0].point,DamageInfo);
				damagable.Damage(DamageInfo);
				countDMG--;
				DamageInfo = new DamageInfo(DamageInfo.Dealer,DamageInfo.Damage*DamageReduction,DamageInfo.IsCrit);
				if(countDMG<=0)
					Deactivate();
				return;
			}
		}
		Deactivate();
	}
	

	public void InitBullet(Vector3 point, float accuracy, DamageInfo info)
	{
		DamageInfo = info;
		RB.position = point;
		Vector3 angle = info.Dealer.gameObject.transform.rotation.eulerAngles;

		Quaternion temp = Quaternion.Euler(angle.x, angle.y + Mathf.Clamp(UnityEngine.Random.Range(-accuracy, accuracy), -15, 15), angle.z);
		TrailRenderer.Clear();

		RB.AddForce(temp * Vector3.forward * Force, ForceMode.VelocityChange);

	}
	public void Deactivate()
	{
		RB.velocity = Vector3.zero;
		TrailRenderer.Clear();
		ObjectPool.Instance.ReturnObjectToPool(gameObject);
	}
}
