using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_GridMovement : MonoBehaviour
{
    /// <summary>
    /// ToDo
    ///*Sort out flipping!
    /// </summary>




    [Header("moving Values")]
    public float moveSpeed = 5f;
    public Transform movePoint;


    private GameObject whoIsMovingNow;
    private scp_KodaAnimationTransition kodaAnimationScript;
    private bool isMoving = false;

    void Start()
    {
        movePoint.parent = null;
        whoIsMovingNow = this.gameObject;
        kodaAnimationScript = FindObjectOfType<scp_KodaAnimationTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == false)
        {
            Moving();
        } 

    }

    public void Moving()
    {
        bool facingRight = true;

        whoIsMovingNow.transform.position = Vector3.MoveTowards(whoIsMovingNow.transform.position, movePoint.position,
                                                            moveSpeed * Time.deltaTime);

        if (Vector3.Distance(whoIsMovingNow.transform.position, movePoint.position) == 0f)
        {
            
            if (Input.GetAxisRaw("Horizontal") == 1f || Input.GetAxisRaw("Horizontal") == -1f)
            {
                isMoving = true;
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                if (whoIsMovingNow.tag == "Player" && Input.GetAxisRaw("Horizontal") == 1f)
                {
                    kodaAnimationScript.KodaRight();
                    facingRight = true;
                    
                }
                if (whoIsMovingNow.tag == "Player" && Input.GetAxisRaw("Horizontal") == -1f)
                {
                    
                    if (!facingRight)
                    {
                        FlipSprite();
                        facingRight = false;
                    }
                    
                    kodaAnimationScript.KodaLeft();

                    
                }                
                isMoving = false;
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                isMoving = true;
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                if (whoIsMovingNow.tag == "Player" && Input.GetAxisRaw("Vertical") == 1)
                {
                    kodaAnimationScript.KodaUp();
                }
                if (whoIsMovingNow.tag == "Player" && Input.GetAxisRaw("Vertical") == -1)
                {
                    kodaAnimationScript.KodaDown();
                }
                isMoving = false;
            }
            else
            {
                if (whoIsMovingNow.tag == "Player")
                {
                    if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
                    {
                        kodaAnimationScript.KodaIdle();
                    }
                    

                }
            }
        }
    }

    public void FlipSprite()
    {
        var objectLocalScale = gameObject.transform.localScale; 
        objectLocalScale.x *= -1;        
        transform.localScale = objectLocalScale;
    }
}
