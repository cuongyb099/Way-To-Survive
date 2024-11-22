using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private bool isWalking = false;
    private bool isShooting = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovementAnimation();
        HandleShootingAnimation();
    }

    // Kiểm soát animation cho di chuyển
    private void HandleMovementAnimation()
    {
        bool moving = Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0;

        // Nếu trạng thái di chuyển thay đổi, cập nhật animation
        if (moving != isWalking)
        {
            isWalking = moving;
            animator.SetBool("isWalking", isWalking);
        }
    }

    // Kiểm soát animation khi bắn súng
    private void HandleShootingAnimation()
    {
        bool shooting = Input.GetButton("Fire1");

        // Nếu trạng thái bắn thay đổi, cập nhật animation
        if (shooting != isShooting)
        {
            isShooting = shooting;
            animator.SetBool("isShooting", isShooting);
        }
    }
}
