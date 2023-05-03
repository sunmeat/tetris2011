using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    class Box
    {

        public Box(int sizeX, int sizeY)
        {
            if (sizeX <= 0) sizeX = 3;
            if (sizeY <= 0) sizeY = 3;

            mSizeX = sizeX;
            mSizeY = sizeY;
        }

        static public int AbsoluteCoord(int x, int y, int width)
        {
            return (y * width + x);
        }

        public void print(int left, int top, ref char[] output, int width)
        {


            const char TOPLEFT = '╔';
            const char TOPRIGHT = '╗';
            const char HORIZON_LINE = '═';
            const char VERTICAL_LINE = '║';

            int topLeft =AbsoluteCoord(left, top, width);
            output[topLeft] = TOPLEFT;


            string horizon_line = new String('═', mSizeX - 2);
            char[] buffer = horizon_line.ToCharArray();

            //Console.SetCursorPosition(left, top);
            //System.Console.Write(TOPLEFT);

            Array.Copy(buffer, 0, output, topLeft + 1, mSizeX - 2);
            output[mSizeX + left - 1] = TOPRIGHT;
            
            //System.Console.BufferWidth = Console.WindowWidth = 20;
            //System.Console.WriteLine(System.Console.BufferWidth);
            //System.Console.WriteLine(Console.WindowWidth);


            //string TMP = "╔═╗  13456789/s* 45646dddddddd++++++++++++++4\n" + n;
            //char[] buffer = TMP.ToCharArray();

            //System.Console.Write(buffer, 0, buffer.Count());

            //

        }
        private int mSizeX;
        private int mSizeY;

    }
}
