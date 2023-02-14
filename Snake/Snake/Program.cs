namespace Snake
{
    internal class Program
    {
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
                    Console.SetCursorPosition(j + 25, i + 10);
                    Console.Write(ss[i][j]);
                    System.Threading.Thread.Sleep(5);
                }
            Console.SetCursorPosition(30, 25);
            Console.Write("Press any key to start");
            Console.ResetColor();
        }
        public void Game()
        {
            Init();
            SplashScreen();
            
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Game();
        }
    }
}