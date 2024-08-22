using BehaviorDesigner.Runtime.Tasks;

public class InitZombie : Action
{
    private ZombieCtrl controller; 
    
    public override void OnAwake()
    {
        controller = GetComponent<ZombieCtrl>();
    }
}
