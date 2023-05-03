using System;
namespace tetris
{
    abstract class AShape : IShape, IDisposable
    {
        protected static int MAX_SHAPE_SIZE = 4;
        protected char[,] mShape = new char[MAX_SHAPE_SIZE, MAX_SHAPE_SIZE];
        protected int mShapeSize;
        protected int mHeight;
        protected int mWidth;
        static protected CField mField;
        static readonly int START_Y = 1;
        static readonly int START_X = СTetrisApp.FIELD_GAME_WIDTH / 2;
        static Random mRandom = new Random();
        public const char EMPTYSYMBOL = ' ';
        public const char SHAPESYMBOL = '█';
        private bool disposed = false;

        public static readonly ConsoleColor[] colorArray = {
                                      ConsoleColor.Green,
                                      ConsoleColor.Magenta,
                                      ConsoleColor.DarkRed,
                                      ConsoleColor.DarkGreen,
                                      ConsoleColor.White,
                                      ConsoleColor.DarkYellow,
                                      ConsoleColor.Blue,
                                      ConsoleColor.Cyan
                                                };

        // координаты  левого верхнего угла, текущего положения фигуры 
        protected SPoint mTopLeft = new SPoint(START_X, START_Y, EMPTYSYMBOL, System.ConsoleColor.Green);

        public virtual void Rotate()
        {
            if (!CheckRotate())
                return;
            Erase();
            char[,] Shape = new char[MAX_SHAPE_SIZE, MAX_SHAPE_SIZE];

            for (int y = 0; y < mShapeSize; ++y)
                for (int x = 0; x < mShapeSize; ++x)
                    Shape[y, x] = EMPTYSYMBOL;

            UpdateSize();

            for (int y = 0; y < mWidth; ++y)
                for (int x = 0; x < mHeight; ++x)
                {
                    int x_n = mWidth - 1 - y;
                    int y_n = x;
                    Shape[y_n, x_n] = mShape[y, x];
                }

            mShape = Shape;
        }
        public abstract AShape Clone();

        public AShape(CField field)
        {
            mField = field;

            mTopLeft.Color = colorArray[mRandom.Next(0, colorArray.Length)];

            for (int i = 0; i < MAX_SHAPE_SIZE; ++i)
            {
                for (int j = 0; j < MAX_SHAPE_SIZE; ++j)
                {
                    mShape[i, j] = EMPTYSYMBOL;
                }
            }
        }

        protected void UpdateSize()
        {
            Swap(ref mWidth, ref mHeight);
        }

        public char GetChar(int x, int y) { return mShape[y, x]; }

        public int Height { get { return mHeight; } }
        public int Width { get { return mWidth; } }

        public static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }
        public void Print()
        {
            for (int y = mHeight -1; y >= 0 ; --y)
            {
                mField.Color = mTopLeft.Color;
                for (int x = 0; x < mWidth; ++x)
                {

                    if (GetChar(x, y) != EMPTYSYMBOL )
                    {
                            mField.SetPoint(x + mTopLeft.X, y + mTopLeft.Y, GetChar(x, y));
                    }
                }
            }

        }

        public void Erase()
        {
            mField.SetPrintLine(mTopLeft.Y - 1, mHeight + 2);

            for (int y = 0; y < mHeight; ++y)
            {
                for (int x = 0; x < mWidth; ++x)
                {
                    if(mShape[y,x] != EMPTYSYMBOL)
                        mField.SetPoint(x + mTopLeft.X, y + mTopLeft.Y, EMPTYSYMBOL);  
                }
            }

        }

        public void MoveLeft()
        {
            if (CheckLeftMoove())
            {
                Erase();

                mTopLeft.X = mTopLeft.X - 1;

                Print();
            }

        }

        public void MoveRight()
        {
            if (CheckRightMoove())
            {
                Erase();

                mTopLeft.X = mTopLeft.X + 1;

                Print();
            }

        }
        
        public void MoveDown()
        {
            if (CheckDownMoove())
            {
                Erase();
                mTopLeft.Y = mTopLeft.Y + 1;
                Print();
                if (!CheckDownMoove())
                    mField.CheckField();

            }
            else
                throw new CExBottomReached();
        }

        // установить координаты для окна следующей фигуруы
        public void SetNextShape()
        {
            mTopLeft.X = СTetrisApp.FIELD_WIDTH - 5;
            mTopLeft.Y = 2;
        }

        protected bool CheckRotate()
        {
            bool result = true;
            // проверяем свободны ли места куда должны стать не пустые ячейки
            for (int y = 0; result && y < Height; ++y)
                for (int x = 0; result && x < Width; ++x)
                { //  Параллельный сдвиг и поворот координат осей 
                    int x_n = Height - 1 - y;
                    int y_n = x;
                    if (GetChar(x, y) != EMPTYSYMBOL)   // если ячейка  не пуста
                        result = (GetChar(x_n, y_n) != EMPTYSYMBOL ||  // и новое положение ячейки не принадлежит фигуре
                       mField.GetPoint(mTopLeft.X + x_n, mTopLeft.Y + y_n).Symbol == EMPTYSYMBOL);  // пусто ли там ?
                }

            //  проверим нет ли помех на пути поврота ?
            // для этого проверим диагональ  правый-верхний -> левый-нижний угол
            bool rightUpFound = false; //флаг
            for (int y = Height - 1; !rightUpFound && result && y >= 0; --y)
                for (int x = Width - 1; result && x >= 0; --x)
                {
                    // ищем нижний правый не пустой символ
                    if (GetChar(x, y) != EMPTYSYMBOL)
                    {  //  проверяем диагональ
                        while (result && x >= 0 && y < Width - 1)
                        {
                            y++;
                            result = mField.GetPoint(mTopLeft.X + x, mTopLeft.Y + y).Symbol == EMPTYSYMBOL;
                            x--;
                        }
                        rightUpFound = true;
                        break;
                    }
                }

            return result;
        }

        public bool CheckDownMoove()
        {
            bool result = true;
            for (int x = 0; result && x < Width; ++x)
            {
                // сравниваем нижнюю часть фигуры с символами на экране
                int shift = 0; // кол-во пустых с низу ячеек в столце x
                while (GetChar(x, Height - 1 - shift) == EMPTYSYMBOL)
                    shift++;
                //mObj1->GetChar(x, Height - 1 - shift) == EMPTYSYMBOL ||
                result = (mField.GetPoint(mTopLeft.X + x, mTopLeft.Y + Height - shift).Symbol == EMPTYSYMBOL);
            }

            return result;
        }

        bool CheckLeftMoove()
        {
            bool result = true;
            for (int y = 0; result && y < Height; ++y)
            {
                int shift = 0; //кол-во пустых ячеек с лева в строке у
                while (GetChar(shift, y) == EMPTYSYMBOL)
                    shift++;
                //mObj1->GetChar(shift, y) == EMPTYSYMBOL ||
                result = (mField.GetPoint(mTopLeft.X - 1 + shift, mTopLeft.Y + y).Symbol == EMPTYSYMBOL);
            }
            return result;
        }

        bool CheckRightMoove()
        {
            bool result = true;
            for (int y = 0; result && y < Height; ++y)
            {
                int shift = 0; //кол-во пустых ячеек с права в строке у
                while (GetChar(Width - 1 - shift, y) == EMPTYSYMBOL)
                    shift++;
                //mObj1->GetChar(Width -1 - shift, y) == EMPTYSYMBOL ||
                result = (mField.GetPoint(mTopLeft.X + Width - shift, mTopLeft.Y + y).Symbol == EMPTYSYMBOL);
            }

            return result;
        }

        public ConsoleColor Color
        { set { mTopLeft.Color = value; } }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    mShape   = null;
                }
                disposed = true;
            }
        }

    }

}
