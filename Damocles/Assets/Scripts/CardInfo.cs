using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfo : MonoBehaviour
{
    [SerializeField] string Information;

    /* Quick Tip!!!
     * 
     * Use \n to cut the line and start at the next line down
     * Example: Hello, my name is:\nJohn Pork
     * 
     * Result: 
     * 
     * Hello, my name is:
     * John Pork
     * 
     * 
     * */

    // Start is called before the first frame update
    void Start()
    {
        TooltipScreenSpaceUI.ShowTooltip_Static(Information);
    }
}
