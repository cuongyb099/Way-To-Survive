using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using Tech.Logger;
using UnityEngine;

public class DefaultAttack : BaseZombieAction
{
    public float CooldownTime = 0.25f;
    public bool IsCooldown;
    private ColliderDetection hitBoxDetect;
    
    public override void OnAwake()
    {
        base.OnAwake();
        
        hitBoxDetect = controller.transform.Find("Default Attack HitBox").GetComponent<ColliderDetection>();
        
        if(!hitBoxDetect) LogCommon.LogError("Not Found Attack HitBox");
        
        hitBoxDetect.CallbackOnTriggerEnter += DetectTarget;
    }

    public override void OnStart()
    {
        controller.AnimHelper.IsCurrentAnimEnd = false;
        
        controller.AnimHelper.CallbackAnimEvent.AddListener(Attack);
        
        if (!IsCooldown)
        {
            controller.Animator.SetFloat(Constant.ZombieRandomAttack, 
                Random.Range(0, Constant.ZombieAttackCount));
            controller.Animator.SetTrigger(Constant.DefaultAttack);    
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (IsCooldown) return TaskStatus.Success;
        
        return controller.AnimHelper.IsCurrentAnimEnd ? TaskStatus.Success : TaskStatus.Running;
    }

    public override void OnEnd()
    {
        controller.AnimHelper.CallbackAnimEvent.RemoveListener(Attack);   
        IsCooldown = true;
        DOVirtual.DelayedCall(CooldownTime, () =>
        {
            IsCooldown = false;
        }, false);
    }

    public void DetectTarget(Collider other)
    {
        if (!other.CompareTag(Constant.PlayerTag)) return;

        if (other.TryGetComponent(out IDamagable target))
        {
            target.Damage(10f);
            DOVirtual.DelayedCall(0.01f,() => hitBoxDetect.SetActiveCollider(false));
        }
    }

    public void Attack()
    {
        hitBoxDetect.SetActiveCollider(true);
    }
}
