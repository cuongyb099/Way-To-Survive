using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject GameTitle;
    public static MainMenuManager Instance { get; private set; } = null;
    protected void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        InitializeUIAsync();
        
    }

    private async void InitializeUIAsync()
    {
        while (!UIManager.Instance)
        {
            await Task.Delay(100);
        }

        var tasks = new List<Task<PanelBase>>()
        {
            UIManager.Instance.CreatePanelAsync(UIConstant.SetupBeforePlayPanel),
            UIManager.Instance.CreatePanelAsync(UIConstant.MainMenuPanel),
        };

        await Task.WhenAll(tasks);
        
        UIManager.Instance.ShowPanel(UIConstant.MainMenuPanel);
    }
}
