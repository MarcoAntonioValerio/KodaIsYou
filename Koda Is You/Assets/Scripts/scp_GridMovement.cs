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
    private bool isFacingRight = true;
    private Vector2 movement;


    //-------------------------------------------
    void Start()
    {
        SetupStuff();
    }     

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    }
    
    private void FixedUpdate()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        if (isMoving == false)
        {
            Moving2(movement);
        }
        FlipSprite(horizontal);
    }
    //----------------------------------------------------------

    private void SetupStuff()
    {
        isFacingRight = true;
        movePoint.parent = null;
        whoIsMovingNow = gameObject;
        kodaAnimationScript = FindObjectOfType<scp_KodaAnimationTransition>();
    }
    private void Moving()
    {

        if (whoIsMovingNow != null)
        {
            whoIsMovingNow.transform.position = Vector3.MoveTowards(whoIsMovingNow.transform.position, movePoint.position,
                                                            moveSpeed * Time.deltaTime);

            //Vector2 pos = movePoint.position;

            //whoIsMovingNow.transform.position = this.GetComponent<Rigidbody2D>().MovePosition(pos);

            if (Vector3.Distance(whoIsMovingNow.transform.position, movePoint.position) == 0f)
            {

                if (Input.GetAxisRaw("Horizontal") == 1f || Input.GetAxisRaw("Horizontal") == -1f)
                {
                    isMoving = true;
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                    if (whoIsMovingNow.tag == "Player" && Input.GetAxisRaw("Horizontal") == 1f)
                    {
                        kodaAnimationScript.KodaAnimationRight();
                        //FlipSprite(Input.GetAxisRaw("Horizontal"));

                    }
                    if (whoIsMovingNow.tag == "Player" && Input.GetAxisRaw("Horizontal") == -1f)
                    {
                        FlipSprite(Input.GetAxisRaw("Horizontal"));
                        kodaAnimationScript.KodaAnimationLeft();

                    }
                    isMoving = false;
                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    isMoving = true;
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    if (whoIsMovingNow.tag == "Player" && Input.GetAxisRaw("Vertical") == 1)
                    {
                        kodaAnimationScript.KodaAnimationUp();
                    }
                    if (whoIsMovingNow.tag == "Player" && Input.GetAxisRaw("Vertical") == -1)
                    {
                        kodaAnimationScript.KodaAnimatinionDown();
                    }
                    isMoving = false;
                }
                else
                {
                    if (whoIsMovingNow.tag == "Player")
                    {
                        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
                        {
                            kodaAnimationScript.KodaAnimationIdle();
                        }


                    }
                }
            }
        }
        

        
    }

    private void Moving2(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void FlipSprite(float horizontal)
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
