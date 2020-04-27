using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_GridMovement : MonoBehaviour
{
    /// <summary>
    /// ToDo
    ///*Sort out flipping!
    /// </summary>




    [Header("Moving Values")]
    public float moveSpeed = 5f;
    public Transform movePoint;
    [Header("Collision Stuff")]
    public LayerMask whatStopsMovement;
    public LayerMask thingICanMove;
    public float raycastCircleSize = 0.2f;


    private GameObject whoIsMovingNow;
    private scp_KodaAnimationTransition kodaAnimationScript;
    private bool isMoving = false;
    private GameObject[] chainedObjects;
    
    


    //-------------------------------------------
    void Start()
    {
        VariablesInitialisation();
    }     

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        Moving();        
        kodaAnimationScript.FlipSprite(horizontal);
    }
    
    
    //----------------------------------------------------------

    private void VariablesInitialisation()
    {               
        movePoint.parent = null;
        whoIsMovingNow = gameObject;
        kodaAnimationScript = FindObjectOfType<scp_KodaAnimationTransition>();
    }
    private void Moving()
    {
        //Checking that whatIsMovingNow is not empty
        if (whoIsMovingNow != null)
        {
            //Translation code
            whoIsMovingNow.transform.position = Vector3.MoveTowards(whoIsMovingNow.transform.position, movePoint.position,
                                                            moveSpeed * Time.deltaTime);

            
            //If the sprite has completely reached the destination
            if (Vector3.Distance(whoIsMovingNow.transform.position, movePoint.position) <= 0.05f)
            {
                //Horizontal Movement
                if (Mathf.Abs( Input.GetAxisRaw("Horizontal")) == 1f)
                {
                        //Collision check
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), raycastCircleSize, whatStopsMovement))
                    {
                        //Updating the position of the movePoint
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                        HorizontalPush();
                        HorizontalAnimations();
                    }
                }
                //Vertical movement
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    //Collision check
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), raycastCircleSize, whatStopsMovement))
                    {
                        //Updating the position of the movePoint
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);

                        VerticalAnimations();
                    }
                        
                }

                //Idle
                else if ((Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0) && whoIsMovingNow.tag == "Player")
                {
                    kodaAnimationScript.KodaAnimationIdle();
                }



            }
        }
        

        
    }

    private void VerticalAnimations()
    {
        //Animations
        if (whoIsMovingNow.tag == "Player" && Input.GetAxisRaw("Vertical") == 1)
        {
            kodaAnimationScript.KodaAnimationUp();
        }
        if (whoIsMovingNow.tag == "Player" && Input.GetAxisRaw("Vertical") == -1)
        {
            kodaAnimationScript.KodaAnimatinionDown();
        }
    }

    private void HorizontalAnimations()
    {
        //Animations
        if (whoIsMovingNow.tag == "Player" && Input.GetAxisRaw("Horizontal") == 1f)
        {
            kodaAnimationScript.KodaAnimationRight();

        }
        if (whoIsMovingNow.tag == "Player" && Input.GetAxisRaw("Horizontal") == -1f)
        {
            kodaAnimationScript.KodaAnimationLeft();

        }
    }

    private void HorizontalPush()
    {
        ///SORT THIS SCRIPT TO ALLOW CHAINED PUSH



        //Will push if the next moving place contains a gameObject wirh a thingsICanMove layerMask.
        if (Physics2D.OverlapCircle(transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), raycastCircleSize, thingICanMove))
        {       
            chainedObjects[0] = Physics2D.OverlapCircle(transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), raycastCircleSize, thingICanMove).gameObject;
            int arrayPos = 0;
            while (Physics2D.OverlapCircle(chainedObjects[arrayPos].transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), raycastCircleSize, thingICanMove))
            {
                chainedObjects[arrayPos] = Physics2D.OverlapCircle(chainedObjects[arrayPos].transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), raycastCircleSize, thingICanMove).gameObject;
                chainedObjects[arrayPos].transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            } 
        }
    }
}
