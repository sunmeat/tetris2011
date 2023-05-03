using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    // инкапсулируем модель поля
    class CField : IFieldView
    {
        public static CField GetInstance(int SizeX, int SizeY)
        {
            if (instance == null)
            {
                instance = new CField(SizeX, SizeY);
            }
            return instance;
        }

        static CField instance = null;

        private CField(int SizeX, int SizeY)
        {
            mSizeX = SizeX;
            mSizeY = SizeY;
            mField = new char[SizeX * SizeY];
            mColor = new ConsoleColor[SizeX * SizeY];
            System.Console.BufferWidth = Console.WindowWidth = SizeX;
            Console.WindowHeight = SizeY + 1;

            System.Console.BufferHeight = Console.WindowTop + Console.WindowHeight;
            mView = new CView(this);

            ClearGameField();

            // подписываем Field.Print() на событие Update для обновления экрана на каждой игровой итерации
            CApp.mEventUpdate += Print;
        }

        public void ClearGameField()
        {
            for (int y = 1; y < mSizeY - 1; ++y)
                ResetLine(y);
        }

        public void ResetLine(int line)
        {
            for (int x = 1; x < СTetrisApp.FIELD_GAME_WIDTH - 2; ++x)
            {
                mField[line * mSizeX + x] = AShape.EMPTYSYMBOL;
                mColor[line * mSizeX + x] = ConsoleColor.Black;
            }
        }

        public  SPoint GetPoint(int absCoord)
        {
            return GetPoint(CoordX(absCoord), CoordY(absCoord));
        }

        public SPoint GetPoint(int x, int y)
        {

            return new SPoint(x, y,
                mField[AbsoluteCoord(x, y)],
                mColor[AbsoluteCoord(x, y)]);
        }

        public int Count { get { return mField.Count(); } }

        public void Print(double v = 0.0) { mView.Print(v); }

        public void SetPoint(int x, int y, char val)
        {
            if (AbsoluteCoord(x, y) < mSizeX * mSizeY)
            {
                int v = AbsoluteCoord(x, y);
                mField[AbsoluteCoord(x, y)] = val;
                mColor[AbsoluteCoord(x, y)] = mCurentColor;
            }
            else
                throw new ArgumentException("ArgumentException in SetPoint");
        }

        public void PrintString(int x, int y, string val)
        {
            if (AbsoluteCoord(x + val.Count(), y) < mSizeX * mSizeY)
            {
                for (int i = 0; i < val.Count(); ++i)
                {
                    mField[AbsoluteCoord(x + i, y)] = val[i];
                    mColor[AbsoluteCoord(x + i, y)] = mCurentColor;
                }
            }
        }

        public void SetHorizonLine(int x, int y, char val, int lench)
        {
            if (AbsoluteCoord(x + lench, y) < mSizeX * mSizeY)
            {
                for (int i = 0; i < lench; ++i)
                {
                    mField[AbsoluteCoord(x + i, y)] = val;
                    mColor[AbsoluteCoord(x + i, y)] = mCurentColor;
                }
            }
            else
                throw new ArgumentException("ArgumentException in SetHorizonLine");

        }

        public ConsoleColor Color
        {
            set { mCurentColor = value; }
            get { return mCurentColor; }
        }

        private int AbsoluteCoord(int x, int y)
        {
            return (y * mSizeX + x);
        }

        public static int CoordX(int absCoord)
        {
            return absCoord % mSizeX;
        }

        public static int CoordY(int absCoord)
        {
            return absCoord / mSizeX;
        }

        public int SizeX
        {
            get { return mSizeX; }
        }

        public int SizeY
        {
            get { return mSizeY; }
        }

        public void SetPrintLine(int line, int height)
        {
            mView.SetPrintLine(line, height);
        }

        bool CheckLine(int y)
        {
            bool result = true;
            for (int x = 1; result && x < СTetrisApp.FIELD_GAME_WIDTH - 1; ++x)
            {
                result = GetPoint(x, y).Symbol != AShape.EMPTYSYMBOL;
            }
            return result;
        }

        public void CheckField()
        {
            for (int y = 1; y < СTetrisApp.FIELD_HEIGHT - 1; ++y)
            {
                if (CheckLine(y))
                    RemoveLine(y);
            }
        }

        void RemoveLine(int ny)
        {
            for (int y = ny; y > 1; y--)
                for (int x = 1; x < СTetrisApp.FIELD_GAME_WIDTH - 1; ++x)
                {
                    Color = GetPoint(x, y - 1).Color;
                    SetPoint(x, y, GetPoint(x, y - 1).Symbol);
                }
            ResetLine(1);
            SetPrintLine(1, ny);
        }

        private static int mSizeX;
        private static int mSizeY;
        private char[] mField;
        private ConsoleColor[] mColor;
        private CView mView;
        private ConsoleColor mCurentColor = ConsoleColor.White;

    }
}
