using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    struct SPoint
    {
        public SPoint(int x, int y, char symbol ,ConsoleColor color)
        {
            mX = x;
            mY = y;
            mSymbol = symbol;
            mColor = color;
        }

        public int X
        {
            get { return mX; }
            set { mX = value; }

        }
        public int Y
        {
            get { return mY; }
            set { mY = value; }

        }
        public ConsoleColor Color
        {
            set { mColor = value; }
            get { return mColor; }
        }
        public char Symbol
        {
            get { return mSymbol; }

        }

        private int mX;
        private int mY;
        char mSymbol;
        private ConsoleColor mColor;
    }
}
