using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    public Animator animator;
    public int combo;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Combos();
    }

    public void Combos()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            animator.SetTrigger("" + combo);  
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
}
