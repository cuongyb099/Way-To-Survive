using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform player;
    public float attackRange = 2f;
    public float moveSpeed = 3.5f;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed; // Đặt tốc độ di chuyển của zombie
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < attackRange)
        {
            Attack();
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        // Logic tấn công (như mất máu)
        Debug.Log("Zombie attacks!");
        // Bạn có thể thêm logic để gây sát thương cho người chơi tại đây.
    }
}