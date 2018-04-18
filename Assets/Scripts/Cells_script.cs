using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cells_script : MonoBehaviour {
    public Button cell_button;
    public Text cell_button_text;

    private Main_Script main_script;

    public void main_script_reference(Main_Script script)
    {
        main_script = script;
    }

    public void make_move()
    {
        if(main_script.GetPlayerSide() != main_script.GetAISide())
        {
            cell_button_text.text = main_script.GetPlayerSide();
            cell_button.interactable = false;
            main_script.EndTurn();
        }
    }
}
