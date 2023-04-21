using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public LayerMask enemyLayers;
    public Transform attackPoint;

    public float attackRange = 0.5f;
    public int attackDamage = 25;
    [SerializeField] int combo = 0;

    public float attackRate = .2f; // maybe became useless ----> it became useless but I'll keep here just in case
    private float timeAtAttack;
    private float nextAttackTime = 0f;
    private float nextAttackTime2 = 0f;

    public ProjectileBehaviour ProjectilePrefab;
    public Transform LaunchOffset;


    [SerializeField] Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        if (combo == 2)
        {
            attackDamage = 50;
        }
        else
        {
            attackDamage = 25;
        }
        // Base attack
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f;
                timeAtAttack = Time.time;

            }
        } 
        
        // Begins the combo attack
        if (combo > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }

        if (Time.time >= nextAttackTime2)
        {
            if (Input.GetMouseButtonDown(1))
            {
                nextAttackTime2 = Time.time + .75f;
                Vector3 localScale = transform.localScale;
                Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
            }
        }
        

    }

    public void Attack()
    {
        // Play an attack
        if (combo == 0)
        {
            animator.SetTrigger("Attack");
        }
        else if (combo == 1)
        {
            animator.SetTrigger("1");
        } 
        else if (combo == 2)
        {
            animator.SetTrigger("2");
        }
        

        // Detect enemies in range of attack 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<enemy>().TakeDamage(attackDamage);
        }
    }
    public void StartCombo()
    {
        if (combo < 3)
        {
            combo++;
        }
    }

    public void FinishAnimaton()
    {
        combo = 0;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
