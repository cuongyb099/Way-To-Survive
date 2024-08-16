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
	private void Awake()
	{
		RB = GetComponent<Rigidbody>();
	}
	public void OnEnable()
	{
        RB.velocity = Vector3.zero;
		DOTween.Sequence().AppendInterval(LiveTime).OnComplete(() => { Deactivate(); });
	}
	private void OnDisable()
	{
		Debug.Log("return to pool");
	}
	private void OnCollisionEnter(Collision collision)
	{
		Deactivate();
	}
	public void InitBullet(Vector3 point,float accuracy, DamageInfo info)
	{
		DamageInfo = info;
        RB.position = point;
        Vector3 angle = RB.rotation.eulerAngles;
        RB.rotation = Quaternion.Euler(angle.x, angle.y + accuracy, angle.z);
        RB.AddForce(RB.rotation * Vector3.forward * Force, ForceMode.VelocityChange);
    }
	public void Deactivate()
	{
		ObjectPool.Instance.ReturnObjectToPool(gameObject);
	}
}
