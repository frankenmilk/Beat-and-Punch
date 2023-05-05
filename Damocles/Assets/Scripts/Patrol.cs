using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{

    public bool slowed = false;
    [SerializeField] float slowedSpeed = 1f;
    [SerializeField] float unslowedSpeed = 23f;
    private float moveSpeed;
    [SerializeField] float enemyJump = 6f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slowed == true)
        {
            moveSpeed = slowedSpeed; 
        }
        else if (slowed == false)
        {
            moveSpeed = unslowedSpeed;
        }
        Patroling();
    }

    void FixedUpdate()
    {

    }

    void Patroling()
    {
        if (IsFacingRight())
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
    }


    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Invis Wall")
        {
            transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), transform.localScale.y);
        }

        if (collision.gameObject.tag == "EnemyJumpPad")
        {
            rb.velocity = new Vector2(rb.velocity.x, enemyJump);
        }
        
    }
}
