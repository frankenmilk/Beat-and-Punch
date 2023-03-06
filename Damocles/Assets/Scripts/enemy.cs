using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int maxHealth;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDmaage(int damage)
    {
        currentHealth -= damage;

        // Play hurt Animation

        if (currentHealth <= 0)
        {
            Die();
        }

        void Die()
        {
            Debug.Log("enemy died :)");

            // Die animation

            // Disable the enemy
        }
    }

    
}
