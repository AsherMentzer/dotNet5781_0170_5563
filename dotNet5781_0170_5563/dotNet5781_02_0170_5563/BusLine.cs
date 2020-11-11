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
        double speedAverage=0.04;
       public BusLine(int _busLine, BusStation first, BusStation last,areas _area)
        {
            busLine = _busLine;
            firstStation =new BusStationLine(first);
            lastStation = new BusStationLine(last);
            area = _area;
            Stations.Add(firstStation);
            lastStation.DistanceFromLastStation = GetDistance(firstStation,lastStation);
            lastStation.TimeFromLastStation = TimeSpan.FromMinutes(lastStation.DistanceFromLastStation / speedAverage);
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
        public void addStationToLine(BusStation station)
        {
            BusStationLine newStation = new BusStationLine(station);
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
                   TimeSpan.FromMinutes(Stations[index + 1].DistanceFromLastStation / speedAverage);
                firstStation = newStation;
                return;
            }
            else if (index == Stations.Count+1)
            {
                Stations.Add(newStation);
               Stations[index].DistanceFromLastStation =
                    GetDistance(newStation, Stations[index - 1]);
                Stations[index].TimeFromLastStation =
                   TimeSpan.FromMinutes(Stations[index].DistanceFromLastStation / speedAverage);
                lastStation = newStation;
                return;
            }

            Stations.Insert(index, newStation);
            Stations[index + 1].DistanceFromLastStation =
                   GetDistance(newStation, Stations[index + 1]);
            Stations[index + 1].TimeFromLastStation =
               TimeSpan.FromMinutes(Stations[index + 1].DistanceFromLastStation / speedAverage);
            Stations[index].DistanceFromLastStation =
                   GetDistance(newStation, Stations[index - 1]);
            Stations[index].TimeFromLastStation =
               TimeSpan.FromMinutes(Stations[index - 1].DistanceFromLastStation / speedAverage);
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
                        firstStation.TimeFromLastStation =new TimeSpan();
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
                     TimeSpan.FromMinutes(Stations[index + 1].DistanceFromLastStation / speedAverage);
                    Stations.RemoveAt(index);
                }
            }
        }
       public bool isStationInLine(BusStationLine station)
        {
            foreach (var stationLine in Stations)
                if (stationLine.GetBusStationNumber == station.GetBusStationNumber)
                    return true;

            return false;
        }
        public double GetDistance(BusStationLine busStopA, BusStationLine busStopB)
        {
            return Math.Sqrt(Math.Pow(busStopA.GetLatitude - busStopB.GetLatitude, 2) +
                Math.Pow(busStopA.GetLongitude - busStopB.GetLongitude, 2));
        }
        public TimeSpan GetTravelTime(BusStationLine busStopA, BusStationLine busStopB)
        {
            TimeSpan TravelTime = new TimeSpan();
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

        public BusLine SubLine(BusStation first, BusStation last)
        {
            BusLine subLine = new BusLine(busLine, first, last, area);
           
            bool exist=false;
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i].GetBusStationNumber == first.BusStationNumber)
                {
                    exist = true;
                    subLine.addS(Stations[i]);
                    subLine.stations[0].DistanceFromLastStation = 0;
                    subLine.stations[0].TimeFromLastStation = new TimeSpan();
                    for (++i ; i < Stations.Count; ++i)
                    {
                        subLine.addS(Stations[i]);
                        if (Stations[i].GetBusStationNumber == last.BusStationNumber)
                            return subLine;
                    }
                }
            }
            if (! exist)
                Console.WriteLine("the first bus station not exist in this line");
            else
                Console.WriteLine("the last station not exist from " +
                    "staion : {0} in this line", first.BusStationNumber);
            return null;
        }
        private void addS(BusStationLine station)
        {
            Stations.Add(station);
        }
        public int ComparTwoLines(BusStation first,BusStation last,
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
