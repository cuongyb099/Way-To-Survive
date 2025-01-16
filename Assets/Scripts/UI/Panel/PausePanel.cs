using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : PanelBase
{
    [Header("Button")] 
    [SerializeField] private Button _resumeBtn;
    [SerializeField] private Button _settingBtn;
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _quitBtn;

    [Header("Other Settings")]
    public bool StopTime;
    public bool BlurBackground; 
    [Range(0.01f,2f)]
    public float TransitionDuration = 0.2f;
    private static Tweener tween;
    
    private void Awake()
    {
        LoadButton();
    }

    private void LoadButton()
    {
        _resumeBtn.onClick.AddListener(() =>
        {
            Hide();
            UIManager.Instance.ShowPanel(UIConstant.MainGameplayPanel);
        });
        _settingBtn.onClick.AddListener(() =>
        {
            Hide();
            UIManager.Instance.ShowPanel(UIConstant.SettingsPanel);
        });
    }

    private void Reset()
    {
        ResetButton();
    }

    private void ResetButton()
    {
        var middle = transform.Find(UIConstant.Middle);

        if (!_resumeBtn)
        {
            _resumeBtn = middle.Find("Resume BTN").GetComponentInChildren<Button>();
        }
        
        if (!_settingBtn)
        {
            _settingBtn = middle.Find("Settings BTN").GetComponentInChildren<Button>();
        }
        
        if (!_restartBtn)
        {
            _restartBtn = middle.Find("Restart BTN").GetComponentInChildren<Button>();
        }
        
        if (!_quitBtn)
        {
            _quitBtn = middle.Find("Quit BTN").GetComponentInChildren<Button>();
        }
    }
    
    public override void Show()
    {
        gameObject.SetActive(true);
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
        PlayerInput.Instance.InputActions.BasicAction.Enable();
        if (StopTime)
        {
            tween.SetUpdate(false);
            tween.Kill();
            tween = DOVirtual.Float(Time.timeScale, 1f, TransitionDuration, v => Time.timeScale = v).SetUpdate(true)
                .OnComplete(() => gameObject.SetActive(false));
        }
        if(BlurBackground)
            GameBlurUI.Instance.UnBlur(TransitionDuration);
    }
}