using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
    enum areas { General, North, West, Center, Jerusalem };
    class BusLine
    {
        int busLine;
        BusStopLine firstStop;
        BusStopLine lastStop;
        areas area;
        List<BusStopLine> Stations;
        BusLine(int _busLine, BusStopLine first, BusStopLine last,
            areas _area, List<BusStopLine> stops)
        {
            busLine = _busLine;
            firstStop = first;
            lastStop = last;
            area = _area;
            Stations = stops;
        }

        public override string ToString()
        {
            string temp = $"bus line: {busLine}, area: {area}, from : {firstStop} to: {lastStop}\nstations numbers: ";
            foreach (var stop in Stations) { temp += $"{stop.GetStop.BusStopNumber} "; };
            return temp;
        }
        //need to add all the details to busStopLine class
        void addStationToLine(BusStopLine newStop)
        {
            if (isStationInLine(newStop))
            {
                Console.WriteLine("this station already in the line");
                return;
            }

            Console.WriteLine("please enter the number of the station in the line");
            int index;
            while (!int.TryParse(Console.ReadLine(), out index)
                || index < 0 || index > Stations.Count)
            {
                Console.WriteLine("enter only number in the range of the list");
            }
            Stations.Insert(index, newStop);
            /////////////////need to update all the travel time and distance
            if (index == 0)
            {
                Stations[index + 1].DistanceFromLastStop =
                    GetDistance(newStop.GetStop, Stations[index + 1].GetStop);
                Stations[index + 1].TimeFromLastStop =
                    Stations[index + 1].DistanceFromLastStop / 40;
                firstStop = newStop;
                return;
            }
            else if (index == Stations.Count - 1)
            {
                Stations[index - 1].DistanceFromLastStop =
                    GetDistance(newStop.GetStop, Stations[index - 1].GetStop);
                Stations[index - 1].TimeFromLastStop =
                    Stations[index - 1].DistanceFromLastStop / 40;
                lastStop = newStop;
                return;
            }
            Stations[index + 1].DistanceFromLastStop =
                   GetDistance(newStop.GetStop, Stations[index + 1].GetStop);
            Stations[index + 1].TimeFromLastStop =
                Stations[index + 1].DistanceFromLastStop / 40;
            Stations[index - 1].DistanceFromLastStop =
                   GetDistance(newStop.GetStop, Stations[index - 1].GetStop);
            Stations[index - 1].TimeFromLastStop =
                Stations[index - 1].DistanceFromLastStop / 40;
            return;
        }

        bool isStationInLine(BusStopLine bus)
        {
            foreach (var station in Stations)
                if (station.GetStop.BusStopNumber == bus.GetStop.BusStopNumber)
                    return true;

            return false;
        }
        public double GetDistance(BusStop busStopA, BusStop busStopB)
        {
            return Math.Sqrt(Math.Pow(busStopA.Latitude - busStopB.Latitude, 2) +
                Math.Pow(busStopA.Longitude - busStopB.Longitude, 2)) / 1000;
        }
        public double GetTravelTime(BusStopLine busStopA, BusStopLine busStopB)
        {
            double TravelTime = 0;
            bool flag = false;
            foreach (var station in Stations)
            {
                if (station == busStopA)
                {
                    flag = true;
                    continue;
                }

                if (flag)
                {
                    TravelTime += station.TimeFromLastStop;
                }
                if (station == busStopB)
                    break;
            }
            return TravelTime;
        }
    }
}
