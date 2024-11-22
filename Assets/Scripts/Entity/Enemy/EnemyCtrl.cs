using System;
using BehaviorDesigner.Runtime;
using ProjectDawn.Navigation.Hybrid;
using UnityEngine;
using ObjectPool = Tech.Pooling.ObjectPool;

public class EnemyCtrl : BasicController
{
    public AgentAuthoring Authoring { get; protected set; }
    public AgentForRootmotion AgentRootmotion { get; protected set; }
    public AgentAvoidAuthoring AvoidAuthoring { get; protected set; }
    public AgentNavMeshAuthoring NavMeshAuthoring { get; protected set; }
    public Animator Anim { get; protected set; }
    public BehaviorTree BTree { get; protected set; }
    
    [HideInInspector] public bool IsTakingDamage;
    [HideInInspector] public bool IsDead;
    
    protected override void Awake()
    {
        base.Awake();
        Anim = GetComponent<Animator>();
        BTree = GetComponent<BehaviorTree>();
        AgentRootmotion = GetComponent<AgentForRootmotion>();
        Authoring = GetComponent<AgentAuthoring>();
        AvoidAuthoring = GetComponent<AgentAvoidAuthoring>();
        NavMeshAuthoring = GetComponent<AgentNavMeshAuthoring>();
    }

    public override void Damage(DamageInfo info)
    {
        IsTakingDamage = true;
        base.Damage(info);
    }

    public override void Death()
    {
        if(IsDead) return;
        
        IsDead = true;
        if(!EnemyManager.Instance) return;
        EnemyManager.Instance.ReturnEnemyToPool();
    }

    private void OnDisable()
    {
        BTree.DisableBehavior(false);
        IsDead = false;
        if (!ObjectPool.Instance) return;
        ObjectPool.Instance.ReturnObjectToPool(this.gameObject);
    }

    public void SetDestination(Vector3 destination)
    {
        Authoring.SetDestination(destination);
    }

    public void StopDestination()
    {
        Authoring.SetDestination(transform.position);
    }
}