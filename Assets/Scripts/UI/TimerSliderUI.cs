using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TimerSliderUI : MonoBehaviour
{
    public float TransitionDuration = 0.5f;
    public Slider Slider { get;private set; }
    [field:SerializeField] public Image BackGround { get;private set; }
    [field:SerializeField] public Image Fill { get;private set; }
    private Color fillColor, backGroundColor;
    private void Awake()
    {
        Slider = GetComponent<Slider>();
        fillColor = Fill.color;
        backGroundColor = BackGround.color;
    }

    private Tweener a, b;
    [ContextMenu("Fade In")]
    public void FadeIn()
    {
        DOVirtual.Color(Color.clear, fillColor, TransitionDuration, x => { Fill.color = x ;});
        DOVirtual.Color(Color.clear, backGroundColor,TransitionDuration, x => { BackGround.color = x ;});
    }
    [ContextMenu("Fade Out")]
    public void FadeOut()
    {
        DOVirtual.Color(fillColor,Color.clear,  TransitionDuration, x => { Fill.color = x ;});
        DOVirtual.Color(backGroundColor, Color.clear,TransitionDuration,  x => { BackGround.color = x ;});
    }
    
}
