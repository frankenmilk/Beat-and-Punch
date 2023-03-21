using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float shotSpeed;
    public int arrowDamage = 10;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * shotSpeed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        enemy enemy = hitInfo.GetComponent<enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(arrowDamage);
        }
        Destroy(gameObject);
    }
}
