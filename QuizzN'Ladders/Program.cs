using System;
using static System.Console;


namespace QuizzN_Ladders
{
     class Program
    {
        static void Main(string[] args)
        {
            CursorVisible = false;
            Clear();

            SetCursorPosition((WindowWidth - 30) / 2, CursorTop);
            WriteLine("Maximized console window is required.");

            SetCursorPosition((WindowWidth - 30) / 2, CursorTop + 2);
            WriteLine("Press any key to continue. Enjoy the game!\n\n\t\t\t\t\t\t- Developers");
            ReadKey(); Clear();

            Title = "Quiz and Climb";
            Game.Run();
            

        }
    }
}
