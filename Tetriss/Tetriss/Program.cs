using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Tetriss
{
    class Program
    {
        const int max_speed = 9;
        const double min_speed = 0.9;

        const string message1 = "  Приехали  ";
        const string message2 = " нажмите любую клавишу для выхода ";
        const string message3 = " Поздравляем ";

        const string message5 = " нажмите любую клавишу для начала игры ";
        const string message6 = " нажмите любую клавишу для продолжения ";

        static int Speed = 1; // скорость
        static int Score = 0; // счет
        static int Lines = 0; // линии

        static Point ptBlock = new Point();
        static windowRect wrBlockAdj = new windowRect();
        static windowRect PlayWindow = new windowRect();
        static Sound SoundEffect = new Sound();
        static bool isRows = false;
        static bool isGameEx = false;

        static Game.StructBlock nextBlock = new Game.StructBlock();
        static Game.StructBlock CurrentBlock = new Game.StructBlock();

        static ConsoleKeyInfo KeyB; //ввод с клавы
        static Game.TetrisClass Tetris;

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            Screen();
            GameDesign();
            Tetris = new Game.TetrisClass(PlayWindow);
            Tetris.ProcessEvent += new Game.TetrisisHandler(Tetris_Process);

            ShowStatus();
            ShowNextBlock();
            PlBlock(CurrentBlock, true);

            Stopwatch.StartNew();
            while(!isGameEx)
            {
                TimeSpan ts = stopwatch.Elapsed;
                Point nPossision = ptBlock;
                bool isCanRotate = false;

                //управление
                if(Console.KeyAvailable)
                {
                    KeyB = Console.ReadKey(true);
                    switch (KeyB.Key)
                    {
                        case ConsoleKey.Enter:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition((Console.WindowWidth - message6.Length) / 2, PlayWindow.height + PlayWindow.top + 2);
                            Console.Write(message6);
                            Console.ResetColor();
                            Console.ReadKey();
                            ClearBottomLine();
                            break;
                        case ConsoleKey.LeftArrow:

                            if (PlayWindow.left < nPossision.x)
                                nPossision.x--;
                            break;

                        case ConsoleKey.RightArrow:

                            if ((PlayWindow.width + PlayWindow.left) > (nPossision.x + wrBlockAdj.width))
                                nPossision.x++;
                            break;
                        case ConsoleKey.DownArrow:
                            if ((PlayWindow.height + PlayWindow.top) > (nPossision.y + wrBlockAdj.height))
                                nPossision.y++;
                            break;
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.Spacebar:
                            windowRect newBlockAdj = new windowRect();
                            
                            Game.RotationEnum saveAngel = Tetris.block.Angel;
                            newBlockAdj = Tetris.block.Rotate(Tetris.block.getNextAngle(0)); // вращение

                            
                            if (nPossision.x + newBlockAdj.width > PlayWindow.width + PlayWindow.left)       //проверка
                                nPossision.x = (PlayWindow.width + PlayWindow.left) - newBlockAdj.width;
                            if (nPossision.y + newBlockAdj.height > PlayWindow.height + PlayWindow.top)
                                nPossision.y = (PlayWindow.height + PlayWindow.top) - newBlockAdj.height;

                            if (Tetris.Collided(new Point(nPossision.x, nPossision.y), newBlockAdj))
                            {
                                newBlockAdj = Tetris.block.Rotate(Tetris.block.getNextAngle(1));

                                if (nPossision.x + newBlockAdj.width > PlayWindow.width + PlayWindow.left)
                                    nPossision.x = (PlayWindow.width + PlayWindow.left) - newBlockAdj.width;
                                if(nPossision.y + newBlockAdj.height > PlayWindow.height - PlayWindow.top)
                                    nPossision.y = (PlayWindow.height + PlayWindow.top) - newBlockAdj.height;
                                if (Tetris.Collided(new Point(nPossision.x, nPossision.y), newBlockAdj))
                                    isCanRotate = false;
                                
                            } 
                            else 
                                    isCanRotate = true;
                            if (isCanRotate)
                            {
                                ptBlock = nPossision;
                                wrBlockAdj = newBlockAdj;
                            }
                            else
                                Tetris.block.Rotate(saveAngel);
                            break;

                        case ConsoleKey.Escape:
                            isGameEx = true;
                            break;
                        
                    


                    }
                    if(!KeyB.Equals(ConsoleKey.Spacebar)) //если не нажата 
                        if(!Tetris.Collided(new Point(nPossision.x, nPossision.y), wrBlockAdj))
                            ptBlock = nPossision;
                    if (ts.TotalSeconds < (min_speed - (Speed - 1) / 10.0))
                        Tetris.block.Draw(ptBlock, wrBlockAdj, isCanRotate);
                }
                if(ts.TotalSeconds >=(min_speed - (Speed -1) / 10.0))
                     {
                    if ((PlayWindow.height + PlayWindow.top) > (ptBlock.y + wrBlockAdj.height))
                        if (Tetris.Collided(new Point(ptBlock.x, ptBlock.y + 1), wrBlockAdj))
                            PlBlock(nextBlock, false);
                        else
                            ptBlock.y++;
                    else
                        PlBlock(nextBlock, false);

                    Tetris.block.Draw(ptBlock, wrBlockAdj, isCanRotate);
                    stopwatch.Reset();
                    stopwatch.Start();
                     }
                        
            }
            stopwatch.Stop();

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition((Console.WindowWidth - message2.Length) / 2, Console.WindowHeight / 2);

            Console.Write(message2);
            Console.ReadKey();
            Console.ResetColor();
            SoundEffect.Halt();
        }

        private static void Tetris_Process(object o, Game.EventArgs e)
        {
            if (e.RowsCompleted > 0)
            {
                isRows = true;
                Score += e.RowsCompleted * (e.RowsCompleted > 1 ? 15 : 10);
                Lines += e.RowsCompleted;
                if ((Lines >= 11) && (Lines <= 20))
                {
                    Speed = 2;
                }
                else if ((Lines >= 21) && (Lines <= 30))
                {
                    Speed = 3;
                }
                else if ((Lines >= 31) && (Lines <= 40))
                {
                    Speed = 4;
                }
                else if ((Lines >= 41) && (Lines <= 50))
                {
                    Speed = 5;
                }
                else if ((Lines >= 51) && (Lines <= 60))
                {
                    Speed = 6;
                }
                else if ((Lines >= 61) && (Lines <= 70))
                {
                    Speed = 7;
                }
                else if ((Lines >= 71) && (Lines <= 80))
                {
                    Speed = 8;
                }
                else if ((Lines >= 81) && (Lines <= 90))
                {
                    Speed = 9;
                }
                ShowStatus();
            }

        }
        private static void ShowStatus()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 3, 2);
            Console.WriteLine(" очки ");
            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 3, 5);
            Console.WriteLine(" Линии ");
            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 3, 17);
            Console.WriteLine(" Скорость ");
            Console.ResetColor();

            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 2, 3);
            Console.WriteLine(string.Format("{0:D8}", Score));
            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 2, 6);
            Console.WriteLine(string.Format("{0:D2}", Lines));
            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 5, 18);
            Console.WriteLine(string.Format("{0:D2}", Speed));

            if (Lines >= 100)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition((Console.WindowWidth - message3.Length) / 2, Console.WindowHeight / 2);
                Console.Write(message3);
                Console.ReadKey();
                Console.ResetColor();

                //SoundEffect.Play(global: :Tetris.Properties.Resources.S104);
                isGameEx = true;

            }
        }
        private static void ShowNextBlock()
        {
            nextBlock = Tetris.block.Generate();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 2, 8);
            Console.Write("будущий");
            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 2, 9);
            Console.Write("▄▄▄▄▄▄▄▄");

            for (int i = 1; i < 6; i++)
            {
                Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 2, i + 9);
                Console.Write("▌      ▌");
            }
            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 2, 15);
            Console.Write("▀▀▀▀▀▀▀▀");
            Console.ResetColor();

            Tetris.block.Preview(new Point(PlayWindow.width + PlayWindow.left + 6, 12), nextBlock);

        }        
        private static void PlBlock(Game.StructBlock sbBlock, bool isNew)
        {
            if (isNew)
            {
                sbBlock = Tetris.block.Generate();
            }
            else
            {
                Tetris.SentToField(ptBlock, wrBlockAdj);
            }
            Tetris.block.Assign(sbBlock);
            Tetris.block.Build();
            Tetris.block.Adjustment(ref wrBlockAdj);

            ptBlock.x = (PlayWindow.left - wrBlockAdj.left) + (PlayWindow.width - wrBlockAdj.width) / 2; //расчет координат
            ptBlock.y = PlayWindow.top;

            Tetris.block.Draw(ptBlock, wrBlockAdj, true);
            ShowNextBlock();

            if (Tetris.Collided(ptBlock, wrBlockAdj))
            {
             //soundEffect.Play(global::Tetriss.Propert.Resource1.S102);
                Console.SetCursorPosition((Console.WindowWidth - message3.Length) / 2, Console.WindowHeight / 2);
                Console.Write(message3);
                isGameEx = true;
            }
          else
            {
              if (isRows)
                {
             //    SoundEffect.Play(global::Tetriss.Propert.Resource1.S103);
                isRows = false;
                }
          //    else
                {
            //     SoundEffect.Play(global::Tetriss.Propert.Resource1.S100);
                }
            }
        }

        private static void GameDesign()
        {
            const string designTB = "████████████████"; //верхняя граница экрана
            const string designLR = "▌               ▐"; //боковые
            PlayWindow.left = 33; //размер окна
            PlayWindow.width = designTB.Length - 2;
            PlayWindow.height = 19; //низ
            PlayWindow.top = 2; //верх

            Console.CursorVisible = false;

            Console.SetCursorPosition(1, 1);
            Console.Write("->, <-, Вниз => Перемещение");
            Console.SetCursorPosition(1, 2);
            Console.Write("Вверх, пробел =Ю вращение блока");
            Console.SetCursorPosition(1, 3);
            Console.Write("Ввод => пауза, продолжить");

            Console.SetCursorPosition(PlayWindow.left - 1, PlayWindow.top - 1);//верхняя граница
            Console.Write(designTB);
            Console.SetCursorPosition(PlayWindow.left - 1, PlayWindow.top + PlayWindow.height);//нижняя граница
            Console.Write(designTB);

            for(int i = PlayWindow.top; i < PlayWindow.height + PlayWindow.top; i++) //прорисовка границы
            {
                Console.SetCursorPosition(PlayWindow.left - 1, i);
                Console.Write(designLR);
            }

            //SoundEffect.Play(global::Tetriss.Propert.Resource1.S101);

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition((Console.WindowWidth - message5.Length) / 2, PlayWindow.height + PlayWindow.top + 2);

            Console.Write(message5);
            Console.ResetColor();
            Console.ReadKey();
            ClearBottomLine();
        }
        //очистка собранных линий
        private static void ClearBottomLine()
        {
           for(int i = 1; i < Console.WindowWidth; i++)
            {
                Console.SetCursorPosition(i, PlayWindow.height + PlayWindow.top + 2);
                Console.Write("  ");
            }
        }
        private static void Screen() //заставка и цвет
        {
            string[] str = new string[2];
            str[0] = "Т Е Т Р И С";
            str[1] = "           ";

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            for (int i = 0; i < str.Length; i++)
            { 
            for (int j = 0; j < str[i].Length; j++)
                {
                    Console.SetCursorPosition(j + 70, i + 5); //смещение
                    Console.Write(str[i][j]); // отображение
                    Console.Beep(2000, 1); 
                    System.Threading.Thread.Sleep(10);  //задержка
                }
            }
        }
    }
}
         
    
    
