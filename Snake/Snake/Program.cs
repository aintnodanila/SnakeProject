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
        int lifes = 5;
        int mushrooms;
        int garden = 0;

        bool Collision()
        {
            if (GameField[headX, headY] > 0) quit = true;
            if (headX < 1 || headX >= w || headY < 1 || headY >= h) quit = true;
            return quit;
        }

        void Update()
        {


            headX += vx;
            headY += vy;
            if (Collision()) return;

            if (GameField[headX, headY] < 0)
            {
                score++;
                mushrooms--;
                if (mushrooms == 0)
                {
                    quit = true;
                    return;
                }
                GameField[headX, headY] = 1;
                Next(headX - vx, headY - vy, 1, 1);
            }
            else
                Next(headX, headY, 1);

        }

        void Next(int tailX, int tailY, int n, int p = 0)
        {

            GameField[tailX, tailY] = n + p;

            if (GameField[tailX + 1, tailY] == n + p) Next(tailX + 1, tailY, n + 1, p);
            else
                if (GameField[tailX - 1, tailY] == n + p) Next(tailX - 1, tailY, n + 1, p);
            else
                if (GameField[tailX, tailY - 1] == n + p) Next(tailX, tailY - 1, n + 1, p);
            else
                if (GameField[tailX, tailY + 1] == n + p) Next(tailX, tailY + 1, n + 1, p);
            else
                if (p == 0) GameField[tailX, tailY] = 0;

        }

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
                        Console.WriteLine("SEE YOU SOON!");
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
                            Console.WriteLine('@');
                            break;

                        case 1:
                            Console.WriteLine('o');
                            break;

                        default:
                            Console.WriteLine('█');
                            break;
                    }
                }

            Console.SetCursorPosition(10, 0);
            Console.Write($"GARDEN: {garden} SCORE: {score} LIFES: {lifes} MUSHROOMS: {mushrooms}");
        }
        void Load(int garden = 1)
        {
            vx = 0;
            vy = 1;
            headX = w / 2;
            headY = h / 2;
            GameField = new int[w + 1, h + 1];
            GameField[headX, headY] = 1;
            Random random = new Random();
            mushrooms = 25;

            for (int i = 0; i < mushrooms; i++)
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
            
            // Works only on Windows
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
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.SetCursorPosition(j + 25, i + 10);
                    Console.Write(ss[i][j]);
               
                    System.Threading.Thread.Sleep(10);
                }
            Console.SetCursorPosition(30, 25);
            Console.Write("PRESS ANY KEY TO START");
            Console.ResetColor();
        }
        public void Game()
        {
            Init();
            SplashScreen();
            Console.ReadKey();
            while (lifes > 0)
            {
                Load(++garden);
                PrintGameField();
                Console.ReadKey();
                while (!quit)
                {
                    KeyBoardUpdate();
                    Update();
                    PrintGameField();
                    System.Threading.Thread.Sleep(5);
                }
                lifes--;
                quit = false;
            }

        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Game();
            Console.ReadKey();
        }
    }
}