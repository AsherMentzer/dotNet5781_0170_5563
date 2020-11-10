using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
   public enum areas { General, North, West, Center, Jerusalem };
    public class BusLine: IComparable
    {
        int busLine;
        BusStationLine firstStation;
        BusStationLine lastStation;
        areas area;
        List<BusStationLine> Stations= new List<BusStationLine>();
       public BusLine(int _busLine, BusStationLine first, BusStationLine last,areas _area)
        {
            busLine = _busLine;
            firstStation = first;
            lastStation = last;
            area = _area;
            Stations.Add(firstStation);
            lastStation.DistanceFromLastStation = GetDistance(firstStation,lastStation);
            lastStation.TimeFromLastStation = lastStation.DistanceFromLastStation / 40;
            stations.Add(lastStation);
        }

        public int GetBusLine { get => busLine; }
        public BusStationLine FirstStation { get => firstStation; }
        public BusStationLine LastStation { get => lastStation; }
        public areas GetArea { get => area; }
         public List<BusStationLine> stations { get=>Stations; }
        public override string ToString()
        {
            string temp = $"bus line: {busLine}, area: {area}, from : {firstStation.GetBusStationNumber} to: {lastStation.GetBusStationNumber}\nstations numbers: ";
            foreach (var station in Stations) { temp += $"{station.GetBusStationNumber} "; };
            return temp;
        }
        //need to add all the details to busStopLine class
        void addStationToLine(BusStationLine newStation)
        {
            if (isStationInLine(newStation))
            {
                Console.WriteLine("this station already in the line");
                return;
            }

            Console.WriteLine("please enter the number of the location of the station in the line");
            int index;
            while (!int.TryParse(Console.ReadLine(), out index)
                || index < 0 || index > Stations.Count+1)
            {
                Console.WriteLine("enter only number in the range of the list");
            }
            
            if (index == 0)
            {
                Stations.Insert(0,newStation);
                Stations[index + 1].DistanceFromLastStation =
                    GetDistance(newStation, Stations[index + 1]);
                Stations[index + 1].TimeFromLastStation =
                    Stations[index + 1].DistanceFromLastStation / 40;
                firstStation = newStation;
                return;
            }
            else if (index == Stations.Count+1)
            {
                Stations.Add(newStation);
               Stations[index].DistanceFromLastStation =
                    GetDistance(newStation, Stations[index - 1]);
                Stations[index].TimeFromLastStation =
                    Stations[index].DistanceFromLastStation / 40;
                lastStation = newStation;
                return;
            }
            Stations[index + 1].DistanceFromLastStation =
                   GetDistance(newStation, Stations[index + 1]);
            Stations[index + 1].TimeFromLastStation =
                Stations[index + 1].DistanceFromLastStation / 40;
            Stations[index].DistanceFromLastStation =
                   GetDistance(newStation, Stations[index - 1]);
            Stations[index].TimeFromLastStation =
                Stations[index - 1].DistanceFromLastStation / 40;
            return;
        }
        public void DeleteStstion(BusStationLine station)
        {
            if (!isStationInLine(station))
            {
                Console.WriteLine("error: this station not exist");
                return;
            }

            for (int index = 0; index < Stations.Count; index++)
            {
                if (Stations[index] == station)
                {
                    if(index==0)
                    {
                        firstStation = Stations[index + 1];
                        firstStation.DistanceFromLastStation = 0;
                        firstStation.TimeFromLastStation = 0;
                        Stations.RemoveAt(index);
                        return;
                    }
                    else if(index== Stations.Count)
                    {
                        lastStation= Stations[index - 1];
                        Stations.RemoveAt(index);
                        return;
                    }
                    Stations[index + 1].DistanceFromLastStation =
                   GetDistance(Stations[index - 1], Stations[index + 1]);
                    Stations[index + 1].TimeFromLastStation =
                        Stations[index + 1].DistanceFromLastStation / 40;
                    Stations.RemoveAt(index);
                }
            }
        }
        bool isStationInLine(BusStationLine station)
        {
            foreach (var stationLine in Stations)
                if (stationLine.GetBusStationNumber == station.GetBusStationNumber)
                    return true;

            return false;
        }
        public double GetDistance(BusStationLine busStopA, BusStationLine busStopB)
        {
            return Math.Sqrt(Math.Pow(busStopA.GetLatitude - busStopB.GetLatitude, 2) +
                Math.Pow(busStopA.GetLongitude - busStopB.GetLongitude, 2)) / 1000;
        }
        public double GetTravelTime(BusStationLine busStopA, BusStationLine busStopB)
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
                    TravelTime += station.TimeFromLastStation;
                }
                if (station == busStopB)
                    break;
            }
            return TravelTime;
        }

        public BusLine SubLine(BusStationLine first, BusStationLine last)
        {
            //List<BusStationLine> SubStations = new List<BusStationLine>();
            BusLine subLine = new BusLine(busLine, firstStation, lastStation, area);
            // int index = Stations.FindIndex(first);
            bool exist=false;
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i] == first)
                {
                    exist = true;
                    for (; i < Stations.Count; i++)
                    {
                        subLine.addStationToLine(Stations[i]);
                        if (Stations[i] == last)
                            return subLine;
                    }
                }
            }
            if (! exist)
                Console.WriteLine("the first bus station not exist in this line");
            else
                Console.WriteLine("the last station not exist from " +
                    "staion : {0} in this line", first.GetBusStationNumber);
            return null;
        }

        public int ComparTwoLines(BusStationLine first,BusStationLine last,
            BusLine busLineA,BusLine busLineB)
        {
            BusLine SubLineA = busLineA.SubLine(first, last);
            BusLine SubLineB = busLineB.SubLine(first, last);
            return  SubLineA.CompareTo(SubLineB);
            ////check in main and print wich is bigger--------------------------------
        }

        public int CompareTo(object obj)
        {
            return GetTravelTime(firstStation, lastStation).CompareTo(((BusLine)obj)
                .GetTravelTime(((BusLine)obj).firstStation, ((BusLine)obj).lastStation));
        }
       
    }
}
