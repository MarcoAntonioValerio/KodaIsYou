using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public float stopRaycastCircleSize = 0.2f;
    public float pushRaycastCircleSize = 0.5f;

    public Transform pushTarget;
    

    [SerializeField] List<GameObject> objectsToPushList;
    [SerializeField] int numbertOfPushableObjects;

    private GameObject whoIsMovingNow;
    
    private scp_KodaAnimationTransition kodaAnimationScript;    
    




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
        numbertOfPushableObjects = GameObject.FindGameObjectsWithTag("Pushable").Count();
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
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), stopRaycastCircleSize, whatStopsMovement))
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
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), stopRaycastCircleSize, whatStopsMovement))
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
        //Check If we are colliding with movable object.
        if (Physics2D.OverlapCircle(transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), pushRaycastCircleSize, thingICanMove))
        {

            //Stores the first collision object an a GameObject variable.
            GameObject objToAdd = Physics2D.OverlapCircle(transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), pushRaycastCircleSize, thingICanMove).gameObject;
            

            //Adds every collision Gameobject to the List
            for (int i = 0; i <= numbertOfPushableObjects; i++)
            {                
                objectsToPushList.Add(objToAdd); 
                
                if (Physics2D.OverlapCircle(objToAdd.transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), pushRaycastCircleSize, thingICanMove))
                {
                    objToAdd = Physics2D.OverlapCircle(objToAdd.transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), pushRaycastCircleSize, thingICanMove).gameObject;
                }
                else
                {
                    i = numbertOfPushableObjects;
                }
            }

            //If the object/objects we are moving do not encounter an unmovable space move them
            if (!Physics2D.OverlapCircle(pushTarget.transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), stopRaycastCircleSize, whatStopsMovement))
            {
                //Moves the objects in the List.
                for (int i = 0; i < objectsToPushList.Count; i++)
                {

                    pushTarget.transform.position = objectsToPushList[i].transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    objectsToPushList[i].layer = 9;
                    objectsToPushList[i].transform.position = pushTarget.transform.position;

                }
            }
            //The object itself should become unmovable if the next place is not pushable
            else if(Physics2D.OverlapCircle(pushTarget.transform.position + new Vector3(Input.GetAxisRaw("Horizontal") , 0f, 0f), stopRaycastCircleSize, whatStopsMovement))
            {
                for (int i = 0; i < objectsToPushList.Count; i++)
                {
                    objectsToPushList[i].layer = 8;
                    pushTarget.transform.position = objectsToPushList[i].transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    

                }
            }
            
        }
        //Clears the List if not colliding with a Pushable objects anymore
        objectsToPushList.Clear();
    }
}
