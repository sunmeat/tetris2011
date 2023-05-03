using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    class СTetrisApp : ITetrisControl
    {
        //********************
        public static СTetrisApp instance = null;
        public static readonly int FIELD_HEIGHT;
        public static readonly int FIELD_WIDTH;
        public const int NEXT_SHAPE_WINDOW_WIDTH = 7;
        public const int NEXT_SHAPE_WINDOW_HEIGHT = 5;

        public static readonly int FIELD_GAME_WIDTH;
        CBox EndGameBox;

        //********************

        bool mGameOver = false;
        static readonly int SHAPE_COUNT = 7;
        readonly AShape[] mShape = new AShape[SHAPE_COUNT];
        AShape mObj1;
        AShape mObj2;
        static CField mField;

        readonly CControl mControl;
        readonly Random mRandom = new Random();
        public CField Field
        {
            get { return mField; }
        }

        public static СTetrisApp GetInstance()
        {
            if (instance == null)
            {
                instance = new СTetrisApp();
            }
            return instance;
        }
        static СTetrisApp()
        {
            FIELD_HEIGHT = CSetings.FIELD_HEIGHT + 2;
            FIELD_WIDTH = CSetings.FIELD_WIDTH + NEXT_SHAPE_WINDOW_WIDTH + 3;
            FIELD_GAME_WIDTH = FIELD_WIDTH - NEXT_SHAPE_WINDOW_WIDTH;
            mField = CField.GetInstance(FIELD_WIDTH, FIELD_HEIGHT);
        }

        private СTetrisApp()
        {
            Console.CursorVisible = false;
            CBox box = new CBox(FIELD_WIDTH - NEXT_SHAPE_WINDOW_WIDTH - 1, FIELD_HEIGHT - 1, ConsoleColor.Blue, ref mField);
            CBox box2 = new CBox(NEXT_SHAPE_WINDOW_WIDTH, NEXT_SHAPE_WINDOW_HEIGHT, ConsoleColor.DarkYellow, ref mField);
            EndGameBox = new CBox(17, 4, ConsoleColor.Red, ref mField);

            box.print(0, 0);
            box2.print(FIELD_WIDTH - NEXT_SHAPE_WINDOW_WIDTH, 0);
            mField.SetPrintLine(0, FIELD_HEIGHT);

            mShape[0] = new CShapeI(mField);
            mShape[1] = new CShapeZ(mField);
            mShape[2] = new CShapeS(mField);
            mShape[3] = new CShapeL(mField);
            mShape[4] = new CShapeO(mField);
            mShape[5] = new CShapeT(mField);
            mShape[6] = new CShapeJ(mField);

            mObj1 = new CShapeL(mField);
            mObj2 = new CShapeO(mField);

            //mControl.SetShapeControl(mObj1);

            mObj2.SetNextShape();

            mObj2.Print();
            mObj1.Print();
            mControl = new CControl(this);

        }

        public AShape GetShapeUnderControl()
        {
            return mObj1;
        }

        public void NextShape()
        {
            if (!mGameOver)
            {
                mObj1.Dispose();
                mObj1 = mObj2.Clone();
                try
                {
                    mObj1.Print();
                }
                catch (Exception)
                {
                    //ClearField();
                }

                mObj2.Erase();


                mObj2 = mShape[mRandom.Next(0, SHAPE_COUNT)];
                mObj2.SetNextShape();

                mObj2.Print();
            }

        }

        public void GameOver()
        {
            mField.Color = AShape.colorArray[mRandom.Next(0, AShape.colorArray.Count())];

            mField.PrintString(5, 5, " Игра окончена");
            mField.PrintString(5, 6, " Для выхода X");
            mField.PrintString(5, 7, " Для повтора R");

            mGameOver = true;
            EndGameBox.print(4, 4);
            mField.SetPrintLine(0, FIELD_HEIGHT);
        }

        public void ClearGameField()
        {
            if (mGameOver)
            {
                mField.ClearGameField();
                mField.SetPrintLine(0, FIELD_HEIGHT);
                mGameOver = false;
            }

        }
    }
}
