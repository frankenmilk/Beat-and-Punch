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


    // Other code
    private BoxCollider2D playerCollider;
    [SerializeField] BoxCollider2D leftWallCollider;
    [SerializeField] BoxCollider2D rightWallCollider;
    private Vector2 velocity;

    [SerializeField] float speed;
    [SerializeField] float jumpHeight;

    // Acceleration stuff
    [SerializeField] float walkAcceleration; // was 75
    [SerializeField] float airAcceleration; // was 30
    [SerializeField] float groundDeceleration; // was 70


    // Gameplay Things //

    // Wall Jump

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


        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, playerCollider.size, 0);
        grounded = false;

        foreach (Collider2D hit in hits)
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
                }
            }
        }

    }

    bool CanJump()
    {
        grounded = false;
        float extraHeightTest = .1f;
        RaycastHit2D bottomRay = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + extraHeightTest, floorLayerMask);
        Color rayColor;
        if (bottomRay.collider != null)
        {
            rayColor = Color.green;
            grounded = true;
        }
        else
        {
            rayColor = Color.red;
            Debug.Log("is not on ground");
        }

        Debug.DrawRay(playerCollider.bounds.center, Vector2.down * (playerCollider.bounds.extents.y + extraHeightTest));

        return bottomRay.collider != null;
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

        velocity.y += Physics2D.gravity.y * Time.deltaTime;


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
