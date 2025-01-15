using UnityEngine;
using UnityEngine.UI;

public class MainMenu : PanelBase
{
    [SerializeField] private Button openSubPanelBtn;
    
    //This Function Call When You Add This Component To GameObject Or ContextMenu Reset 
    private void Reset()
    {
        LoadComponent();        
    }

    private void Awake()
    {
        LoadComponent();
    }

    private void LoadComponent()
    {
        if (!openSubPanelBtn)
        {
            openSubPanelBtn = GetComponentInChildren<Button>();
        }
        openSubPanelBtn.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        UIManager.Instance.ShowPanel("Sub Panel");
    }
}