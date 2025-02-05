using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class FadeBlurPanel : PanelBase
{
    public bool StopTime = false;
    public bool BlurBackground = false;
    [Range(0.01f,2f)]public float TransitionDuration = 0.2f;
    public UnityEvent OnShowDo;
    public UnityEvent OnHideDo;
    private static Tweener tween;
    public override void Show()
    {
        base.Show();
        OnShowDo?.Invoke();
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
    public override void Hide()
    {
        base.Hide();
        OnHideDo?.Invoke();
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

    private void OnDestroy()
    {
        Hide();
    }
}
