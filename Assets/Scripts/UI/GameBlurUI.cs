using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Tech.Singleton;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameBlurUI : Singleton<GameBlurUI>
{
    public Volume GlobalVolume;
    public float FocusValue;
    public float TransitionDuration;
    private DepthOfField dof;
    private static Tweener tween;
    public void Blur(float Duration)
    {
        if (GlobalVolume.profile.TryGet(out dof))
        {
            tween.SetUpdate(false);
            tween.Kill();
            tween = DOVirtual.Float(dof.focusDistance.value, 0.1f, Duration, v => dof.focusDistance.value = v).SetUpdate(true);
        }
    }
    public void UnBlur(float Duration)
    {
        if (GlobalVolume.profile.TryGet(out dof))
        {
            tween.SetUpdate(false);
            tween.Kill();
            tween = DOVirtual.Float(dof.focusDistance.value, FocusValue, Duration, v => dof.focusDistance.value = v).SetUpdate(true);
        }
    }
}
