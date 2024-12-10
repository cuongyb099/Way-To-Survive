
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
        GameEvent.OnStartShoppingState?.Invoke();
        Timer = gameManager.ShoppingTime;
    }

    public override void Exit()
    {
        GameEvent.OnStopShoppingState?.Invoke();
        gameManager.SkipShopping = false;
    }

    public override void FixedUpdate()
    {

    }

    public override EGameState GetNextState()
    {
        if(Timer <= 0 || gameManager.SkipShopping)
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
