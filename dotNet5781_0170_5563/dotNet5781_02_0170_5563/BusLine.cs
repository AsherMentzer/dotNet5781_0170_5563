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
        BusStop firstStop;
        BusStop lastStop;
        areas area;
        List<BusStop> aSideStations;
        List<BusStop> bSideStations;


        BusLine(int _busLine, BusStop first, BusStop last, areas _area, List<BusStop> aStops, List<BusStop> bStops)
        {
            busLine = _busLine;
            firstStop = first;
            lastStop = last;
            area = _area;
            aSideStations = aStops;
            bSideStations = bStops;
        }

        public override string ToString()
        {
            string temp = $"bus line: {busLine}, area: {area}, from : {firstStop} to: {lastStop}\nstations numbers: ";
            foreach (var stop in aSideStations) { temp += $"{stop.BusStopNumber} "; };
            temp += $"\nfrom : {lastStop} to: {firstStop}\nstations numbers: ";
            foreach (var stop in bSideStations) { temp += $"{stop.BusStopNumber} "; };
            return temp;
        }

        void addStationToLine()
        {
            //int numStation;
            //int index; 
            //BusStop newStop; 
            //char side;
            //Console.Write("enter the number of the station: ");
            //while(!int.TryParse(Console.ReadLine(), out numStation))
            //    Console.WriteLine("enter a number only");
            //    foreach(var number in )
            //if (side == a)
            //    list<stations
            //if (index < 0 || index > stations.Count + 1)
            //    throw null;// should be change-------------------------------
            //stations.Insert(index, newStop);
        }
    }
}
