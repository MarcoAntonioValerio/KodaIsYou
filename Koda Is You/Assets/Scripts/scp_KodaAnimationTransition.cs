using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_KodaAnimationTransition : MonoBehaviour
{
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
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
        
}
