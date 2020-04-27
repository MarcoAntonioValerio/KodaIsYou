using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_KodaAnimationTransition : MonoBehaviour
{
    private Animator animator;
    private bool isFacingRight = true;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        isFacingRight = true;
    }

    public void KodaAnimationRight()
    {
        animator.SetInteger("DirectionController",1);
    }
    public void KodaAnimationLeft()
    {        
        animator.SetInteger("DirectionController", 1);     
    }
    public void KodaAnimationIdle()
    {
        animator.SetInteger("DirectionController", 0);
    }
    public void KodaAnimationUp()
    {
        animator.SetInteger("DirectionController", 2);
    }
    public void KodaAnimatinionDown()
    {
        animator.SetInteger("DirectionController", 3);
    }

    public void FlipSprite(float horizontal)
    {
        if (horizontal > 0 && !isFacingRight || horizontal < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

}
