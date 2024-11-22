using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("AI")]
public class DotsStopDestination : BaseEnemyBehavior
{
    public override void OnStart()
    {
        enemyCtrl.AvoidAuthoring.enabled = false;
    }
    
    public override TaskStatus OnUpdate()
    {
        enemyCtrl.StopDestination();
        return TaskStatus.Success;
    }
}