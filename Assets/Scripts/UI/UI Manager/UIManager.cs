using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tech.Logger;
using Tech.Singleton;
using UnityEngine;

[DefaultExecutionOrder(-900)]
public class UIManager : Singleton<UIManager>
{
    private Dictionary<string, PanelBase> _panelDictionary = new ();

    public string LastPanelInteract;
    //In Complex UI System Should Use Stack System This System Is Simple Work With Small Game
    public async Task<PanelBase> CreatePanelAsync(string panelName, Action<PanelBase> onComplete = null)
    {
        if (_panelDictionary.ContainsKey(panelName)) return null;
        
        var go = await AddressablesManager.Instance.InstantiateAsync(panelName, transform);
        go.name = panelName;
        if (!go.TryGetComponent(out PanelBase panel))
        {
            LogCommon.LogError(go.name + "No Has Panel Component");
            return null;
        }

        LastPanelInteract = panelName;
        _panelDictionary.Add(panelName, panel);
        onComplete?.Invoke(panel);
        return panel;
    }

    public void RemovePanel(string panelName)
    {
        _panelDictionary.Remove(panelName);
        LastPanelInteract = panelName;
        AddressablesManager.Instance.RemoveAsset(panelName);                       
    }

    public T GetPanel<T>(string panelName) where T : PanelBase
    {
        LastPanelInteract = panelName;
        return (T)_panelDictionary.GetValueOrDefault(panelName);  
    }
    
    public void ShowPanel(string panelName)
    {
        LastPanelInteract = panelName;
        if (!_panelDictionary.TryGetValue(panelName, out var panel))
        {
            _ = CreatePanelAsync(panelName, x => x.Show());
            return;
        }
        
        panel.Show();
    }

    public void HidePanel(string panelName)
    {
        LastPanelInteract = panelName;
        if (!_panelDictionary.TryGetValue(panelName, out var panel))
        {
            _ = CreatePanelAsync(panelName, x => x.Hide());
            return;
        }
        
        panel.Hide();
    }
}