using QuizzN_Ladders;
using System;
using static System.Console;
using System.Threading;

class World
{
    private string[,] Grid;
    private int Rows;
    private int Cols;
    private int prevPlace;
    static public int x = 0;
    static public int y = 0;


    public World(string[,] grid)
    {
        Grid = grid;
        Rows = Grid.GetLength(0);
        Cols = Grid.GetLength(1);
    }

    public void Draw()// this is for primting the board from the text file
    {
        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Cols; x++)
            {
                string element = Grid[y, x];
                if (element == "*" || element == "&")
                {
                    ForegroundColor = ConsoleColor.Red;
                }
                else if (element == "/" || element == "\\" || element == "=")
                {
                    ForegroundColor = ConsoleColor.DarkMagenta;

                }
                
                SetCursorPosition(x, y);
                Write(element);
                ResetColor();


            }

        }
        for (int i = 0; i < Rows; i++)
        {
            SetCursorPosition(100, i);
            BackgroundColor = ConsoleColor.DarkRed;
            Write(" ");
            ResetColor();
        }
    }

    public void DrawPlayer()// drawing player chinecheck nya muna kung kanino turn na tapos kung sino turn yun ang pag lalakarin nya 
    {
        if (Game.IsPlayer1Turn)
        {
            PlayerStand(0, -1, ConsoleColor.Blue, Game.Player2Place);
            PlayerWalk(1, 1, ConsoleColor.Yellow, Game.Player1Place);
            PlayerStand(0, -1, ConsoleColor.Blue, Game.Player2Place);
            PlayerStand(1, 1, ConsoleColor.Yellow, Game.Player1Place);

        }
        else
        {
            PlayerStand(1, 1, ConsoleColor.Yellow, Game.Player1Place);
            PlayerWalk(0, -1, ConsoleColor.Blue, Game.Player2Place);

        }

    }

    private void PlayerWalk(int Placement1, int Placement2, ConsoleColor Color, int PlayerPlace) // eto for walking player
    {
        string[] character = { "(_\\", ">O)" };

        for (prevPlace = PlayerPlace - Game.dice; prevPlace <= PlayerPlace; prevPlace++)
        {
            PlayerCoordinates(prevPlace);

            for (int i = 0; i < character.Length; i++)
            {

                ForegroundColor = Color;
                SetCursorPosition(x - Placement1, y - Placement2);
                Console.Write(character[i]);
                ResetColor();
                y--;

            }

            Thread.Sleep(500);

            PlayerCoordinates(prevPlace);

            for (int j = 0; j < character.Length; j++)
            {
                if (prevPlace == PlayerPlace) continue;
                ForegroundColor = ConsoleColor.Black;
                SetCursorPosition(x - Placement1, y - Placement2);
                Console.Write(character[j]);
                ResetColor();
                y--;

            }

        }

    }
    private void PlayerStand(int Placement1, int Placement2, ConsoleColor Color, int PlayerPlace) // eto yung naka steady lang
    {

        PlayerCoordinates(PlayerPlace);
        string[] character = { "(_\\", ">O)" };


        for (int i = 0; i < character.Length; i++)
        {

            ForegroundColor = Color;
            SetCursorPosition(x - Placement1, y - Placement2);
            Console.Write(character[i]);
            ResetColor();
            y--;

        }

    }

    private void PlayerCoordinates(int pos)// alam mo na to hahahaha
    {
        switch (pos)
        {
            case 1:
                x = 37;
                y = 56;
                break;
            case 2:
                x = 23;
                y = 52;
                break;
            case 3:
                x = 9;
                y = 56;
                break;
            case 4:
                x = 9;
                y = 46;
                break;
            case 5:
                x = 23;
                y = 42;
                break;
            case 6:
                x = 37;
                y = 38;
                break;
            case 7:
                x = 52;
                y = 40;
                break;
            case 8:
                x = 65;
                y = 44;
                break;
            case 9:
                x = 51;
                y = 48;
                break;
            case 10:
                x = 77;
                y = 55;
                break;
            case 11:
                x = 91;
                y = 51;
                break;
            case 12:
                x = 91;
                y = 35;
                break;
            case 13:
                x = 77;
                y = 31;
                break;
            case 14:
                x = 63;
                y = 27;
                break;
            case 15:
                x = 38;
                y = 22;
                break;
            case 16:
                x = 23;
                y = 27;
                break;
            case 17:
                x = 9;
                y = 23;
                break;
            case 18:
                x = 23;
                y = 19;
                break;
            case 19:
                x = 37;
                y = 15;
                break;
            case 20:
                x = 23;
                y = 11;
                break;
            case 21:
                x = 37;
                y = 5;
                break;
            case 22:
                x = 51;
                y = 9;
                break;
            case 23:
                x = 65;
                y = 13;
                break;
            case 24:
                x = 79;
                y = 17;
                break;
            case 25:
                x = 93;
                y = 21;
                break;
        }
    }


}
