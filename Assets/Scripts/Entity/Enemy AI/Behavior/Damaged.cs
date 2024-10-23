using UnityEngine;

public class Damaged : BaseZombieAction
{
    private ZombieCtrl controller;
    
    public override void OnAwake()
    {
        controller = GetComponent<ZombieCtrl>();
    }
    
    public override void OnStart()
    {
        controller.Animator.SetFloat(EnemyConstant.ZombieRandomDamaged, Random.Range(0, EnemyConstant.ZombieDamagedCount) );
        controller.Animator.SetTrigger(EnemyConstant.GetHit);
        controller.IsDamaged = false;
    }
}