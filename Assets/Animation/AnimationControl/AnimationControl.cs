using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;

    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsShooting = Animator.StringToHash("isShooting");

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        UpdateMovementAnimation();
        UpdateShootingAnimation();
    }

    private void UpdateMovementAnimation()
    {
        bool isWalking = Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0;
        animator.SetBool(IsWalking, isWalking);
    }

    private void UpdateShootingAnimation()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger(IsShooting);
        }
    }
}