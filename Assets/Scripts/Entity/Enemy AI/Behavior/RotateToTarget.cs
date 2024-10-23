using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

public class RotateToTarget : BaseZombieAction
{
    public float RotateTime = 0.25f;
    
    public override void OnStart()
    {
        Transform targetToRotate = (Transform)controller.OriginTree.GetVariable(EnemyConstant.AiCurTarget).GetValue();
        var dirToRotate = (targetToRotate.position - controller.transform.position).normalized;
        dirToRotate.y = 0;
        var targetRotation = Quaternion.LookRotation(dirToRotate, Vector3.up);
        
        controller.transform.DORotateQuaternion(targetRotation, RotateTime).OnComplete(() =>
        {
            controller.transform.rotation = targetRotation;
        });
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
