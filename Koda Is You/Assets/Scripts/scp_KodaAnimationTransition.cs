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

    public void KodaRight()
    {
        animator.SetInteger("DirectionController",1);
    }
    public void KodaLeft()
    {        
        animator.SetInteger("DirectionController", 1); 
    }
    public void KodaIdle()
    {
        animator.SetInteger("DirectionController", 0);
    }
    public void KodaUp()
    {
        animator.SetInteger("DirectionController", 2);
    }
    public void KodaDown()
    {
        animator.SetInteger("DirectionController", 3);
    }
        
}
