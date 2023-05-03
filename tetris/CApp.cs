using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    class CApp
    {
        public static readonly int MAX_FPS = 60;

        // вызывается каждую игровую итерация
        public delegate void Update(Double time);
        public static event Update mEventUpdate;

        // вызывается при нажатии на клавишу
        public delegate void KeyPressed(ConsoleKey key);
        public static event KeyPressed mEventKeyPressed;

        //  обеспечивает бесконечный игровой цикл 
        public static void Run()
        {

            CStopwatch timer = new CStopwatch();

            ConsoleKeyInfo keyPressed = new ConsoleKeyInfo();
            ulong frame = 0;

            CSetings.SetSetings();

            СTetrisApp tetris = СTetrisApp.GetInstance();

            do
            {
                if (Console.KeyAvailable)
                {
                    keyPressed = Console.ReadKey(true);

                    if (mEventKeyPressed != null)
                    {
                        mEventKeyPressed(keyPressed.Key);
                    }
                }

                if (mEventUpdate != null)
                {
                    mEventUpdate(timer.Now() / 1000);
                }
                ++frame;

                while ((frame / (timer.Now() / 1000) > MAX_FPS))
                {
                    System.Threading.Thread.Sleep(5);
                }

            } while (keyPressed.Key != ConsoleKey.X);

        }
    }
}
