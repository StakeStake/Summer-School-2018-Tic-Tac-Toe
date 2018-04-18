using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Script : MonoBehaviour {
    public Text[] list_of_cells; // An array that contains text of all nine game board cells

    // Game panels
    public GameObject options_panel; // A panel with options regarding play mode and AI difficulty
    public GameObject whos_move_panel; // A panel indicating which player's move it is
    public GameObject game_over_panel; // A panel appearing after the game is over. Contains text indicating a winner

    // Buttons on top af a game window
    public Button settings_button; // A button calling the options panel
    public Button restart_button;
    public Button exit_button;

    // Buttons to chose side against AI
    public Button X_side_button;
    public Button O_side_button;

    // Game variables
    private string player_side; // A string indicating which player's turn it is
    private float time_lag; // Time delay before AI makes a move
    private int AI_Difficulty; // AI difficulty indicator
    private string AI_Side; // Indictor of which side AI plays

    void Awake ()
    {
        game_over_panel.SetActive(false);
        whos_move_panel.SetActive(false);
        restart_button.gameObject.SetActive(false);
        X_side_button.transform.parent.gameObject.SetActive(false);
        SetBoardInteractible(false);
        settings_button.gameObject.SetActive(false);

        options_panel.SetActive(true);
        exit_button.gameObject.SetActive(true);

        AI_Side = null;
        player_side = null;

        time_lag = 1; // Sets time delay
        exit_button.onClick.AddListener(delegate { Application.Quit(); }); // Sets exit button function

        // "Play" button behaves differently when AI is activated
        options_panel.GetComponentInChildren<Button>().onClick.AddListener(delegate
        {
            if (options_panel.GetComponentInChildren<Toggle>().isOn == false)
            {
                Start_Game(true);
            }

            else
            {
                AI_Difficulty = (int)options_panel.GetComponentInChildren<Slider>().value;
                AI_Settings();
            }
        });
    }

    private void AI_Settings()
    {
        options_panel.SetActive(false);
        X_side_button.transform.parent.gameObject.SetActive(true);
        X_side_button.gameObject.SetActive(true);
        O_side_button.gameObject.SetActive(true);

        // Player choses the side he wants to play if AI mode was selected
        X_side_button.onClick.AddListener(delegate { Start_Game(true); });
        O_side_button.onClick.AddListener(delegate { Start_Game(false); });
    }

    public void Start_Game(bool side)
    {
        // Checks whether AI is activated
        if (options_panel.GetComponentInChildren<Toggle>().isOn == true)
        {
            // Sets AI's side
            if (side == true)
            {
                AI_Side = "O";
            }

            else
            {
                AI_Side = "X";
            }
        }

        game_over_panel.SetActive(false);
        restart_button.gameObject.SetActive(false);
        X_side_button.gameObject.SetActive(false);
        O_side_button.gameObject.SetActive(false);
        options_panel.SetActive(false);
        X_side_button.transform.parent.gameObject.SetActive(false);

        whos_move_panel.SetActive(true);
        whos_move_panel.GetComponentInChildren<Text>().text = "Next move: " + player_side;

        player_side = "X"; // X always moves first
        script_reference_buttons(); // Setting a reference to a main script for each button
        SetBoardInteractible(true);
    }

    public void EndTurn() // Ends a turn. Calls for checking conditions function in Winning_Conditions.cs
    {
        if (Winning_Conditions.Checking_Conditions(list_of_cells) == "tie")
        {
            EndingGame("tie");
        }

        else if (Winning_Conditions.Checking_Conditions(list_of_cells) == "change")
        {
            ChangeSides();
        }

        else
        {
            // If a winner is identified, EndingGame returns the winner's side
            EndingGame(Winning_Conditions.Checking_Conditions(list_of_cells));
        }
    }

    public void EndingGame(string end_result)
    {
        SetBoardInteractible(false);
        settings_button.gameObject.SetActive(false);
        whos_move_panel.SetActive(false);

        game_over_panel.SetActive(true);
        restart_button.gameObject.SetActive(true);
        settings_button.gameObject.SetActive(true);
        
        player_side = null;

        // Printing text on "Game Over" panel
        game_over_panel.GetComponentInChildren<Text>().text = "Game Over\n";

        if (end_result == "tie")
        {
            game_over_panel.GetComponentInChildren<Text>().text += "It's a Tie!";
        }

        else
        {
            game_over_panel.GetComponentInChildren<Text>().text += end_result + " Wins!";
        }

        settings_button.onClick.AddListener(delegate { Awake (); });
    }

    public void Game_Restart() // Restarts the game
    {
        game_over_panel.SetActive(false);
        settings_button.gameObject.SetActive(false);
        restart_button.gameObject.SetActive(false);

        whos_move_panel.SetActive(true);
        SetBoardInteractible(true);

        player_side = "X";
        whos_move_panel.GetComponentInChildren<Text>().text = "Next move: " + player_side;   
    }

    void Update()
    {
        // Controls appearance of AI difficulty slider
        if (options_panel.GetComponentInChildren<Toggle>().isOn == true)
        {
            options_panel.transform.GetChild(2).gameObject.SetActive(true);
            if ((int)options_panel.GetComponentInChildren<Slider>().value == 0)
            {
                options_panel.GetComponentInChildren<Slider>().GetComponentInChildren<Text>().text = "Easy";
            }

            else if ((int)options_panel.GetComponentInChildren<Slider>().value == 1)
            {
                options_panel.GetComponentInChildren<Slider>().GetComponentInChildren<Text>().text = "Normal";
            }
        }

        else
        {
            options_panel.transform.GetChild(2).gameObject.SetActive(false);
        }

        // Calls AI to make a move
        if (player_side == AI_Side && AI_Side != null)
        {
            time_lag += time_lag * Time.deltaTime;
            if (time_lag >= 8)
            {
                int AI_Move;
                AI_Move = AI.AI_Move(list_of_cells, AI_Side, AI_Difficulty);
                list_of_cells[AI_Move].text = player_side;
                list_of_cells[AI_Move].GetComponentInParent<Button>().interactable = false;
                EndTurn();
                time_lag = Random.Range(1, 5);
            }
        }
    }

    public void ChangeSides() // Changes a side for a next move
    {
        player_side = (player_side == "X") ? "O" : "X";
        whos_move_panel.GetComponentInChildren<Text>().text = "Next move: " + player_side;
    }

    void SetBoardInteractible(bool active) // Blocks and unblocks board cells to be interactible
    {
        if (active == true)
        {
            for (int i = 0; i < list_of_cells.Length; i++)
            {
                list_of_cells[i].GetComponentInParent<Button>().interactable = true;
                list_of_cells[i].text = "";
            }
        }

        else
        {
            for (int i = 0; i < list_of_cells.Length; i++)
            {
                list_of_cells[i].GetComponentInParent<Button>().interactable = false;
                list_of_cells[i].fontStyle = FontStyle.Normal;
            }
        }

    }

    void script_reference_buttons() // Setting a reference to a main script for buttons
    {
        for (int i = 0; i < list_of_cells.Length; i++)
        {
            list_of_cells[i].GetComponentInParent<Cells_script>().main_script_reference(this);
        }
    }

    // Return Player's and AI's sides to Winning_Consditions.cs and AI.cs when needed
    public string GetPlayerSide()
    {
        return player_side;
    }

    public string GetAISide()
    {
        return AI_Side;
    }
}
