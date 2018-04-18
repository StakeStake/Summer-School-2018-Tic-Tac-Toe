using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI {
    public static int AI_Move(Text[] cells, string ai_side, int difficulty)
    {
        int cell_move;
        string enemy_side = (ai_side == "X") ? "O" : "X";

        // "Easy" difficulty level
        if (difficulty == 0)
        {
            // Makes a random move
            do
            {
                cell_move = Random.Range(0, 8);
            } while (cells[cell_move].text.Equals("X") || cells[cell_move].text.Equals("O"));
            return cell_move;
        }

        // "Normal" difficulty level
        else if (difficulty == 1)
        {
            //Checking rows for possible winning conditions
            for (int i = 0; i < 7; i += 3)
            {
                if (cells[i].text == ai_side && cells[i + 1].text == ai_side && cells[i + 2].text == "")
                {
                    return i + 2;
                }
                else if (cells[i].text == ai_side && cells[i + 2].text == ai_side && cells[i + 1].text == "")
                {
                    return i + 1;
                }
                else if (cells[i + 1].text == ai_side && cells[i + 2].text == ai_side && cells[i].text == "")
                {
                    return i;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                //Checking columns for possible winning conditions
                if (cells[i].text == ai_side && cells[i + 3].text == ai_side && cells[i + 6].text == "")
                {
                    cell_move = i + 6;
                    return i + 6;
                }

                else if (cells[i].text == ai_side && cells[i + 6].text == ai_side && cells[i + 3].text == "")
                {
                    cell_move = i + 3;
                    return i + 3;
                }

                else if (cells[i + 3].text == ai_side && cells[i + 6].text == ai_side && cells[i].text == "")
                {
                    cell_move = i;
                    return cell_move;
                }
            }

            //Checking diagonals for possible winning conditions
            if (cells[0].text == ai_side && cells[4].text == ai_side && cells[8].text == "")
            {
                return 8;
            }

            else if (cells[0].text == ai_side && cells[8].text == ai_side && cells[4].text == "")
            {
                return 4;
            }

            else if (cells[4].text == ai_side && cells[8].text == ai_side && cells[0].text == "")
            {
                return 0;
            }

            else if (cells[2].text == ai_side && cells[4].text == ai_side && cells[6].text == "")
            {
                return 6;
            }

            else if (cells[2].text == ai_side && cells[6].text == ai_side && cells[4].text == "")
            {
                return 4;
            }

            else if (cells[4].text == ai_side && cells[6].text == ai_side && cells[2].text == "")
            {
                return 2;
            }

            //Checking if enemy has one move left to win

            for (int i = 0; i < 7; i += 3)
            {
                if (cells[i].text == enemy_side && cells[i + 1].text == enemy_side && cells[i + 2].text == "")
                {
                    return i + 2;
                }
                else if (cells[i].text == enemy_side && cells[i + 2].text == enemy_side && cells[i + 1].text == "")
                {
                    return i + 1;
                }
                else if (cells[i + 1].text == enemy_side && cells[i + 2].text == enemy_side && cells[i].text == "")
                {
                    return i;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (cells[i].text == enemy_side && cells[i + 3].text == enemy_side && cells[i + 6].text == "")
                {
                    cell_move = i + 6;
                    return i + 6;
                }

                else if (cells[i].text == enemy_side && cells[i + 6].text == enemy_side && cells[i + 3].text == "")
                {
                    cell_move = i + 3;
                    return i + 3;
                }

                else if (cells[i + 3].text == enemy_side && cells[i + 6].text == enemy_side && cells[i].text == "")
                {
                    cell_move = i;
                    return cell_move;
                }
            }

            if (cells[0].text == enemy_side && cells[4].text == enemy_side && cells[8].text == "")
            {
                return 8;
            }

            else if (cells[0].text == enemy_side && cells[8].text == enemy_side && cells[4].text == "")
            {
                return 4;
            }

            else if (cells[4].text == enemy_side && cells[8].text == enemy_side && cells[0].text == "")
            {
                return 0;
            }

            else if (cells[2].text == enemy_side && cells[4].text == enemy_side && cells[6].text == "")
            {
                return 6;
            }

            else if (cells[2].text == enemy_side && cells[6].text == enemy_side && cells[4].text == "")
            {
                return 4;
            }

            else if (cells[4].text == enemy_side && cells[6].text == enemy_side && cells[2].text == "")
            {
                return 2;
            }

            // Make random move if conditions were not met
            do
            {
                cell_move = Random.Range(0, 8);
            } while (cells[cell_move].text.Equals("X") || cells[cell_move].text.Equals("O"));

            return cell_move;
        }   

        // Backup random move generator
        do
        {
            cell_move = Random.Range(0, 8);
        } while (cells[cell_move].text.Equals("X") || cells[cell_move].text.Equals("O"));

        return cell_move;
    }

}
