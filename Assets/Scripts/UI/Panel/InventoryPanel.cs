using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : PanelBase
{
    [Header("Button")] 
    [SerializeField] private Button _exitButton;
    
    protected override void OnAwake()
    {
        LoadButton();
    }

    private void LoadButton()
    {
        _exitButton.onClick.AddListener(() =>
        {
            Hide();
            UIManager.Instance.ShowPanel(UIConstant.MainGameplayPanel);
        });
    }
}