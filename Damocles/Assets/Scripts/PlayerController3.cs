using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour
{
    Rigidbody2D playerBody;
    BoxCollider2D playerCollider;

    // Set layermasks in inspector
    [SerializeField] private LayerMask floorLayerMask;
    [SerializeField] private LayerMask wallLayerMask;

    private bool touchingWall;
    private bool grounded;

    private RaycastHit2D landingHit;
    private RaycastHit2D leftHit;
    private RaycastHit2D rightHit;
    private RaycastHit2D topHit;

    float rightPositionX;
    float topPositionY;
    float bottomPositionY;
    float leftPositionX;

    private Vector2 velocity;

    [SerializeField] float speed;
    [SerializeField] float jumpHeight;

    // Acceleration stuff
    [SerializeField] float walkAcceleration; // was 75
    [SerializeField] float airAcceleration; // was 30
    [SerializeField] float groundDeceleration; // was 70



    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        GetInput();

        landingHit = Physics2D.Raycast(transform.position, Vector2.down, .01f, floorLayerMask);
        leftHit = Physics2D.Raycast(new Vector2(leftPositionX + transform.position.x, this.transform.position.y), new Vector2(leftPositionX - 0.2f, 0.0f), 0.2f);
        rightHit = Physics2D.Raycast(new Vector2(rightPositionX + transform.position.x, this.transform.position.y), new Vector2(rightPositionX + 0.2f, 0.0f), 0.2f);
        topHit = Physics2D.Raycast(new Vector2(this.transform.position.x, topPositionY + transform.position.y), new Vector2(transform.position.x, 0.2f), 0.2f);


        //Debug.Log(leftHit.collider.tag);
        if (landingHit.collider != null)
        {
            if (landingHit.collider.tag == "floor")
            {
                grounded = true;
                Debug.Log(landingHit.collider.name);
            }
        }
        
        if (topHit.collider != null)
        {
            if (topHit.collider.tag == "floor")
            {
                grounded = false;
                Debug.Log("Hit the top");
            }
        }
        if (leftHit.collider != null)
        {
            if (leftHit.collider.tag == "wall")
            {

                touchingWall = true;
            }

        }


        if (rightHit.collider != null)
        {
            if (rightHit.collider.tag == "wall")
            {
                touchingWall = true;
            }
        }




        if (rightHit.collider == null && leftHit.collider == null)
        {
            touchingWall = false;
        }

        if (touchingWall)
        {
            grounded = true;
        }



           Debug.DrawRay(transform.position, Vector2.down, Color.red);
        /*
           Debug.DrawRay(new Vector2(this.transform.position.x, topPositionY + transform.position.y), new Vector2(0, 0.2f), Color.red);
           Debug.DrawRay(new Vector2(leftPositionX + transform.position.x, this.transform.position.y), new Vector2(leftPositionX - 0.2f, 0), Color.red);
           Debug.DrawRay(new Vector2(rightPositionX + transform.position.x, this.transform.position.y), new Vector2(0.2f, 0), Color.red);
        */

    }

    void GetInput()
    {

        transform.Translate(velocity * Time.deltaTime);
        float horiMoveInput = Input.GetAxisRaw("Horizontal");

        // The jump code, checks if you are grounded then uses the jump button (space) to go up
        if (grounded)
        {
            velocity.y = 0;

            if (Input.GetButtonDown("Jump"))
            {
                // Calculate the velocity required to achieve the target jump height.
                velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            }
        }

        



        if (horiMoveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * horiMoveInput, walkAcceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);
        }
    }
}
