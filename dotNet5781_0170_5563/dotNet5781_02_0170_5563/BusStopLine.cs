using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
    public class BusStopLine
    {
        BusStop stop;
        double distanceFromLastStop;
        double timeFromLastStop;

        BusStopLine(BusStop newStop, float distance=0, float time=0)
        {
            stop = newStop;
            distanceFromLastStop = distance;
            timeFromLastStop = time;
        }
        public BusStop GetStop{get=>stop;}
        public double DistanceFromLastStop { get => distanceFromLastStop;
            set => distanceFromLastStop = value; }
        public double TimeFromLastStop { get => timeFromLastStop; set => timeFromLastStop = value; }

    }
}
