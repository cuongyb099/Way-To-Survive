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
	public DamageInfo DamageInfo;
	public Rigidbody RB { get; private set; }
	public TrailRenderer TrailRenderer { get; private set; }
	private void Awake()
	{
		RB = GetComponent<Rigidbody>();
		TrailRenderer = GetComponent<TrailRenderer>();
	}
	public void OnEnable()
	{
		RB.velocity = Vector3.zero;
		DOTween.Sequence().AppendInterval(LiveTime).OnComplete(() => { Deactivate(); });
	}
	private void OnCollisionEnter(Collision collision)
	{
		Deactivate();
	}
	public void InitBullet(Vector3 point,float accuracy, DamageInfo info)
	{
		DamageInfo = info;
        RB.position = point;
        Vector3 angle = transform.rotation.eulerAngles;
		
		Quaternion temp = Quaternion.Euler(angle.x, angle.y + Mathf.Clamp(Random.Range(-accuracy, accuracy), -10, 10), angle.z);
		TrailRenderer.Clear();
		RB.velocity = Vector3.zero;
		RB.AddForce(temp * Vector3.forward * Force, ForceMode.VelocityChange);
		
    }
	public void Deactivate()
	{
		TrailRenderer.Clear();
		ObjectPool.Instance.ReturnObjectToPool(gameObject);
	}
}
