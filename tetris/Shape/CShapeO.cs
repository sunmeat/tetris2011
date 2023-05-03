using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    class CShapeO : AShape
    {
        public CShapeO(CField field)
            : base(field)
        {
            mWidth = 2;
            mHeight = 2;
            mShapeSize = 2;
            mShape[0, 0] =
                mShape[1, 0] =
                mShape[0, 1] =
                mShape[1, 1] = SHAPESYMBOL;
        }

        public override AShape Clone()
        {
            AShape newShape = new CShapeO(mField);
            newShape.Color = mTopLeft.Color;
            return newShape;
        }
        public override void Rotate()
        {
        }
    }
}
