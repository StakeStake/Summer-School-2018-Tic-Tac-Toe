using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Winning_Conditions {
    public static string Checking_Conditions(Text[] fields)
    {
        bool tie_indicator = false;
        //Checks if all cells are filled
        for (int i = 0; i < fields.Length; i++)
        {
            if (!fields[i].text.Equals("X") && !fields[i].text.Equals("O"))
            {
                tie_indicator = false;
                break;
            }

            else
            {
                tie_indicator = true;
            }
        }

        // Checking rows for winning conditions
        if (fields[0].text == fields[1].text && fields[1].text == fields[2].text && fields[2].text != "")
        {
            return fields[2].text;
        }

        else if (fields[3].text == fields[4].text && fields[4].text == fields[5].text && fields[5].text != "")
        {
            return fields[5].text;
        }

        else if (fields[6].text == fields[7].text && fields[7].text == fields[8].text && fields[8].text != "")
        {
            return fields[8].text;
        }

        // Checking columns for winning conditions
        else if (fields[0].text == fields[3].text && fields[3].text == fields[6].text && fields[6].text != "")
        {
            return fields[6].text;
        }

        else if (fields[1].text == fields[4].text && fields[4].text == fields[7].text && fields[7].text != "")
        {
            return fields[7].text;
        }

        else if (fields[2].text == fields[5].text && fields[5].text == fields[8].text && fields[8].text != "")
        {
            return fields[8].text;
        }

        // Checking diagonals for winning conditions
        else if (fields[0].text == fields[4].text && fields[4].text == fields[8].text && fields[8].text != "")
        {
            return fields[8].text;
        }

        else if (fields[2].text == fields[4].text && fields[4].text == fields[6].text && fields[6].text != "")
        {
            return fields[6].text;
        }

        // Indicates a tie if none side has won and all cells are filled
        else if (tie_indicator == true)
        {
            return "tie";
        }

        // Returns a call to side changing function
        else
        {
            return "change";
        }
    }
}
