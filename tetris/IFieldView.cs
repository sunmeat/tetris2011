using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    interface IFieldView
    {
        SPoint GetPoint(int absCoord);
        SPoint GetPoint(int x, int y);
    }
}
