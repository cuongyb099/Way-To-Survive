using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using Tech.Logger;
using UnityEngine;

public class DefaultAttack : BaseZombieAction
{
    private ColliderDetection hitBoxDetect;
    public ColliderDetectionType DetectionType;
    public float Damage = 10f;
    
    public override void OnAwake()
    {
        base.OnAwake();
        
        hitBoxDetect = controller.DetectionCtrl.GetDetection(DetectionType);
        
        hitBoxDetect.CallbackColliderEnter += DetectTarget;
    }

    public override void OnStart()
    {
        controller.AnimHelper.IsCurrentAnimEnd = false;
        controller.Animator.SetTrigger(EnemyConstant.ZombieDefaultAttack);
        controller.AnimHelper.CallbackAnimEvent.AddListener(HandleAttack);
    }


    public override TaskStatus OnUpdate()
    {
        return controller.AnimHelper.IsCurrentAnimEnd ? TaskStatus.Success : TaskStatus.Running;
    }

    public override void OnEnd()
    {
        controller.AnimHelper.CallbackAnimEvent.RemoveListener(HandleAttack);   
    }

    public void DetectTarget(Collider other)
    {
        if (!other.CompareTag(EnemyConstant.PlayerTag)) return;

        if (other.TryGetComponent(out IDamagable target))
        {
            var damageInfo = new DamageInfo(null, Damage);
            target.Damage(damageInfo);
            hitBoxDetect.SetActiveCollider(false);
        }
    }

    public void HandleAttack()
    {
        hitBoxDetect.SetActiveCollider(true);
    }
}
