using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    interface ITetrisControl
    {
        void NextShape();
        void GameOver();
        void ClearGameField();
        AShape GetShapeUnderControl();

    }
}
