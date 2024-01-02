using System;
using static System.Console;
using System.Threading;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;


namespace QuizzN_Ladders
{
    class Game
    {
        static private World Myworld;
        static public int dice = 0;
        static public int Player1Place = 1;
        static public int Player2Place = 1;
        static public bool IsPvc = true;
        static public bool IsPlayer1Turn = false;
        static public int add = 0;


        static public void Run()//responsible for running the game
        {
            string[,] grid = LevelParser.ParseFileToArray("Map1.txt");
            Myworld = new World(grid);
            ReadKey();
            MainMenu.Run();
            IsPvc = MainMenu.PlayerOrCmputer();
            AskUsername();
            GameLoop();


        }

        public static void Render()// draws every thing cinall ko lang to para mas madali
        {
            Clear();
            Myworld.Draw();
            Myworld.DrawPlayer();
        }
        public static void PlayerInput()// eto yung player input
        {
            SetCursorPosition(101, World.y + 1);

            WriteLine("(Enter) Roll Die | (n) Exit");

            if (IsPlayer1Turn && IsPvc)// bago sya mag pa input chinecheck muna kung TRUE ba yung Pvc at kung turn na ni p2 pag both true comp ang mag roroll
            {
                DiceRoll();
                IsPlayer1Turn = !IsPlayer1Turn;//same lang din dito sa baba pero for pvc

            }
            else// dito naman yung kapag di om pvc
            {

                ConsoleKeyInfo keyInfo = ReadKey(true);

                ConsoleKey key = keyInfo.Key;

                if (ConsoleKey.Enter == key)
                {
                    DiceRoll();
                    IsPlayer1Turn = !IsPlayer1Turn;// eto pagka roll ng dice mag papalit sya ng bool para magpalit ng turn

                }

                else if (ConsoleKey.N == key)
                {
                    WriteLine("See you next time");
                    ReadKey();
                    Environment.Exit(0);
                }

            }

        }
        static public void DiceRoll()
        {

            Random random = new Random();
            dice = random.Next(1, 7);

            if (IsPlayer1Turn)// dito na nag chcheck kung kaninonf turn tapos kung sino mano dun nya iaad yung dice 
            {

                Player2Place += dice;
            }


            else
            {
                Player1Place += dice;
            }


            SetCursorPosition(101, World.y + 2);
            Console.WriteLine("  Dice rolling ...");
            Thread.Sleep(1000);

            SetCursorPosition(101, World.y + 3);
            Console.WriteLine($"  Dice Rolled: +{dice}");
            Thread.Sleep(1000);


        }
        public static void CurrentStanding()//this is for the players info
        {

            string pPos = ($"{name2}'s Position: {Player2Place}");
            SetCursorPosition(101, World.y - 1);
            Write(pPos);

            string pPos2 = ($"{name1}'s Position: {Player1Place}");
            SetCursorPosition(101, World.y - 3);
            Write(pPos2);
        }

        public static void RandomQuestions()
        {
            string[] ladderQuestions =
{
            "What is the capital of Australia?",
            "Which planet is known as the \"Red planet\"?",
            "In what year did the Titanice sink?",
            "Who wrote the play \"Romeo and Juliet\"?",
            "Largest ocean on Earth?",
            "Which galaxy is the Milky Way destined to collide with in the distant future?",
            "What is the phenomenon where a celestial body, such as a star, collapses under its own gravity and becomes extremely dense?",
            "Which dwarf planet was formerly considered the ninth planet in our solar system before being reclassified in 2006?",
            "Who was the first President of the United States?",
            "Which ancient civilization built the pyramids of Giza?",
            "Which emperor is known for building the Great Wall of China?",
            "What is the best-selling video game of all time?",
            "What popular online multiplayer game features a battle royale mode where 100 players compete to be the last person or team standing?",
            "In the game League of Legends, what do players control in the virtual arena/map?",
            "What is the name of the block-building game where players can create and explore their own virtual worlds?",
            "Which company developed the first commercially successful personal computer with the release of the Apple II in 1977?",
            "What technology, using radio waves, allows devices to communicate and exchange data wirelessly over short distances?",
            "What is the programming language created by James Gosling and his team at Sun Microsystems, which is known for its platform independence?",
            "Which industry involves the cultivation of crops and the raising of livestock for food, fiber, and other products?",
            "What term is used to describe the process of converting raw materials into finished goods on a large scale?",
            "What is the term for the economic system in which individuals or corporations own and operate most businesses with minimal government interference?",
            "What is the term for the fear of the dark?",
            "What mythical creature is said to be a reanimated corpse, often depicted as feeding on the blood of the living?",
            "What is the name of the infamous doll that is said to be haunted and has inspired horror movies?"
            };

            string[] ladderAnswers =
            {
            "Canberra",
            "Mars",
            "1912",
            "William Shakespeare",
            "Pacific",
            "Andromeda",
            "Black hole",
            "Pluto",
            "George Washington",
            "Egyptians",
            "Qin Shi Huang",
            "Minecraft",
            "PUBG",
            "Champions",
            "Roblox",
            "Apple",
            "Bluetooth",
            "Java",
            "Agriculture",
            "Manufacturing",
            "Capitalism",
            "Nyctophobia",
            "Vampire",
            "Annabelle"
            };

            Random ladderRandom = new Random();
            int index = ladderRandom.Next(ladderQuestions.Length);
            Clear();

            LadderWarning();

            SetCursorPosition((WindowWidth - 30) / 2, CursorTop + 2);
            WriteLine("Give the correct answer to advance. Retain blockplace if wrong.");

            SetCursorPosition((WindowWidth - 30) / 2, 4);
            WriteLine("Question: " + ladderQuestions[index]);

            SetCursorPosition((WindowWidth - 30) / 2, 6);
            Write("Your answer is: ");
            string userAnswer = ReadLine();

            if (userAnswer.ToLower() == ladderAnswers[index].ToLower())
            {
                SetCursorPosition((WindowWidth - 30) / 2, 8);
                WriteLine($"Correct! Position: +{add}");
                if (IsPlayer1Turn) Player2Place += add;
                else Player1Place += add;
                Thread.Sleep(100);
                Render();
            }

            else
            {
                WriteLine("\nIncorrect! Position: +0");
                Player1Place += 0;
            }

        }


        static public void PosCheck(int place)// dito mo lagay mga sankes quizzes etc
        {


            switch (Player1Place)
            {
                //snake
                case 10:
                    Player1Place -= 9;SnakeWarning();Render();
                    break;
                case 16:
                    Player1Place -= 10;SnakeWarning(); Render();
                    break;
                case 21:
                    Player1Place -= 6; SnakeWarning(); Render();
                    break;

                //ladder
                case 7:
                    add = 7;
                    RandomQuestions();
                    break;
                case 12:
                    add = 11;
                    RandomQuestions();
                    break;



            }


        }

        public static string name, name1, name2;
        public static void AskUsername()
        {
            if (IsPvc)
            {
                SetCursorPosition((WindowWidth - 30) / 2, CursorTop + 2);
                Write("Enter Username: ");
                name = ReadLine();
            }

            else
            {
                SetCursorPosition((WindowWidth - 30) / 2, CursorTop + 2);
                Write("Enter Player1 Username: ");
                name1 = ReadLine();

                SetCursorPosition((WindowWidth - 30) / 2, 4);
                Write("Enter Player2 Username: ");
                name2 = ReadLine();
            }
        }

        static public void GameLoop()// the loop that keeps the game going
        {



            do
            {

                //Draw player position
                Render();

                // check player position
                if (IsPlayer1Turn)
                {
                    PosCheck(Player1Place);
                }else
                {
                    PosCheck(Player2Place);
                }

                //Write player stats
                CurrentStanding();

                // ask player for input 
                PlayerInput();


            } while (Player1Place <= 25 && Player2Place <= 25);
            // tas palafgay dito yung winner

            Evaluation();




        }

        public static void Evaluation()
        {
            if (Player1Place >= 25 || Player2Place >= 25 )
            {
                Clear();
                FadeIn();
                Thread.Sleep(50);
                FadeOut();
                WriteLine(@"
loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooool
lXXKXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXKK0;l
lK0OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO00;l
lX0OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO00:l
lX0OOOOOOOOOOOOOOOOOOOOO00OOOOOOO00OOOOOOOOO00OOOOOOO0OOOOOOO00OOOOOOOOO0000OOOOOO0OO000OOOOOO00O000000OOOOO0O00O000000O0OOOOO000OOO0OO00O0OOOOOOOOOOOOOOOOOO00;l
lX0OOOOOOOOOOOOOOOOOOOOxc::okOOOo::cdOO0xc:::;lkOOOo::cxOOOxc::okOOOO0Oo::::lxkO0xc::::::dO0kl:::::::::dOd:::::::::::lo::cdOOkl::ll::::::::::xOOOOOOOOOOOOOOO00;l
lX0OOOOOOOOOOOOOOOOOOO0l   ,kOOk,  .lxll;......:ldk;  .lOO0o.  ,kOOOdll'.....;cloc.  ....,lll.   ......lOl....    ...;,  .lOOd.  .'   ......'oOOOOOOOOOOOOOOO00;l
lX0OOOOOOOOOOOOOOOOOOOOl   ,kOOk,  .lc   ;xxkd.  ,x;  .lOO0o.  ,kOOk;  .cxkd'   .'. .okx:  .'.  'dkkxxkkOkxxko.  'dxxk;  .lOOd.  .'  .ckxxxxkkOOOOOOOOOOOOOOO00;l
lX0OOOOOOOOOOOOOOOOOOOOl...'lool'..'oc   :kkOx'  ,x:  .lOO0o.  ,kOOO;  .;ooc.   .,. .cll,  .,.  .cllldkOOOOOOx.  'xOOO:   ;ooc.  ''  .;llloxOOOOOOOOOOOOOOOOO00;l
lX0OOOOOOOOOOOOOOOOOOOOkxko.    'dxxO:   :OO0x'  ,k;  .lOO0o.  ,kOOk;           .,.      .;cc.      .;kOOOOO0x.  'xOOO:          ''       .oOOOOOOOOOOOOOOOOO00:l
lX0OOOOOOOOOOOOOOOOOOOOOOOkc.  .lkOOO:   :kkOx'  ,k;  .lOO0o.  ,kOOk;   ';:,.   .,. .,.  ;xko.  .cllldOOOOOO0d.  'xOOO:   ';:,.  ''  .;llloxOOOOOOOOOOOOOOOOO00:l
lX0OOOOOOOOOOOOOOOOOOOOOOO0d.  'xOOOOc...;xxkd. .;k:...cxkkl. .;kOOO;  .lO0x'   .,. 'xd,. ..,.  'dxxxxxkOkOO0d.  'xOOO:  .lO0d.  ''  .lxxxxxxkOOOOOOOOOOOOOOO00:l
lX0OOOOOOOOOOOOOOOOOOOOOOO0d.  'xOOOOxdd:......ldxkxdd;.....,odxOOOk;  .lO0x'   .,. .dOkc. .'.   ......ckOOOOd.  'xOOO:  .lOOd.  ''   .......oOOOOOOOOOOOOOOO00:l
lX0OOOOOOOOOOOOOOOOOOOOOOOOklccokOOOOOOOxcccccokOOOOOOdcccccdOOOOOOOdcclxOOkoccclolcokOOxlccolcccccccccdOOOOOkoccokOOOdcclxOOklccloccccccccclxOOOOOOOOOOOOOOO00:l
lX0OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO00:l
lX0OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO00:l
lX0OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOkOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOkOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO00:l
lX0OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOkkkkOOOOkkkkOOkkxkkOOOOkkkkkxxkOOOkkxkkkkkkkxkxxOkkkxkOOOOkxxkxxxkOOOkxkkkkxxkOOOOkkkOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO00;l
lX0OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOk;  'oOOk;  'dx,  'oOOOOl      ,dOOOl            dOOOx       dOOO:         kOOd,.,xOkOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO00;l
lX0OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO0k'  .lO0x'  .dd.  .o0l'          ;dd:;:c;.  .;:;cdk:            c;          ooo. .dOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO00;l
lX0OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO0k'  .lOOx'  .dd.  .o0;  .lOOOOc  ,dOOO0Kx.  'xOO0kx   .o0O0o.   d;  .d00x.  :lo. .dOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO00;l
lX0OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO0k'  .lO0k'  .dd.  .oO;  .lOOOOOkkkOOOOOKx.  'xOOO0x   .oOOOo.   d;         cloo. .dOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO00;l
lX0OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO0k:  .;ooc.  ,xd.  .oO;  .lOOOOxolokOOO0Kx.  'xOOOkx   .oOkOo.   x;   .   .oxOoo. .d0OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO00;l
lX0OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOkkk;    'dkkOd.  .oO;  .lOO0O;  .oOOO0Kx.  'xOOOkx   .dOO0o.   x;  .l:. .coxod:,ckOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO00;l
lX0OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO0x.  .lOOO0d.  .oOl           ;:dOO0Kx.  'xOO0kkc           lx;  .dOo,.  :xlclkOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO00;l
lX0kOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOk:  ,dOOOOd,  'dOOOOc.....,dOOOOOO00x,  :kOOO0OO0k;.....;xOOO:..,dO0x,   ,dO  OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO00;l
lX0OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOkxxkkOOOOOkxxkkOOOOkxxkkxkOOOOOOOOOOkxxkOOOOOkxxxOOOOOkxxxkxkOOOOkxxkOOOOkxxkOkxkOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO00;l
lX0OOOOO0000000000000000OOOOOOOOOOOOOO000OOOOOOOOO0OO0OOOOOOOOOOOOOOOOOOOOO0OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO0OOOOOOOOOOOOOOOOO00000OOOOOOO00;l
loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooool");

                Thread.Sleep(2000);
                Write("Press any key to exit");
                ReadKey();
            }

            if (Player2Place >= 25 && IsPvc)
            {
                Clear();
                FadeIn();
                Thread.Sleep(50);
                FadeOut();
                WriteLine(@"
0MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMWMMWWWWWWWWWWMWWWWWWWMMMMMMWWWWWWMMWWMWWWWWWWMWWWWMMMWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMWWWWNXKKKKKKXWWWNKKK0KXWWWWWXKKKKNWWWMWXKKKKNNKKKKKKXXNWWXKKKNMWXKKKXXKKKKKKKKKKKXKKKKKKKKKKNXKKKKKKKXWWWWWWWWWWWWWWWWWWWWWWN:
0MMMMMMMMMMMMMMMMMMWW0l;......'xNOl'......;xXWO,....lNWWWk,....dk,......';dOc..,OMXc..'oc..........,:..........dd.......'cONWWWWWWWWWWWWWWWWWWWN:
0MMMMMMMMMMMMMMMMMMWx.       .oXx.          :Xk.    .dWW0,     ox.        .;'  .kMK;  .l;          .,         .ol         .oNWWWWWWWWWWWWWWWWWWN:
0MMMMMMMMMMMMMMMMMMK,   .:dddkXK;   .cdo,   .xx.     .OXc      ox.  'xc.   ..  .kMK;   lkodl.   :doxc   .ldodod0l   ,dd'   ,0MWWWWWWWWWWWWWWWWWN:
0MMMMMMMMMMMMMMMMWM0'   ;XWWMWWK,   :XWWx.  .dx.      ,c.      ox.  cXx.   ..  .kMK;   dMWW0'  .xWWWl   .:::::lOl   cXNl   ,KMWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMWM0'   ;XWWWWMK,   ;XWWx.  .dx.               ox.  .'.   .:'  .kMK;   dMWW0'  .xWWWo         .dl   .:,.  .xWWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMWM0'   :XWWWWWK,   ;XMWx.  .dx.  .:'    .:.   ox.  ..,;;cO0,  .kWK;   dMWW0'  .xWWWo   .:ccccl0o    .    oWWWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMWMK,   .:llldKX;   .:oo,   .xx.  .dx.   lk'   ox.  ;KNWWWWK,   'l:.   dMWW0'  .xMWWo   .cooolo0l   :x,   ,0WWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMWx.        :Kx.          :Kk.   dWd. :KO'   ox.  :XWWWWWNl         ,0WWW0'  .xWWWo          ol   cXO'   cXWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMW0o,......'dXOl,.....':xXWk,...xWNo:KW0;...dk'..lNMWWWWWNx:'...';oKWWWW0;..'kWWWd..........do...lNWx'..,OWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMWNXKKOkkkkOKXWWWX0kkOO0KXXXXKKKKNNXKKXXN0kOO00OO0XNNXXXXXXXKKXKKXNWWWWNXKOkkOKXXWNKKKKK0OOkO00Okk0XXKOkkOXWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMNo........cXWWMWx'.........lXWWMWx'...l0c..'...'xWK:.''.....:0WWWWWWMWd........:KMWWNOo:,...............cKWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMWx.       .kWWWX:          .OWWMX;    cO'       lN0'         'kWWWMWWWl        '0WW0:.                  ,KMWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMK,        lNMWk.           lWWWk.    cO,       lN0'          .dNWMWMWl        '0M0,                    ,KMWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMWl        ,KMWl            ,KMWo     cO,       lW0'           .lXMWWWl        '0Wo                     ,KMWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMMO.       .xMK,            .xMK,     :O;      .dW0'            .:KWWWl        '0Wl                     ,KMWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMWMX:        :Nx.             :Nx.     ;KO;.....cXW0'              ,0WWl        '0MO'                    ;KMWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMMMx.       .xc              .xc      .kK;     cNM0'               'kNl        '0WWk'          .,,,,,,,,lXMWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMMMK,        '.               ,.      ,KK;     cNM0'                .dl        '0MWWKl.        .c0NNNNNNWWWWWWWWWWWWWWWWWWWWNc
0MWMMMMMMMMMMMMMMMMMMMWO.                               .kMX;     lNM0'                           '0WWMWWWNO:.        'xXWWWWWWWWWWWWWWWWWWWWWWNc
0MWMMMMMMMMMMMMMMMMMWWMX:                               ,KMX;     cNM0'          ,'               '0MWMWWWWWNk;.        ,kNWWWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMMWWWd.                              lNMX;     cNM0'          o0,              '0Wkcccccccc:.         .cKWWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMMMWM0'                             .kWWX;     lNM0'          lWK:             '0Wl                     ;KMWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMMMMMNl             .,.             ,KWWX;     lNM0'          oWMXl.           '0Wl                      dWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMMMMMMk.            ,k:             cNWMX;     lNM0'          oWMMNx.          '0Wl                      oWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMMMMMMX:            cNd.           .xWWMX;     lNM0,          oWMMWWk'         '0Wl                     .kMWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMMMMMWW0'          ;XWWl           lNWWWK;     lNM0'          oWWWWWWWXc.      '0Nl                 .'l0NWWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMMMMMWWNkodddoodood0WWWKdooooddddod0WWWWNkdoood0WWXkoooddooooo0WWWWMWWMXxoooodokNW0doodooooooooooddk0XWWWWWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWMWWWWMMMMMMMMWMWWWWWWWMWWMMMMWWWWWMWWMWWMMWWWWWWWWWWWWWWWWWWWWWWWWMMWWWWWWWWWWWWMWWWWWWWWWWWWWWWWWWWWWWWWNc
0MWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWNc
0MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWNc
0MWMWWWWWWWMMMMMMMMMWWWMMMMMWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWNc");

                Thread.Sleep(2000);
                Write("Press any key to exit");
                ReadKey();
            }

        }

        public static void SnakeWarning()
        {


            ForegroundColor = ConsoleColor.Red;
            WriteLine("SNAKE BLOCK!");
            ResetColor();
            Thread.Sleep(1000);
            Clear();
        }

        public static void LadderWarning()
        {
            Clear();
            string message = @" 
 __   __   __          ___       _______   _______   _______ .______         .______    __        ______     ______  __  ___  __   __  
|  | |  | |  |        /   \     |       \ |       \ |   ____||   _  \        |   _  \  |  |      /  __  \   /      ||  |/  / |  | |  | 
|  | |  | |  |       /  ^  \    |  .--.  ||  .--.  ||  |__   |  |_)  |       |  |_)  | |  |     |  |  |  | |  ,----'|  '  /  |  | |  | 
|  | |  | |  |      /  /_\  \   |  |  |  ||  |  |  ||   __|  |      /        |   _  <  |  |     |  |  |  | |  |     |    <   |  | |  | 
|__| |__| |  `----./  _____  \  |  '--'  ||  '--'  ||  |____ |  |\  \----.   |  |_)  | |  `----.|  `--'  | |  `----.|  .  \  |__| |__| 
(__) (__) |_______/__/     \__\ |_______/ |_______/ |_______|| _| `._____|   |______/  |_______| \______/   \______||__|\__\ (__) (__) 
                                                                                                                                       ";

            ForegroundColor = ConsoleColor.Green;
            WriteLine("\n\n\n\n\n\n\n\n\n\n\n" + message);
            ResetColor();
            Thread.Sleep(1000);
            Clear();
        }
        public static void FadeIn()
        {
            for (int i = 0; i < 16; i++)
            {
                BackgroundColor = (ConsoleColor)i;
                Clear();
                Thread.Sleep(50); // Wait for 100 milliseconds
            }
        }

        public static void FadeOut()
        {
            for (int i = 15; i >= 0; i--)
            {
                BackgroundColor = (ConsoleColor)i;
                Clear();
                Thread.Sleep(50); // Wait for 100 milliseconds
            }
        }
    }



}




