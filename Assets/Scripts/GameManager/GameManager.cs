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
    [field:SerializeField] public int FPSLimitValue{ get; private set; } = 30;
    [field:SerializeField] public int WaveWonTime{ get; private set; } = 30;
    [field:SerializeField] public float ShoppingTime{ get; private set; } = 30f;

    public bool SkipShopping { get; set; } = false;
    //Singleton
    public static GameManager Instance { get; private set; } = null;
    
    [Header("UI Elements")]
    public GameObject LoseCanvas;
    public GameObject BuffCanvas;
    
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
        Application.targetFrameRate= FPSLimitValue;
        
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
        
        UIManager.Instance.ShowPanel(UIConstant.MainGameplayPanel);
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
