using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    //  инкапсулируем вывод на экран
    class CView
    {
        public CView(IFieldView field)
        {
            mField = field;
        }

        private IFieldView mField;
        private static ulong mFrame = 0;
        private PrintLine mPrintLine;

        // структура хранит начальную и конечную строку для вывода на экран
        private struct PrintLine
        {
            public void SetPrintLine(int line, int height)
            {
                mStart = line;
                mEnd = line + height;
            }
            public int mStart;
            public int mEnd;
        }

        public void SetPrintLine(int line, int height)
        {
            mPrintLine.SetPrintLine(line, height);
        }

        public void Print(double time = 0.0)
        {
            int fps = (int)(++mFrame / (time));
            if (fps > 0)
                System.Console.Title = "Tetris FPS " + fps.ToString() + " time  " + ((int)time).ToString();

            //if (fps >= CApp.MAX_FPS || time == 0)
            {
                for (int y = mPrintLine.mStart; y < mPrintLine.mEnd; ++y)
                {
                    for (int x = 0; x < СTetrisApp.FIELD_GAME_WIDTH - 1; ++x)
                    {
                        SPoint curPoint = mField.GetPoint(x, y);
                        {
                            Console.SetCursorPosition(x, y);

                            System.Console.ForegroundColor = curPoint.Color;
                            System.Console.Write(curPoint.Symbol);
                        }
                    }
                }

                // Выводим окно следующей фигуры если текущая только появилась
                if (mPrintLine.mStart < 1)
                    for (int y = 0; y < СTetrisApp.NEXT_SHAPE_WINDOW_HEIGHT +1; ++y)
                    {
                        for (int x = СTetrisApp.FIELD_WIDTH - СTetrisApp.NEXT_SHAPE_WINDOW_WIDTH;
                            x < СTetrisApp.FIELD_WIDTH; ++x)
                        {
                            SPoint curPoint = mField.GetPoint(x, y);
                            {
                                Console.SetCursorPosition(x, y);

                                System.Console.ForegroundColor = curPoint.Color;
                                System.Console.Write(curPoint.Symbol);
                            }
                        }
                    }
            }
        }

        public void Foreground(ConsoleColor color )
        {
            System.Console.ForegroundColor = color;
        }
    }
}
