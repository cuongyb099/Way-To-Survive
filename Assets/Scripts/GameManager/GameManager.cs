using System;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using DG.Tweening;
using ResilientCore;
using Tech.Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public AudioSource GMAudioSource { get; private set; }
    [field: Header("Game Variables")]
    [field:SerializeField] public int FPSLimitValue{ get; private set; } = 30;
    [field:SerializeField] public int WaveWonTime{ get; private set; } = 30;
    [field:SerializeField] public float ShoppingTime{ get; private set; } = 30f;

    public bool SkipShopping { get; set; } = false;
    //Singleton
    public static GameManager Instance { get; private set; } = null;
    
    [Header("UI Elements")]
    public GameObject LoseCanvas;
    public GameObject BuffCanvas;
    public TimerSliderUI CountDownSlider;
    protected void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        
        Player = FindAnyObjectByType<PlayerController>();
        WaveManager = FindAnyObjectByType<WaveManager>();
        EnemyManager = FindAnyObjectByType<EnemyManager>();
        GMAudioSource = GetComponent<AudioSource>();
        Application.targetFrameRate= FPSLimitValue;
        
        States.Add(EGameState.Shopping, new ShoppingState(this));
        States.Add(EGameState.Combat, new CombatState(this));
        States.Add(EGameState.WaveWon, new WaveWonState(this));
        States.Add(EGameState.Died, new DiedState(this));

        Player.OnDeath += () => { LoseCanvas.SetActive(true); };
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
}
