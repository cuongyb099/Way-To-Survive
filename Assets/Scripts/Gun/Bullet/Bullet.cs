using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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
	private Tween seq;
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
		ObjectPool.Instance.SpawnObject(HitEffectWall, collision.contacts[0].point,Quaternion.identity,PoolType.ParticleSystem);
		seq.Kill();
		Deactivate();
	}
	public void InitBullet(Vector3 point,float accuracy, DamageInfo info)
	{
		DamageInfo = info;
        RB.position = point;
        Vector3 angle = transform.rotation.eulerAngles;
		
		Quaternion temp = Quaternion.Euler(angle.x, angle.y + Mathf.Clamp(Random.Range(-accuracy, accuracy), -10, 10), angle.z);
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
