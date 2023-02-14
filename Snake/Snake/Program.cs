namespace Snake
{
    internal class Program
    {
        System.Timers.Timer timer = new System.Timers.Timer();

        bool quit = false;
        int vx;
        int vy;
        int headX;
        int headY;
        int[,] GameField;
        int w = 80, h = 40;
        int score = 0;
        int lifes = 3;
        int apples;
        int level = 0;

        void KeyBoardUpdate()
        {

            if (Console.KeyAvailable)
            {

                ConsoleKey key = Console.ReadKey().Key;
                System.Diagnostics.Debug.WriteLine(key);
                System.Diagnostics.Debug.WriteLine("X=" + headX + " Y=" + headY + " VX=" + vx + " VY=" + vy);
                Console.Title = DateTime.Now.ToLongTimeString();
                switch (key)
                {

                    case ConsoleKey.LeftArrow:
                        vx = -1;
                        vy = 0;
                        break;

                    case ConsoleKey.RightArrow:
                        vx = 1;
                        vy = 0;
                        break;

                    case ConsoleKey.UpArrow:
                        vx = 0;
                        vy = -1;
                        break;

                    case ConsoleKey.DownArrow:
                        vx = 0;
                        vy = 1;
                        break;

                    case ConsoleKey.Escape:
                        timer.Stop();
                        quit = true;
                        Console.WriteLine("SEE YOU SOON");
                        break;
                }

            }
        }
        void PrintGameField()
        {

            for (int y = 0; y <= h; y++)
                for (int x = 0; x <= w; x++)
                {
                    Console.SetCursorPosition(x, y + 1);

                    switch (GameField[x, y])
                    {
                        case 0:
                            Console.WriteLine(' ');
                            break;

                        case -1:
                            Console.WriteLine('b');
                            break;

                        case 1:
                            Console.WriteLine('o');
                            break;

                        default:    
                            Console.WriteLine('#');
                            break;
                    }
                }

            Console.SetCursorPosition(10, 0);
            Console.Write($"LVL:{level} SCORE:{score} LIFES:{lifes} APPLES:{apples}");
        }
        void Load(int level = 1)
        {
            vx = 0;
            vy = 1;
            headX = w / 2;
            headY = h / 2;
            GameField = new int[w + 1, h + 1];
            GameField[headX, headY] = 1;
            Random random = new Random();
            apples = level + 1;

            for (int i = 0; i < apples; i++)
                GameField[random.Next(1, w), random.Next(1, h)] = -1;


            for (int i = 0; i <= w; i++)
            {
                GameField[i, 0] = 10000;
                GameField[i, h] = 10000;
            }
            for (int i = 0; i < h; i++)
            {
                GameField[0, i] = 10000;
                GameField[w, i] = 10000;

            }
        }
        void Init()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(w + 1, h + 3);
            Console.SetBufferSize(w + 1, h + 3);

        }
        void SplashScreen()
        {
            string[] ss = new string[10];

            ss[0] = "  %%%   %    %   %%% %   %   %%%%%%";
            ss[1] = " %   %  %    %  %  % %  %    %     ";
            ss[2] = "  %     %%   % %   % % %     %     ";
            ss[3] = "   %    % %  % %%%%% %%      %%%%% ";
            ss[4] = "    %   %  % % %   % % %     %     ";
            ss[5] = "     %  %   %% %   % %  %    %     ";
            ss[6] = " %   %  %    % %   % %   %   %     ";
            ss[7] = "  %%%   %    % %   % %    %  %%%%%%";
            ss[8] = "                                   ";
            ss[9] = " - USE THE ARROW BUTTONS TO MOVE - ";

            for (int i = 0; i < ss.Length; i++)
                for (int j = 0; j < ss[i].Length; j++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.SetCursorPosition(j + 25, i + 10);
                    Console.Write(ss[i][j]);
                    System.Threading.Thread.Sleep(5);
                }
            Console.SetCursorPosition(30, 25);
            Console.Write("PRESS ANY KEY TO START");
            Console.ResetColor();
        }
        public void Game()
        {
            Init();
            SplashScreen();
            Load();
            PrintGameField();
            
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Game();
        }
    }
}