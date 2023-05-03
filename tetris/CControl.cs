using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    class CControl
    {
        AShape ShapeUnderControl
        {
            get { return mTetris.GetShapeUnderControl(); }
        }
        ITetrisControl mTetris;
        static double timeFromLastIteration = 0.0;


        public void Update(double time)
        {
            double speed = 0;

            if (time > 90)
                speed = .1;
            else
                if (time > 60)
                    speed = .1;
                else
                    if (time > 15)
                        speed = .05;

            if (time - timeFromLastIteration > .4 - speed)
            {
                if (Console.KeyAvailable)
                {
                    KeyPressed(Console.ReadKey(true).Key);
                }
                KeyPressed(ConsoleKey.DownArrow);
                timeFromLastIteration = time;
            }

        }

        public CControl(ITetrisControl nTetris)
        {
            mTetris = nTetris;

            CApp.mEventKeyPressed += KeyPressed;
            CApp.mEventUpdate += Update;
        }

        public void KeyPressed(ConsoleKey key)
        {
            try
            {
                if (key == ConsoleKey.LeftArrow)
                {
                    ShapeUnderControl.MoveLeft();
                }
                else
                    if (key == ConsoleKey.RightArrow)
                    {
                        ShapeUnderControl.MoveRight();
                    }
                    else
                        if (key == ConsoleKey.Spacebar)
                        {
                            ShapeUnderControl.Rotate();
                        }
                        else
                            if (key == ConsoleKey.DownArrow)
                            {
                                ShapeUnderControl.MoveDown();
                            }
                            else
                                if (key == ConsoleKey.R)
                                {
                                    mTetris.ClearGameField();
                                }
            }

            catch (CExBottomReached)
            {
                mTetris.NextShape();
                if (!ShapeUnderControl.CheckDownMoove())
                {
                    mTetris.GameOver();
                }
            }
            finally
            {
                Flush();
            }
        }

        // очищаем буфер клавиатуры
        void Flush()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }
    }
}

