using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum EGameState
{
    Shopping,
    Combat,
    WaveWon,
    Died,
}
public class GameManager : StateMachine<EGameState>
{
    public PlayerController Player { get; private set; }
    public WaveManager WaveManager { get; private set; }
    public EnemyManager EnemyManager { get; private set; }
    [field: Header("Game Variables")]
    [field:SerializeField] public int WaveWonTime{ get; private set; } = 30;
    [field:SerializeField] public float ShoppingTime{ get; private set; } = 30f;

    public bool SkipShopping { get; set; } = false;
    //Singleton
    public static GameManager Instance { get; private set; } = null;
    
    [Header("UI Elements")]
    public GameObject LoseCanvas;
    
    protected void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        InitializeUIAsync();
        Player = FindAnyObjectByType<PlayerController>();
        WaveManager = FindAnyObjectByType<WaveManager>();
        EnemyManager = FindAnyObjectByType<EnemyManager>();
        
        States.Add(EGameState.Shopping, new ShoppingState(this));
        States.Add(EGameState.Combat, new CombatState(this));
        States.Add(EGameState.WaveWon, new WaveWonState(this));
        States.Add(EGameState.Died, new DiedState(this));

        Player.OnDeath += () => { LoseCanvas.SetActive(true); };

    }

    private async void InitializeUIAsync()
    {
        while (!UIManager.Instance)
        {
            await Task.Delay(100);
        }

        var tasks = new List<Task<PanelBase>>()
        {
            UIManager.Instance.CreatePanelAsync(UIConstant.PausePanel),
            UIManager.Instance.CreatePanelAsync(UIConstant.MainGameplayPanel),
            UIManager.Instance.CreatePanelAsync(UIConstant.SettingsPanel), 
            UIManager.Instance.CreatePanelAsync(UIConstant.InventoryPanel),
            UIManager.Instance.CreatePanelAsync(UIConstant.BuffPanel),
            //UIManager.Instance.CreatePanelAsync(UIConstant.LostPanel),
            UIManager.Instance.CreatePanelAsync(UIConstant.WeaponWheelPanel),
        };

        await Task.WhenAll(tasks);
        
        UIManager.Instance.ShowPanel(UIConstant.MainGameplayPanel);
        GameEvent.OnInitializedUI?.Invoke();
    }

    private void Start()
    {
        CurrentState = States[EGameState.Shopping];
        TransitionToState(EGameState.Shopping);
    }


    public void ChangeGameState(EGameState newGameState)
    {
        TransitionToState(newGameState);
    }

    [ContextMenu("Skip CombatState")]
    public void SkipCombatState()
    {
        if(CurrentState != States[EGameState.Combat]) return;
        TransitionToState(EGameState.WaveWon);
    }
}
