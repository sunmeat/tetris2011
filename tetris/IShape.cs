using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    interface IShape
    {
         AShape Clone();
         void Rotate();
         void MoveLeft();
         void MoveDown();

         void Print();
         void Erase();
    }
}
