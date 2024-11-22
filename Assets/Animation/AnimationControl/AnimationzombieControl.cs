using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform player;
    public float attackRange = 2f;
    public float moveSpeed = 3.5f;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < attackRange)
        {
            // Tấn công người chơi
            Attack();
        }
        else
        {
            // Di chuyển về phía người chơi
            agent.SetDestination(player.position);
        }
    }

    void Attack()
    {
        // Logic tấn công (như mất máu)
        Debug.Log("Zombie attacks!");
    }
}