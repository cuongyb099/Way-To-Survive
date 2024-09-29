using BehaviorDesigner.Runtime.Tasks;

public class IsDamaged : Conditional
{
    private ZombieStat stat;
    private ZombieCtrl controller;
    
    public override void OnAwake()
    {
        controller = GetComponent<ZombieCtrl>();
        stat = (ZombieStat)controller.Stat;
    }

    public override TaskStatus OnUpdate()
    {
        return stat.IsDamaged ? TaskStatus.Success : TaskStatus.Failure;
    }
}