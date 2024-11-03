using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
       
        bool isWalking = Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0;
        animator.SetBool("isWalking", isWalking);

        
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("isShooting", true);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("isShooting", false);
        }
    }
}