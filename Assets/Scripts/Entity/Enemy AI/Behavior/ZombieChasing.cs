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
        player = ((SharedTransform)GlobalVariables.Instance.GetVariable(Constant.Target)).Value;
        SetUpAnimation();
    }
    
    private void SetUpAnimation()
    {
        controller.Agent.speed = Speed;
        
        controller.Animator.SetFloat(Constant.ZombieRandomStatus,
            Random.Range(0,Constant.ZombieStatusCount));
    }
    
    public override void OnStart()
    {
        /*controller.NavMeshObstacle.enabled = false;
        StartCoroutine(DelayAFrame());*/
        controller.Agent.isStopped = false;
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
        /*controller.Agent.enabled = false;
        controller.NavMeshObstacle.enabled = true;*/
        controller.Agent.isStopped = true;
    }
}
