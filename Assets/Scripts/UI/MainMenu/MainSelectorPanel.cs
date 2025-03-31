using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSelectorPanel : FadeBlurPanel
{
    [Header("Button")] 
    [SerializeField] private Button _startBtn;
    [SerializeField] private Button _tutorialBtn;
    [SerializeField] private Button _settingsBtn;
    [SerializeField] private Button _quitBtn;
    
    protected override void OnAwake()
    {
        LoadButton();
        Show();
    }
    private void LoadButton()
    {
        _startBtn.onClick.AddListener(() =>
        {
            Hide();
            LevelAsyncManager.Instance.SwitchToMap1();
        });
        _tutorialBtn.onClick.AddListener(() =>
        {
            Hide();
        });
        _settingsBtn.onClick.AddListener(() =>
        {
            Hide();
            //UIManager.Instance.ShowPanel(UIConstant.SettingsPanel);
        });
        _quitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
