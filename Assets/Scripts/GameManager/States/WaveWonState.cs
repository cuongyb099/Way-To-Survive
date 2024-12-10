
using UnityEngine;

public class WaveWonState : BaseState<EGameState>
{
    public float Timer { get; private set; }
    GameManager gameManager;
    public WaveWonState(GameManager manager) : base(EGameState.WaveWon)
    {
        gameManager = manager;
    }


    public override void Enter()
    {
        GameEvent.OnStartWinState?.Invoke();
        Timer = gameManager.WaveWonTime;
        gameManager.BuffCanvas.SetActive(true);
    }

    public override void Exit()
    {
        GameEvent.OnStopWaveWinState?.Invoke();
    }

    public override void FixedUpdate()
    {
        
    }

    public override EGameState GetNextState()
    {
        if(Timer <= 0)
        {
            return EGameState.Shopping;
        }
        return Key;
    }

    public override void Update()
    {
        gameManager.CountDownSlider.Slider.value = Timer/gameManager.WaveWonTime;
        Timer -= Time.deltaTime;
    }

}
