using ResilientCore;

public class DiedState : BaseState<EGameState>
{
    GameManager gameManager;
    public DiedState(GameManager manager) : base(EGameState.Died)
    {
        gameManager = manager;
    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override EGameState GetNextState()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }

    public override void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }
}
