using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererHelper : MonoBehaviour
{
    public Transform Start;
	public Transform Forward;
	public float AimAmount { get; set; } = 1.0f;
    private LineRenderer LR;
	private void Awake()
	{
		LR = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		LR.SetPosition(0, Start.transform.position);
		LR.SetPosition(1, Start.transform.position + Forward.forward*AimAmount);
	}
	public void SetLineRenderer(Transform start, float aimAmount,Transform forward)
	{
		Start = start;
		AimAmount = aimAmount;
		Forward = forward;
	}
}
