using UnityEngine;

public class IsAttackAnimationEnd : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(GlobalAnimation.IsAttackAnimationEnd, false);
    }
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(GlobalAnimation.IsAttackAnimationEnd, true);
    }
}
