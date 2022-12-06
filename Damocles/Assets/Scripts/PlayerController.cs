using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector2 velocity;

    private bool grounded;

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
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
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

        // Collider stuff (very cool, just don't look at it)
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);
        grounded = false;

        foreach (Collider2D hit in hits)
        {
            if (hit == boxCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

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
}
