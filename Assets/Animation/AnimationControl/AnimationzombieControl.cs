using UnityEngine;

public class ZombieAnimationController : MonoBehaviour
{
    private Animator animator;
    private ZombieAI zombieAI;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        zombieAI = GetComponent<ZombieAI>();
    }

    private void Update()
    {
        if (isDead) return; // Dừng mọi hoạt động nếu zombie đã chết

        // Kiểm tra trạng thái đuổi theo
        bool isChasing = zombieAI.IsChasingPlayer(); // Phương thức kiểm tra trong ZombieAI
        animator.SetBool("isChasing", isChasing);

        // Kiểm tra trạng thái tấn công
        bool isAttacking = zombieAI.IsAttackingPlayer(); // Phương thức kiểm tra trong ZombieAI
        animator.SetBool("isAttacking", isAttacking);
    }

    public void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        zombieAI.enabled = false; // Tắt AI khi zombie chết
    }
}