using System.Collections;
using ProjectDawn.Navigation;
using UnityEngine;

public class ZombieCtrl : EnemyCtrlBase
{
    private float lastMoveTime;
    private Vector3 lastPosition;
    public bool IsDamaged;
    
    protected override void Awake()
    {
        base.Awake();
        /*NavMeshObstacle.enabled = false;
        NavMeshObstacle.carveOnlyStationary = false;
        NavMeshObstacle.carving = true;*/

        lastPosition = transform.position;
    }

    /*protected virtual void Update()
    {
        if (Vector3.Distance(lastPosition, transform.position) > NavMeshObstacle.carvingMoveThreshold)
        {
            lastMoveTime = Time.time;
            lastPosition = transform.position;
        }
        if (lastMoveTime + NavMeshObstacle.carvingTimeToStationary < Time.time)
        {
            Agent.enabled = false;
            NavMeshObstacle.enabled = true;
        }
    }*/
    public override void Damage(DamageInfo info)
    {
        IsDamaged = true;
        base.Damage(info);
    }

    //Replace New NavmeshSystem With MultiThreading DOTS
    public void SetDestination(Vector3 Position)
    {
        AgentAuthor.SetDestination(Position);
        
        /*NavMeshObstacle.enabled = false;

        lastMoveTime = Time.time;
        lastPosition = transform.position;

        StartCoroutine(MoveAgent(Position));*/
    }

    public void StopDestination()
    {
        AgentAuthor.Stop();
        
        /*StopAllCoroutines();
        Agent.SetDestination(transform.position);*/
    }
    
    /*private IEnumerator MoveAgent(Vector3 Position)
    {
        yield return null;
        Agent.enabled = true;
        Agent.SetDestination(Position);
    }*/
}
