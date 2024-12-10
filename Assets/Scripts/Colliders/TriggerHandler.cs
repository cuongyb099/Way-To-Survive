using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
	public bool IsTriggered {  get; private set; }
	private List<GameObject> colliders = new List<GameObject>();
	private void OnTriggerEnter(Collider other)
	{
		IsTriggered = true;
		colliders.Add(other.gameObject);
	}
	private void OnTriggerExit(Collider other)
	{
		colliders.Remove(other.gameObject);
		if(colliders.Count == 0)
			IsTriggered = false;
	}
}
