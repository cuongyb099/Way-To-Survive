using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("AI")]
public class DotsSetDestination : BaseEnemyBehavior
{
    public SharedTransform Target;

    public override void OnStart()
    {
        enemyCtrl.AvoidAuthoring.enabled = true;
    }

    public override TaskStatus OnUpdate()
    {
        if (Target.Value == null)
        {
            return TaskStatus.Failure;
        }
        
        enemyCtrl.SetDestination(Target.Value.position);
        return TaskStatus.Running;
    }
}