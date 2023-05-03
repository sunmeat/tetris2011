using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    class CShapeT : AShape
    {
        public CShapeT(CField field)
            : base(field)
        {
            mWidth = 3;
            mHeight = 2;
            mShapeSize = 3;
            mShape[0, 0] =
                mShape[0, 1] =
                mShape[0, 2] =
                mShape[1, 1] = SHAPESYMBOL;

        }

        public override AShape Clone()
        {
            AShape newShape = new CShapeT(mField);
            newShape.Color = mTopLeft.Color;
            return newShape;
        }

    }
}
