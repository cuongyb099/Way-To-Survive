using DG.Tweening;

public abstract class FadePanel : PanelBase
{
    protected const float _fadeDuration = 0.25f; 

    public override void Hide()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0f, _fadeDuration)
            .SetEase(Ease.Linear);
    }

    public override void Show()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1f, _fadeDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => canvasGroup.blocksRaycasts = true);
    }
}