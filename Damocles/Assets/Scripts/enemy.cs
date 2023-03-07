using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Animator animator;

    public int maxHealth;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Ouch");

        // Play hurt Animation

        if (currentHealth <= 0)
        {
            Die();
        }

        void Die()
        {
            Debug.Log("enemy died :)");

            // Plays death animation
            animator.SetBool("IsNotAlive", true);

            // Disables the enemy

            GetComponent<Collider2D>().enabled = false;
            GetComponent<Patrol>().enabled = false;
            this.enabled = false;
            
        }
    }

    
}
