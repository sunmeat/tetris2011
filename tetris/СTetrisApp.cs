using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    class СTetrisApp
    {

        static readonly int SHAPE_COUNT = 7;
        readonly AShape[] mShape = new AShape[SHAPE_COUNT];
        AShape mObj1;
        AShape mObj2;
        readonly CFiled mField;
        readonly CControl mControl;
        readonly Random mRandom = new Random();

        public СTetrisApp(ref CFiled field)
        {
            mField = field;
            mControl = new CControl();

            mShape[0] = new CShapeI(field);
            mShape[1] = new CShapeZ(field);

            mObj1 =  new CShapeO(field);
            mObj2 = new CShapeO(field);

            mControl.SetShapeControl(mObj1);

            mObj2.SetNextShape();
            
            mObj2.Print();
            mObj1.Print();

        }

        void NextShape()
        {

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
        }

    }
}
