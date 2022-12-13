using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerBody;

    private bool touchingWall;
    private bool grounded;

    [SerializeField] private LayerMask floorLayerMask;
    [SerializeField] private LayerMask wallLayerMask;


    private BoxCollider2D playerCollider;
    [SerializeField] Collider2D wallCollider;
    [SerializeField] Collider2D floorCollider;
    private Vector2 velocity;

    [SerializeField] float speed;
    [SerializeField] float jumpHeight;

    // Acceleration stuff
    [SerializeField] float walkAcceleration; // was 75
    [SerializeField] float airAcceleration; // was 30
    [SerializeField] float groundDeceleration; // was 70


    // Gameplay Things //

    // Wall Jump

    bool isOnWall;
    [SerializeField] float wallKick;

    // Double Jump



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

        Collider2D[] floorHits = Physics2D.OverlapBoxAll(transform.position, playerCollider.size, 0, floorLayerMask);
        Collider2D[] wallHits = Physics2D.OverlapBoxAll(transform.position, playerCollider.size, 0, wallLayerMask);

        grounded = false;
        isOnWall = false;

        foreach (Collider2D hit in wallHits)
        {

            if (hit == playerCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(playerCollider);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                WallJump();
            }
        }

        foreach (Collider2D hit in floorHits)
        {

            if (hit == playerCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(playerCollider);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
                {
                    grounded = true;
                    isOnWall = false;
                }
            }
        }

    }

    void WallJump()
    {
        isOnWall = true;
        velocity.y = 0;

        // This jump should push you away from the wall and up
        if (Input.GetButtonDown("Jump"))
        {
            // Calculate the velocity required to achieve the target jump height.
            velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            velocity.x = 
        }

    }
    

    // Is the actual movement stuff
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

        if (isOnWall == false || grounded == false && isOnWall == false)
        {
            velocity.y += Physics2D.gravity.y * Time.deltaTime;
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
