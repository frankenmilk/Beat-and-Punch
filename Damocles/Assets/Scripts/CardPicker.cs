using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPicker : MonoBehaviour
{
    public static int selectedCard;
    private int timesWithoutPicked;
    public void ChooseCard()
    {
        if (selectedCard > 0)
        {
            if (selectedCard == 1)
            {

            }
            else if (selectedCard == 2)
            {

            }
            else if (selectedCard == 3)
            {

            }
            else if (selectedCard == 4)
            {

            }
            else if (selectedCard == 5)
            {

            }
            else if (selectedCard == 6)
            {

            }
            else if (selectedCard == 7)
            {

            }
            else if (selectedCard == 8)
            {

            }
            else if (selectedCard == 9)
            {

            }
            else if (selectedCard == 10)
            {

            }
            else if (selectedCard == 11)
            {

            }
            else if (selectedCard == 12)
            {

            }
            else if (selectedCard == 13)
            {

            }
            else if (selectedCard == 14)
            {

            }
            else if (selectedCard == 15)
            {

            }
            else if (selectedCard == 16)
            {

            }
            else if (selectedCard == 17)
            {

            }
            else if (selectedCard == 18)
            {

            }
            else if (selectedCard == 19)
            {

            }
            else if (selectedCard == 20)
            {

            }
            else if (selectedCard == 21)
            {

            }
            else if (selectedCard == 22)
            {

            }
            else if (selectedCard == 23)
            {

            }
            else if (selectedCard == 24)
            {

            }
            else if (selectedCard == 25)
            {

            }
        }
        else if (selectedCard <= 0)
        {
            if (timesWithoutPicked < 1)
            {
                Debug.Log(selectedCard);
                string information = "Please pick a card before confirming!";
                TooltipScreenSpaceUI.ShowTooltip_Static(System.Text.RegularExpressions.Regex.Unescape(information));
                timesWithoutPicked++;
            }
            else if (timesWithoutPicked == 1)
            {
                string information = "Please just choose a card";
                TooltipScreenSpaceUI.ShowTooltip_Static(System.Text.RegularExpressions.Regex.Unescape(information));
                timesWithoutPicked++;
            }
        }
        


    }
}
