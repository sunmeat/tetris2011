using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    class CStopwatch
    {
        public CStopwatch()
        {
            DateTime now = new DateTime();
            now = DateTime.Now;
            mStartTime = now.Ticks;
        }
        public double Now()
        {
            // возвращает число миллисекунд после вызова Start
            //now = DateTime.Now;
            double liPerfNow = DateTime.Now.Ticks;
            return ((liPerfNow - mStartTime) / 10000);
        }

        private double mStartTime;
    }
}
