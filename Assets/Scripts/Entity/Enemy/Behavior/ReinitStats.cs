using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Utilities")]
public class ReinitStats : Action
{
    public override TaskStatus OnUpdate()
    {
        GetComponent<StatsController>().ReInit();
        return TaskStatus.Success;
    }
}