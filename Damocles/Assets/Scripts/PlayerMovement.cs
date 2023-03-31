using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    private float Yvelocity;

    // For movement
    private float horizontal;
    [SerializeField] private float speed = 16f;
    [SerializeField] private float jumpingPower = 16f;
    private bool isFacingRight = true;

    // For Wall Slide & Wall Jump
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    // For collision
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    // Player Damage Stuff
    private bool canMove = true;
    private float timeTime;

    int damage25 = 25;
    //int damage50 = 50;

    [SerializeField] float knockBackForce;
    [SerializeField] float knockBackForceUp;


    // Start is called before the first frame update
    void Update()
    {
        if (Time.time >= timeTime + 1f)
        {
            canMove = true;
        }


        if (animator.GetBool("IsWallSliding") == true)
        {
            animator.SetBool("IsJumping", false);
        }

        if (rb.velocity.y < .01 && !IsWalled())
        {
            animator.SetBool("IsJumping", false);
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        animator.SetFloat("Yvelocity", Mathf.Abs(rb.velocity.y));

        horizontal = Input.GetAxisRaw("Horizontal");

        if (canMove == true)
        {
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                animator.SetBool("IsJumping", true);
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }
        
        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        if (!isWallJumping && canMove == true)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        
    }

    private float Timer()
    {
        return Time.deltaTime;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            animator.SetBool("IsWallSliding", true);
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            animator.SetBool("IsWallSliding", false);
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x / 2, wallJumpingPower.y / 2);
            wallJumpingCounter = 0f;
            
            if(transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                transform.Rotate(0, 180, 0);
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
        }
    }

    // Player Damage area
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "enemy25")
        {
            TakeDamage(damage25);
        }
    }

    public void Knockback()
    {
        Transform attacker = GetClosestDamageSource();
        Vector2 knockbackDirection = new Vector2(transform.position.x - attacker.transform.position.x, 0);
        rb.velocity = new Vector2(knockbackDirection.x, knockBackForceUp) * knockBackForce;
    }

    public Transform GetClosestDamageSource()
    {
        GameObject[] DamageSources = GameObject.FindGameObjectsWithTag("enemy25");
        DamageSources = GameObject.FindGameObjectsWithTag("enemy50");
        float closestDistance = Mathf.Infinity;
        Transform currentClosestDamageSource = null;

        foreach (GameObject go in DamageSources)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                currentClosestDamageSource = go.transform;
            }
        }
        return currentClosestDamageSource;
    }

    void TakeDamage(int damage)
    {
        canMove = false;
        timeTime = Time.time;
        Knockback();

        PlayerStats.playerHealth -= damage;

        animator.SetTrigger("Ouch");

        // Plays death Animation if current health reaches or goes below 0
        if (PlayerStats.playerHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {

        // Plays death animation
        animator.SetBool("IsNotAlive", true);

        // Disables the enemy

        GetComponent<Collider2D>().enabled = false;


    }


}
