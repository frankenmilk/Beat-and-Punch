using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            Rigidbody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            Rigidbody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "invis wall")
        {
            Debug.Log("entered");
            transform.localScale = new Vector2(-(Mathf.Sign(Rigidbody.velocity.x)), transform.localScale.y);
        }
        
    }
}
