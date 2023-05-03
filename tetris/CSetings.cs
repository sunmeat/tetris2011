using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    class CSetings
    {
        public static int FIELD_HEIGHT = 41;
        public static int FIELD_WIDTH = 50;
        public static void SetSetings()
        {
            System.Console.WriteLine("Введите ширину поля (не менее 20) ");
            var buff = System.Console.ReadLine();
            var width = 0;
                try
                {
                    width = System.Convert.ToInt32(buff);
                }
                catch (Exception )
                {
                width = 0;
                }
            System.Console.WriteLine("Введите высоту поля (не менее 20)");
            buff = System.Console.ReadLine();
            var height = 0;

                try
                {
                    height = System.Convert.ToInt32(buff);
                }
                catch (Exception )
                {
                    width = 0;
                }
            System.Console.Clear();

            if (height >= 20 && height < 60)
            {
                FIELD_HEIGHT = height;
            }

            if (width >= 20 && width < 60)
            {
                FIELD_WIDTH = width;
            }
        }
    }

}
