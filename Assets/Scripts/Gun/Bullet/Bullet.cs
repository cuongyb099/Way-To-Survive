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
	public float LiveTime = 3f;
	public float Force = 10f;
	public GameObject HitEffectWall;
	public DamageInfo DamageInfo;
	public Rigidbody RB { get; private set; }
	public TrailRenderer TrailRenderer { get; private set; }
	private DG.Tweening.Tween seq;
	private void Awake()
	{
		RB = GetComponent<Rigidbody>();
		TrailRenderer = GetComponent<TrailRenderer>();
	}
	public void OnEnable()
	{
		seq = DOVirtual.DelayedCall(LiveTime, () => { Deactivate(); });
	}
	private void OnCollisionEnter(Collision collision)
	{
		ObjectPool.Instance.SpawnObject(HitEffectWall, collision.contacts[0].point, Quaternion.identity, PoolType.ParticleSystem);
		seq.Kill();

		Collider collider = collision.contacts[0].otherCollider;

		if (!collider.CompareTag(DamageInfo.Dealer.tag))
		{
			if (collider.TryGetComponent(out IDamagable damagable))
			{
				DamagePopUpGenerator.Instance.CreateDamagePopUp(collision.contacts[0].point,DamageInfo);
				damagable.Damage(DamageInfo);
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
	public void DealDamage()
	{

	}
}
