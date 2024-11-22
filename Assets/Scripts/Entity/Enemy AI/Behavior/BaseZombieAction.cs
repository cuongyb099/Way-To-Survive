using BehaviorDesigner.Runtime.Tasks;

public class BaseZombieAction : Action
{
    protected ZombieCtrl controller;

    public override void OnAwake()
    {
        controller = GetComponent<ZombieCtrl>();
    }
}
