using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DamagePopUpController : MonoBehaviour
{
    public AnimationCurve ScaleCurve;
    public AnimationCurve FadeCurve;
	public AnimationCurve HeightCurve;
	public Action StopAnimation;
	private TextMeshProUGUI text;
    private float time = 0f;
    private Vector3 initPos;

    void Awake()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
	}
	private void Start()
	{
		ResetPopUp();
	}
	void Update()
    {
		text.color = new Color(text.color.r, text.color.g, text.color.b ,FadeCurve.Evaluate(time));
        transform.localScale = Vector3.one * ScaleCurve.Evaluate(time);
        transform.position = initPos + new Vector3(0,1 + HeightCurve.Evaluate(time),0);
        time += Time.deltaTime;
		if(time >= 1f)
			StopAnimation?.Invoke();
    }
	public void ResetPopUp()
	{
		time = 0f;
		initPos = transform.position;
	}

	public void Get()
	{
		gameObject.SetActive(true);
	}

	public void Release()
	{
		gameObject.SetActive(false);
	}
}
