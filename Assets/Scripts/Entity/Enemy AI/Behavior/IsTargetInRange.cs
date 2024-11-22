using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class IsTargetInRange : Conditional
{
    private Transform target;
    public float Radius;
    public Vector3 OffSetRange;
    public LayerMask TargetLayer;
    private ZombieCtrl controller;
    
    public override void OnAwake()
    {
        base.OnAwake();
        controller = GetComponent<ZombieCtrl>();
        target = ((SharedTransform)GlobalVariables.Instance.GetVariable(Constant.Target)).Value;
    }

    public override TaskStatus OnUpdate()
    {
        var results = new Collider[4];
        
        if (Physics.OverlapSphereNonAlloc(controller.transform.position + OffSetRange, Radius, results, TargetLayer) > 0)
        {
            controller.OriginTree.SetVariableValue(Constant.AiCurTarget, results[0].transform);
            return TaskStatus.Success;
        }
        
        return TaskStatus.Failure;
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(controller.transform.position + OffSetRange, Radius);
    }
}
