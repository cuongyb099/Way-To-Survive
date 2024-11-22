using BehaviorDesigner.Runtime.Tasks;

public class IsDead : BaseZombieAction
{
    private ZombieStat stat;

    public override void OnAwake()
    {
        base.OnAwake();
        stat = (ZombieStat)controller.Stat;
    }

    public override TaskStatus OnUpdate()
    {
        
        return TaskStatus.Success;
    }
}