using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererHelper : MonoBehaviour
{
    public Transform Start;
	public float AimAmmount { get; set; } = 1.0f;
    private LineRenderer LR;
	private void Awake()
	{
		LR = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		LR.SetPosition(0, Start.transform.position);
		LR.SetPosition(1, Start.transform.position + Start.transform.rotation * Vector3.forward*AimAmmount);
	}
}
