using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
    class BusStopLine
    {
        BusStop stop;
        float distanceFromLastStop;
        float timeFromLastStop;

        BusStopLine(BusStop newStop, float distance, float time)
        {
            stop = newStop;
            distanceFromLastStop = distance;
            timeFromLastStop = time;
        }
    }
}
