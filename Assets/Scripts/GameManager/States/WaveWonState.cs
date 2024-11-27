
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
        Timer = gameManager.WaveWonTime;
        gameManager.BuffCanvas.SetActive(true);
        gameManager.CountDownSlider.FadeIn();
    }

    public override void Exit()
    {
        gameManager.CountDownSlider.FadeOut();
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
