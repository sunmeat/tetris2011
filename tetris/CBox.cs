using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    //класс умеет выводить на экран прямоугольник заданного размера в указанном месте поля.
    // для вывода используется класс Field
    class CBox
    {
        public CBox(int sizeX, int sizeY, ConsoleColor color, ref CField field)
        {
            if (sizeX <= 0) sizeX = 3;
            if (sizeY <= 0) sizeY = 3;

            mField = field;
            mColor = color;
            mSizeX = sizeX;
            mSizeY = sizeY;
        }

        public void print(int left, int top)
        {
            mField.Color = mColor;
            mField.SetPoint(left, top, TOPLEFT);
            mField.SetHorizonLine(left + 1, top, HORIZON_LINE, mSizeX - 2);
            mField.SetHorizonLine(left + 1, top + mSizeY, HORIZON_LINE, mSizeX - 2);

            mField.SetPoint(mSizeX - 1 + left, top, TOPRIGHT);
            mField.SetPoint(left, top + mSizeY, BOTTOMLEFT);
            mField.SetPoint(mSizeX + left - 1, top + mSizeY, BOTTOMRIGH);

            for (int y = top + 1; y < top + mSizeY; ++y)
            {
                mField.SetPoint(left, y, VERTICAL_LINE);
                mField.SetPoint(left + mSizeX - 1, y, VERTICAL_LINE);
            }

        }
        private readonly int mSizeX;
        private readonly int mSizeY;
        private readonly CField mField;
        private readonly ConsoleColor mColor;

        static readonly char TOPLEFT       = '╔';
        static readonly char TOPRIGHT      = '╗';
        static readonly char HORIZON_LINE  = '═';
        static readonly char VERTICAL_LINE = '║';
        static readonly char BOTTOMLEFT    = '╚';
        static readonly char BOTTOMRIGH    = '╝';
    }
}
