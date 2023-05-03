using System;


namespace tetris
{
    class CShapeZ : AShape
    {
        public CShapeZ(CField field)
            : base(field)
        {
            mWidth = 3;
            mHeight = 2;
            mShapeSize = 3;
            mShape[0, 0] =
                mShape[0, 1] =
                mShape[1, 1] =
                mShape[1, 2] = SHAPESYMBOL;

        }

        public override AShape Clone() {
            AShape newShape = new CShapeZ(mField);
            newShape.Color = mTopLeft.Color;
            return newShape; 
        
        }

    }
}
