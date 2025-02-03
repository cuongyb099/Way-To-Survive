using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class SettingsPanel : FadeBlurPanel
{
    [Header("Button")] 
    [SerializeField] private Button _backBtn;
    protected override void OnAwake()
    {
        LoadButton();
    }

    private void LoadButton()
    {
        _backBtn.onClick.AddListener(() =>
        {
            Hide();
            UIManager.Instance.ShowPanel(UIConstant.PausePanel);
        });
    }

}
