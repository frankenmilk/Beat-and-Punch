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

    private float nextAttackTime = 0f;
    private float nextArrowTime = 0f;
    private float specialCooldown = 0f;
    private float comboTime = 0f;


    public ProjectileBehaviour ArrowPrefab;
    public ProjectileBehaviour SudokenPrefab;
    public Transform LaunchOffset;


    [SerializeField] Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        if (combo == 1)
        {
            attackDamage = 25;
            comboTime = Time.time + 2f;
        }
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

        if (Time.time >= nextArrowTime)
        {
            if (Input.GetMouseButtonDown(1))
            {
                nextArrowTime = Time.time + .75f;
                Vector3 localScale = transform.localScale;
                Instantiate(ArrowPrefab, LaunchOffset.position, transform.rotation);
            }
        }



        // Specials 
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (PlayerStats.currentSpecial == "ShieldOfAegis")
            {
                PlayerStats.AegisActive = true;
                animator.SetBool("AegisActive", true);
                GetComponent<PlayerMovement>().enabled = false;
            }

            if (Time.time >= specialCooldown)
            {
                if (PlayerStats.currentSpecial == "FiresOfHell")
                {
                    PlayerStats.FireActive = true;
                    animator.SetBool("FireActive", true);
                    GetComponent<PlayerMovement>().enabled = false;
                    rb.velocity = new Vector2(0, rb.velocity.y); // Sets the Player Velocity to 0

                    Collider2D[] SpecialHitEnemies = Physics2D.OverlapCircleAll(rb.position, 2.8f, enemyLayers);

                    // Damages the Enemey
                    foreach (Collider2D enemy in SpecialHitEnemies)
                    {
                        enemy.GetComponent<enemy>().TakeDamage(PlayerStats.FiresOfHellDamage);
                    }

                }
            }

            if (Time.time >= specialCooldown)
            {
                if (PlayerStats.currentSpecial == "HermesCaduceus")
                {
                    PlayerStats.CaduceusActive = true;
                    animator.SetBool("CaduceusActive", true);
                    GetComponent<PlayerMovement>().enabled = false;
                    rb.velocity = new Vector2(0, rb.velocity.y); // Sets the Player Velocity to 0

                    Collider2D[] SpecialHitEnemies = Physics2D.OverlapCircleAll(rb.position, 10f, enemyLayers);

                    // Damages the Enemey
                    foreach (Collider2D enemy in SpecialHitEnemies)
                    {
                        float slowEnd = Time.time + PlayerStats.slowTime;

                        IEnumerator Slower()
                        {
                            enemy.GetComponent<Patrol>().slowed = true;
                            Debug.Log("Losers got slowed");
                            yield return new WaitForSeconds(PlayerStats.slowTime);
                            enemy.GetComponent<Patrol>().slowed = false;
                        }

                        StartCoroutine(Slower());
                    }
                }
            }


            if (Time.time >= specialCooldown)
            {
                if (PlayerStats.currentSpecial == "Sudoken")
                {
                    Vector3 localScale = transform.localScale;
                    Instantiate(SudokenPrefab, LaunchOffset.position, transform.rotation);

                }
            }

            if (Time.time >= specialCooldown)
            {
                if (PlayerStats.currentSpecial == "MedusaHead")
                {
                    GetComponent<PlayerMovement>().enabled = false;
                    animator.SetBool("MedusaActive", true);
                    rb.velocity = new Vector2(0, rb.velocity.y); // Sets the Player Velocity to 0

                    Collider2D[] SpecialHitEnemies = Physics2D.OverlapCircleAll(rb.position, 10f, enemyLayers);


                    foreach (Collider2D enemy in SpecialHitEnemies)
                    {
                        float freezeEnd = Time.time + PlayerStats.freezeTime;

                        IEnumerator Freeze()
                        {
                            enemy.GetComponent<Patrol>().frozen = true;
                            Debug.Log("Losers got F R O Z E N");
                            yield return new WaitForSeconds(PlayerStats.freezeTime);
                            enemy.GetComponent<Patrol>().frozen = false;
                        }

                        StartCoroutine(Freeze());
                    }
                }
            }
                

                if (PlayerStats.currentSpecial == "TheMask")
                {
                    PlayerStats.MaskActive = true;
                    animator.SetBool("MaskActive", true);
                    GetComponent<PlayerMovement>().enabled = false;
                    rb.velocity = new Vector2(0, rb.velocity.y); // Sets the Player Velocity to 0

                    Collider2D[] SpecialHitEnemies = Physics2D.OverlapCircleAll(rb.position, 2.8f, enemyLayers);

                    // Damages the Enemey
                    foreach (Collider2D enemy in SpecialHitEnemies)
                    {
                        enemy.GetComponent<enemy>().TakeDamage(PlayerStats.FiresOfHellDamage);
                    }
                }

                if (PlayerStats.currentSpecial == "")
                {

                }
        }
        
        if (Time.time >= specialCooldown)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                if (PlayerStats.currentSpecial == "ShieldOfAegis")
                {
                    PlayerStats.AegisActive = false;
                    animator.SetBool("AegisActive", false);
                    GetComponent<PlayerMovement>().enabled = true;
                }

                if (PlayerStats.currentSpecial == "FiresOfHell")
                {
                    PlayerStats.FireActive = false;
                    animator.SetBool("FireActive", false);
                    GetComponent<PlayerMovement>().enabled = true;
                    specialCooldown = Time.time + 25f;
                }

                if (PlayerStats.currentSpecial == "HermesCaduceus")
                {
                    PlayerStats.CaduceusActive = false;
                    animator.SetBool("CaduceusActive", false);
                    GetComponent<PlayerMovement>().enabled = true;
                    specialCooldown = Time.time + 20f;
                }

                if (PlayerStats.currentSpecial == "Sudoken")
                {
                    PlayerStats.CaduceusActive = false;
                    specialCooldown = Time.time + 10f;
                }

                if (PlayerStats.currentSpecial == "MedusaHead")
                {
                    PlayerStats.MedusaActive = false;
                    animator.SetBool("MedusaActive", false);
                    GetComponent<PlayerMovement>().enabled = true;
                    specialCooldown = Time.time + 30f;
                }
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
            foreach (Collider2D enemy in hitEnemies)
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
            Gizmos.DrawWireSphere(rb.position, 20f);
        }

}
