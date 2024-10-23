using BehaviorDesigner.Runtime;
using ProjectDawn.Navigation.Hybrid;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyCtrlBase : BasicController
{
    public Animator Animator { get; private set; }
    public BehaviorTree OriginTree { get; private set; }
    public AnimEventHelper AnimHelper  { get; private set; }
    public BaseStat Stat { get; private set; }
    /*public NavMeshObstacle NavMeshObstacle { get; private set; }
    public NavMeshAgent Agent { get; private set; }*/
    public AgentAuthoring AgentAuthor { get; private set;}
    public CustomAgent Agent { get; private set; }
    public ColliderDetectionCtrl DetectionCtrl { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
        AgentAuthor = GetComponent<AgentAuthoring>();
        Animator = GetComponentInChildren<Animator>();
        AnimHelper = GetComponentInChildren<AnimEventHelper>();
        DetectionCtrl = GetComponentInChildren<ColliderDetectionCtrl>();
        OriginTree = GetComponent<BehaviorTree>();
        Stat = GetComponentInChildren<BaseStat>();
        /*Agent = GetComponent<NavMeshAgent>();
        NavMeshObstacle = GetComponent<NavMeshObstacle>();*/
    }
}
