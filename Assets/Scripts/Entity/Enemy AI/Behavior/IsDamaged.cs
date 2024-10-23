using BehaviorDesigner.Runtime.Tasks;

public class IsDamaged : Conditional
{
    private ZombieCtrl controller;
    
    public override void OnAwake()
    {
        controller = GetComponent<ZombieCtrl>();
    }

    public override TaskStatus OnUpdate()
    {
        return controller.IsDamaged ? TaskStatus.Success : TaskStatus.Failure;
    }
}