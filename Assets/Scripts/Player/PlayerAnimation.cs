using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private const string IS_WALKING = "IsWalking";
    private const string IS_SLIDING = "IsSliding";
    private const string ATTACK = "Attack";
    private const string JUMP = "Jump";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeWalkingAnimation(float movement)
    { 
        if (movement == 0) animator.SetBool(IS_WALKING, false);
        else animator.SetBool(IS_WALKING, true);
    }

    public void PlayAttackAnimation()
    {
        animator.SetTrigger(ATTACK);
    }

    public void PlayJumpAnimation()
    {
        animator.SetTrigger(JUMP);
    }

    public void ChangeSlidingAnimation(bool isSliding)
    {
        animator.SetBool(IS_SLIDING, isSliding);
    }
}
