using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector2 velocity;

    private float speed = 10;
    float jumpHeight = 4;

    float walkAcceleration = 75;
    float airAcceleration = 30;
    float groundDeceleration = 70;


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
        velocity.x = Mathf.MoveTowards(velocity.x, speed * horiMoveInput, walkAcceleration * Time.deltaTime);
        float vertMoveInput = Input.GetAxisRaw("Vertical");
        velocity.y = Mathf.MoveTowards(velocity.y, speed * vertMoveInput, walkAcceleration * Time.deltaTime);

    }
}
