
using ResilientCore;
using UnityEngine;

public class ShoppingState : BaseState<EGameState>
{
    public float Timer { get; private set; }
    GameManager gameManager;
    public ShoppingState(GameManager manager) : base(EGameState.Shopping)
    {
        gameManager = manager;
    }

    public override void Enter()
    {
        gameManager.CountDownSlider.FadeIn();
        Timer = gameManager.ShoppingTime;
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
            return EGameState.Combat;
        }
        return Key;
    }

    public override void Update()
    {
        gameManager.CountDownSlider.Slider.value = Timer/gameManager.ShoppingTime;
        Timer -= Time.deltaTime;
    }
}
