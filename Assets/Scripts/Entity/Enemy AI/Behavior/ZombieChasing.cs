using System.Collections;
using BehaviorDesigner.Runtime;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using Random = UnityEngine.Random;

public class ZombieChasing : BaseZombieAction
{
    private Transform player;
    public float Speed;
    
    public override void OnAwake()
    {
        base.OnAwake();
        player = ((SharedTransform)GlobalVariables.Instance.GetVariable(EnemyConstant.Target)).Value;
        SetUpAnimation();
    }
    
    private void SetUpAnimation()
    {
        controller.AgentAuthor.Speed = Speed;
        
        controller.Animator.SetFloat(EnemyConstant.ZombieRandomStatus,
            Random.Range(0,EnemyConstant.ZombieStatusCount));
    }

    public override TaskStatus OnUpdate()
    {
        //if (!controller.Agent.enabled) return TaskStatus.Running;
        //controller.Agent.destination = player.position;   
        controller.SetDestination(player.position);
        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        controller.StopDestination();        
    }
}
