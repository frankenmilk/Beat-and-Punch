using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfo : MonoBehaviour
{
    [SerializeField] string information;
    [SerializeReference] int cardNumber;

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
     * Note:
     * Adding a \n and then a space after (hello\n hello) causes the result to look like this.
     * 
     * hello
     *  hello
     *  
     * */

    public void SendCardInfo()
    {
        CardPicker.selectedCard = cardNumber;
        Debug.Log(cardNumber);
        TooltipScreenSpaceUI.ShowTooltip_Static(System.Text.RegularExpressions.Regex.Unescape(information));
    }
}
