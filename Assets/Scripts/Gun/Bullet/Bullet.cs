using System.Collections;
using System.Collections.Generic;
using Tech.Pooling;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float LiveTime = 3f;
	public float Force = 10f;
	public Rigidbody RB { get; private set; }
	private Coroutine handler;
	private void Awake()
	{
		RB = GetComponent<Rigidbody>();
	}
	public void OnEnable()
	{
		RB.velocity = Vector3.zero;
		RB.AddForce(RB.rotation * Vector3.forward * Force, ForceMode.VelocityChange);
		handler = StartCoroutine(Countdown());
	}
	private void OnDisable()
	{
		Debug.Log("return to pool");
	}
	private void OnCollisionEnter(Collision collision)
	{
		Deactivate();
	}
	public void Deactivate()
	{
		if(handler != null)
		{
			StopCoroutine(handler);
		}
		ObjectPool.Instance.ReturnObjectToPool(gameObject);
	}
	public IEnumerator Countdown()
	{
		yield return new WaitForSeconds(LiveTime);
		Deactivate();
	}
}
