using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    class CShapeL : AShape
    {
        public CShapeL(CField field)
            : base(field)
        {
            mWidth = 3;
            mHeight = 2;
            mShapeSize = 3;
            mShape[0, 2] =
                mShape[1, 0] =
                mShape[1, 1] =
                mShape[1, 2] = SHAPESYMBOL;

        }

        public override AShape Clone()
        {
            AShape newShape = new CShapeL(mField);
            newShape.Color = mTopLeft.Color;
            return newShape;
        }

    }
}
