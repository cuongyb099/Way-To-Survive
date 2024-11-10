using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class CanvasUIHandler : MonoBehaviour
{
    public bool StopTime = false;
    public bool BlurBackground = false;
    [Range(0.01f,2f)]public float TransitionDuration = 0.2f;
    public UnityEvent OnEnableDo;
    public UnityEvent OnDisableDo;
    private static Tweener tween;
    protected virtual void OnEnable()
    {
        OnEnableDo?.Invoke();
        PlayerInput.Instance.InputActions.BasicAction.Disable();
        if (StopTime)
        {
            tween.SetUpdate(false);
            tween.Kill();
            tween = DOVirtual.Float(Time.timeScale, 0f, TransitionDuration, v => Time.timeScale = v).SetUpdate(true);
        }
        if(BlurBackground)
            GameBlurUI.Instance.Blur(TransitionDuration);
    }
    protected virtual void OnDisable()
    {
        OnDisableDo?.Invoke();
        PlayerInput.Instance.InputActions.BasicAction.Enable();
        if (StopTime)
        {
            tween.SetUpdate(false);
            tween.Kill();
            tween = DOVirtual.Float(Time.timeScale, 1f, TransitionDuration, v => Time.timeScale = v).SetUpdate(true);
        }
        if(BlurBackground)
            GameBlurUI.Instance.UnBlur(TransitionDuration);
    }

}
