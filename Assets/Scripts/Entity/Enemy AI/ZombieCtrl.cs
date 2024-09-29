using System.Collections;
using UnityEngine;

public class ZombieCtrl : EnemyCtrlBase
{
    private float lastMoveTime;
    private Vector3 lastPosition;

    protected override void Awake()
    {
        base.Awake();
        NavMeshObstacle.enabled = false;
        NavMeshObstacle.carveOnlyStationary = false;
        NavMeshObstacle.carving = true;

        lastPosition = transform.position;
    }

    protected virtual void Update()
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
    }

    public void SetDestination(Vector3 Position)
    {
        NavMeshObstacle.enabled = false;

        lastMoveTime = Time.time;
        lastPosition = transform.position;

        StartCoroutine(MoveAgent(Position));
    }

    private IEnumerator MoveAgent(Vector3 Position)
    {
        yield return null;
        Agent.enabled = true;
        Agent.SetDestination(Position);
    }
}
