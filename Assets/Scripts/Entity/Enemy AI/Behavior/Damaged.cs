using UnityEngine;

public class Damaged : BaseZombieAction
{
    private ZombieStat stat;
    private ZombieCtrl controller;
    
    public override void OnAwake()
    {
        controller = GetComponent<ZombieCtrl>();
        stat = (ZombieStat)controller.Stat;
    }
    
    public override void OnStart()
    {
        controller.Animator.SetFloat(Constant.ZombieRandomDamaged, Random.Range(0, Constant.ZombieDamagedCount) );
        controller.Animator.SetTrigger(Constant.GetHit);
        stat.IsDamaged = false;
    }
}