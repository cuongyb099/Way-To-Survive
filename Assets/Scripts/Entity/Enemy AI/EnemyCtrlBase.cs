using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyCtrlBase : MonoBehaviour
{
    public NavMeshAgent Agent { get; private set; }
    public Animator Animator { get; private set; }
    public BehaviorTree OriginTree { get; private set; }
    public AnimEventHelper AnimHelper  { get; private set; }
    public BaseStat Stat { get; private set; }
    public NavMeshObstacle NavMeshObstacle { get; private set; }
    
    protected virtual void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponentInChildren<Animator>();
        AnimHelper = GetComponentInChildren<AnimEventHelper>();
        OriginTree = GetComponent<BehaviorTree>();
        Stat = GetComponentInChildren<BaseStat>();
        NavMeshObstacle = GetComponent<NavMeshObstacle>();
    }
}
