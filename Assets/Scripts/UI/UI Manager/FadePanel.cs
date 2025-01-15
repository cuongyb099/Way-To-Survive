using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class FadePanel : PanelBase
{
    protected CanvasGroup canvasGroup;
    protected const float fadeDuration = 0.35f;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        
    }

    public override void Show()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.DOFade(1f, fadeDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => canvasGroup.interactable = true);
    }

    public override void Hide()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = false;
        canvasGroup.DOFade(0f, fadeDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => gameObject.SetActive(false));
    }
}