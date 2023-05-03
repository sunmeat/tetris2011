using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    class CShapeI : AShape
    {
        public CShapeI(CField field) : base(field)
        {
            mWidth = 4;
            mHeight = 1;
            mShapeSize = 4;
            mShape[0, 0] =
                mShape[0, 1] =
                mShape[0, 2] =
                mShape[0, 3] = SHAPESYMBOL;
            //mTopLeft.Color = ConsoleColor.DarkMagenta;

        }
        public override AShape Clone() { 
            AShape newShape = new CShapeI(mField);
            newShape.Color = mTopLeft.Color;
            return newShape;
        }
        public override void Rotate() 
        {
            if (!CheckRotate())
                return;

            //int maxSide = System.Math.Max(mWidth, mHeight);
            //mField.SetPrintRectangle(mTopLeft.Y, mTopLeft.X, maxSide, maxSide);

            Erase();

            for (int x = 1; x < mShapeSize; ++x)
            {
                Swap(ref mShape[x,0], ref mShape[0,x]);
            }

            UpdateSize();
        }

    }
}
