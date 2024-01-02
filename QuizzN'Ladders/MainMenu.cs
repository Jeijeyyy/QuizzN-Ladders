using System;
using System.Net;
using System.Reflection.Emit;
using System.Threading;
using static System.Console;


namespace QuizzN_Ladders
{
    class MainMenu
    {

       static private int SelectedIndex = 0;

        static public void Run()// Eto yung para sa malupit na main title
        {
            
            CursorVisible = false;
            string[] mainTitle = { "    .aMMMb  dMP dMP dMP dMMMMMP   .aMMMb  dMMMMb  dMMMMb    dMP     .aMMMb  dMMMMb  dMMMMb  dMMMMMP dMMMMb  .dMMMb ", "   dMP\"dMP dMP dMP amr   .dMP\"   dMP\"dMP dMP dMP dMP VMP   dMP     dMP\"dMP dMP VMP dMP VMP dMP     dMP.dMP dMP\" VP ", "  dMP.dMP dMP dMP dMP  .dMP\"    dMMMMMP dMP dMP dMP dMP   dMP     dMMMMMP dMP dMP dMP dMP dMMMP   dMMMMK\"  VMMMb   ", "dMP.MMP dMP.aMP dMP .dMP\"     dMP dMP dMP dMP dMP.aMP   dMP     dMP dMP dMP.aMP dMP.aMP dMP     dMP\"AMF dP .dMP   " , " VMMP\"MP VMMMP\" dMP dMMMMMP   dMP dMP dMP dMP dMMMMP\"   dMMMMMP dMP dMP dMMMMP\" dMMMMP\" dMMMMMP dMP dMP  VMMMP\"      " };
            string[] subTitle = { "                                                            _                                                    ", "  __,      ,    ,__,  __,   /,  _     __,   ,__,   __/      // __,   __/   __/   _   ,_      __  __,   ,____,   _ \r\n", "(_/(_   _/_)__/ / (_(_/(__/(__(/_   (_/(__/ / (__(_/(_   _(/_(_/(__(_/(__(_/(__(/__/ (_   _(_/_(_/(__/ / / (__(/_", "                                                                                             _/_                   \r\n" };
            int height = Convert.ToInt32(((WindowHeight * 83) * .01));


            for (int i = 0; i < height; i++)
            {

                for (int j = 0; j < WindowWidth; j++)
                {
                    BackgroundColor = ConsoleColor.White;
                    Write(" ");
                }

            }

            int pos = Convert.ToInt32((height * 37) * 0.01);

            for (int k = 0; k < mainTitle.Length; k++)
            {

                BackgroundColor = ConsoleColor.White;
                ForegroundColor = ConsoleColor.Black;
                SetCursorPosition((WindowWidth - mainTitle[k].Length) / 2, pos);
                WriteLine(mainTitle[k]);
                pos++;
                Thread.Sleep(20);

            }

            for (int o = 0; o < subTitle.Length; o++)
            {

                BackgroundColor = ConsoleColor.White;
                ForegroundColor = ConsoleColor.Black;
                SetCursorPosition((WindowWidth - subTitle[o].Length) / 2, pos);
                WriteLine(subTitle[o]);
                pos++;
                Thread.Sleep(20);

            }


            SetCursorPosition(0, 0);
            for (int l = 0; l < WindowWidth; l++)
            {
                BackgroundColor = ConsoleColor.Black;
                Write(" ");
                Thread.Sleep(10);
                ResetColor();
            }

            for (int m = WindowWidth - 1; m >= 0; m--)
            {
                BackgroundColor = ConsoleColor.DarkRed;
                SetCursorPosition(m, height);
                Write(" ");
                Thread.Sleep(10);
                ResetColor();
            }

            string a = "Press any key to continue...";
            SetCursorPosition((WindowWidth - 1) - a.Length, height);
            BackgroundColor = ConsoleColor.DarkRed;
            Write(a);
            ResetColor();


            ForegroundColor = ConsoleColor.DarkGray;
            SetCursorPosition(0, height + 1);
            WriteLine(@"By: Jomuel Abania
Lyndon Salcedo
(c) To Super Smash Bros Ultimate For The TitleScreen");

            ReadKey();
            OptionNavigation();// eto naman ung menu para sa player vs computer kuha ko lang sa dating menu natin
            
        }
        
       static public void DisplayOptions()
        {
            int i = 0;
            string[] Options = { "Player", "Computer", };
            SetCursorPosition((WindowWidth - 30) / 2, CursorTop);


            for (i = 0; Options.Length > i; i++)
            {
                ResetColor();
                Write("    ");
                string currentOption = Options[i];
                if (i == SelectedIndex)
                {
                    BackgroundColor = ConsoleColor.DarkRed;
                    Write("|" + currentOption + "|");
                }
                else
                {
                    Write(currentOption);
                }

            }
            ResetColor();
        }

        static public void OptionNavigation()
        {
            ConsoleKey keyPressed;
            do
            {
                Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.LeftArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex += 3;
                    }
                }
                else if (keyPressed == ConsoleKey.RightArrow)
                {
                    SelectedIndex++;

                    if (SelectedIndex == 3)
                    {
                        SelectedIndex -= 3;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);


        }
        public static bool PlayerOrCmputer()// eto nag rereturn kung pinili mo ay comp or player
        {
            if (SelectedIndex == 1)
            {

                return true;
            }
            else
            {
                return false;
            }

        }
        
    }
}
