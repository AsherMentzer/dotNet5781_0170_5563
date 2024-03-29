﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
    /// <summary>
    /// enum for the area of the line
    /// </summary>
    public enum areas { General = 1, North, West, Center, Jerusalem };
    /// <summary>
    /// class for bus line have deatails of the bus line number
    /// list if all the stationd in line and the first and last station
    /// is heir from Icomperable so we can compare 2 lines 
    /// </summary>
    public class BusLine : IComparable
    {
        int busLine;
        BusStationLine firstStation;
        BusStationLine lastStation;
        areas area;
        List<BusStationLine> Stations = new List<BusStationLine>();
        double speedAverage = 40;//speed average for bus is 40 km per hour
        /// <summary>
        /// constructor that get the deatails of the first and last stations and 
        /// build line with this 2 stations  claculate the
        ///distance and the time from first to last station
        /// </summary>
        /// <param name="_busLine">the line number</param>
        /// <param name="first">first station</param>
        /// <param name="last">last station</param>
        /// <param name="_area">the area of the line</param>
        public BusLine(int _busLine, BusStation first, BusStation last, areas _area)
        {
            busLine = _busLine;
            firstStation = new BusStationLine(first);
            lastStation = new BusStationLine(last);
            area = _area;
            Stations.Add(firstStation);
            lastStation.DistanceFromLastStation = GetDistance(firstStation, lastStation);
            lastStation.TimeFromLastStation = TimeSpan.FromMinutes(lastStation.DistanceFromLastStation / speedAverage);
            stations.Add(lastStation);
        }
        /// <summary>
        /// constructor that get the deatails of the line list of stations and 
        /// build line with this  stations and claculate the
        ///distance and the time from station to next station
        /// </summary>
        /// <param name="_busLine">the num of line</param>
        /// <param name="_area">the area</param>
        /// <param name="stations">list of the stations</param>
        public BusLine(int _busLine, areas _area, List<BusStation> stations)
        {
            busLine = _busLine;

            BusStationLine sLine = new BusStationLine(stations[0]);
            Stations.Add(sLine);
            //foreach station calculate the time and distance and add to stations list
            for (int i = 1; i < stations.Count; ++i)
            {
                BusStationLine stationLine = new BusStationLine(stations[i]);
                Stations.Add(stationLine);
                Stations[i].DistanceFromLastStation = GetDistance(Stations[i - 1], Stations[i]);
                Stations[i].TimeFromLastStation = TimeSpan.FromMinutes(stationLine.DistanceFromLastStation / speedAverage);

            }
            firstStation = Stations[0];
            lastStation = Stations[Stations.Count - 1];
            area = _area;
        }
        public int GetBusLine { get => busLine; }
        public BusStationLine FirstStation { get => firstStation; }
        public BusStationLine LastStation { get => lastStation; }
        public areas GetArea { get => area; }
        public List<BusStationLine> stations { get => Stations; }
        /// <summary>
        ///  all the deatails of the line num-area-first and last
        ///  station and all the station in the line 
        /// </summary>
        /// <returns>the string with all the deatails</returns>
        public override string ToString()
        {
            //string temp = $"bus line: {busLine}, area: {area}, from : {firstStation.GetBusStationNumber} " +
            //    $"to: {lastStation.GetBusStationNumber}\nstations numbers: ";
            string temp = "";
            foreach (var station in Stations) 
            { 
                temp += $"{station.GetBusStationNumber} " +
                    $"{station.GetLatitude} {station.GetLongitude} {station.TimeFromLastStation}\n";
            };
            return temp;
        }
        /// <summary>
        /// get station and aad it to the line
        /// </summary>
        /// <param name="station">real station</param>
        public void addStationToLine(BusStation station)
        {
            BusStationLine newStation = new BusStationLine(station);

            //if this station already exist in this line
            if (isStationInLine(newStation))
                throw new DuplicateNameException("this station already in the line");

            //in witch place in line you want this staion like stop number 5 etc..
            Console.WriteLine("please enter the number of the location of the station in the line");
            int index;
            while (!int.TryParse(Console.ReadLine(), out index)
                || index < 0 || index > Stations.Count + 1)
            {
                Console.WriteLine("enter only number in the range of the list");
            }
            //if is the first sattion in the line time and distance=0
            //and update the old first station time and distance details  
            if (index == 0)
            {
                Stations.Insert(0, newStation);
                Stations[index + 1].DistanceFromLastStation =
                    GetDistance(newStation, Stations[index + 1]);
                Stations[index + 1].TimeFromLastStation =
                   TimeSpan.FromMinutes(Stations[index + 1].DistanceFromLastStation / speedAverage);
                firstStation = newStation;
                return;
            }
            //if is the last station no need to update the old last station deatails
            //only this satation 
            else if (index == Stations.Count + 1)
            {
                Stations.Add(newStation);
                Stations[index].DistanceFromLastStation =
                     GetDistance(newStation, Stations[index - 1]);
                Stations[index].TimeFromLastStation =
                   TimeSpan.FromMinutes(Stations[index].DistanceFromLastStation / speedAverage);
                lastStation = newStation;
                return;
            }
            //if is in the middle of the line need to update this station and 
            //the station after this one with time and distance
            Stations.Insert(index, newStation);
            Stations[index + 1].DistanceFromLastStation =
                   GetDistance(newStation, Stations[index + 1]);
            Stations[index + 1].TimeFromLastStation =
               TimeSpan.FromMinutes(Stations[index + 1].DistanceFromLastStation / speedAverage);
            Stations[index].DistanceFromLastStation =
                   GetDistance(newStation, Stations[index - 1]);
            Stations[index].TimeFromLastStation =
               TimeSpan.FromMinutes(Stations[index].DistanceFromLastStation / speedAverage);
            return;
        }
        /// <summary>
        /// delete a station from the line and aupdates the the rest of satation in the line
        /// </summary>
        /// <param name="station">the station to delelte</param>
        public void DeleteStstion(BusStationLine station)
        {
            // if there is only 2 stations in the line (line must contain at least 2 stations)
            if (Stations.Count == 2) throw new MinimumStationsException
                        ("there is only 2 stations in this line so you can't delete anymore");

            // in case the station not in the line
            if (!isStationInLine(station))
                throw new KeyNotFoundException("error: this station not exist");

            // fined the station in stations list and delete it
            for (int index = 0; index < Stations.Count; index++)
            {
                if (Stations[index] == station)
                {
                    // if the first station is deleted 
                    if (index == 0)
                    {
                        firstStation = Stations[index + 1];         // update the first station of the line
                        firstStation.DistanceFromLastStation = 0;   // update the distance to 0
                        firstStation.TimeFromLastStation = new TimeSpan(); // update the travel tim to 0
                        Stations.RemoveAt(index);                   // delete
                        return;
                    }
                    // if the last station is deleted 
                    else if (index == Stations.Count)
                    {
                        lastStation = Stations[index - 1];  // update the last stop of the line
                        Stations.RemoveAt(index);           // delete
                        return;
                    }

                    // if the station that deleted is in the middle of the line
                    Stations[index + 1].DistanceFromLastStation =
                    GetDistance(Stations[index - 1], Stations[index + 1]); // updeate the distance
                    Stations[index + 1].TimeFromLastStation =
                    TimeSpan.FromMinutes(Stations[index + 1].DistanceFromLastStation / speedAverage); // update the travel time
                    Stations.RemoveAt(index);
                }
            }
        }


        /// <summary>
        /// check if he station is in line
        /// </summary>
        /// <param name="station">the station to check</param>
        /// <returns>true if the station is in line, false if the station is not in the line</returns>
        public bool isStationInLine(BusStationLine station)
        {
            foreach (var stationLine in Stations)
                if (stationLine.GetBusStationNumber == station.GetBusStationNumber)
                    return true;

            return false;
        }

        /// <summary>
        /// get the distance between 2 station, the caculation is by pitagoras sentence
        /// </summary>
        /// <param name="busStopA">first station</param>
        /// <param name="busStopB">second station</param>
        /// <returns>return the distance between two stations</returns>
        public double GetDistance(BusStationLine busStopA, BusStationLine busStopB)
        {
            return Math.Sqrt(Math.Pow(busStopA.GetLatitude - busStopB.GetLatitude, 2) +
                Math.Pow(busStopA.GetLongitude - busStopB.GetLongitude, 2)) * 400;
        }

        /// <summary>
        /// compute the tavel time between 2 statipn by the distance divide the avavrage speed
        /// </summary>
        /// <param name="busStopA">first station</param>
        /// <param name="busStopB">last station</param>
        /// <returns>return the travel time</returns>
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
                // after the first station found caculate the distance
                if (flag)
                {
                    TravelTime += station.TimeFromLastStation;
                }
                if (station == busStopB) // if is calculated the distance to the scond station
                    break;
            }
            return TravelTime;
        }

        /// <summary>
        /// build and retun line that is sub line
        /// </summary>
        /// <param name="first">first stop in the sub line</param>
        /// <param name="last">last stop in the sub line</param>
        /// <returns>return new line that is sub line with same numbe line</returns>
        public BusLine SubLine(BusStation first, BusStation last)
        {
            BusLine subLine = new BusLine(busLine, first, last, area);

            bool exist = false;
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i].GetBusStationNumber == first.BusStationNumber)
                {
                    exist = true;
                    subLine.addS(Stations[i]);
                    subLine.stations[0].DistanceFromLastStation = 0;
                    subLine.stations[0].TimeFromLastStation = new TimeSpan();
                    for (++i; i < Stations.Count; ++i)
                    {
                        subLine.addS(Stations[i]);
                        if (Stations[i].GetBusStationNumber == last.BusStationNumber)
                            return subLine;
                    }
                }
            }
            if (!exist)
                Console.WriteLine("the first bus station not exist in this line");
            else
                Console.WriteLine("the last station not exist from " +
                    "staion : {0} in this line", first.BusStationNumber);
            return null;
        }

        /// <summary>
        /// add station 
        /// </summary>
        /// <param name="station"></param>
        private void addS(BusStationLine station)
        {
            Stations.Add(station);
        }

        /// <summary>
        /// for IComperable compare the travel time between 2 station of 2 line wich travel time is shorter
        /// </summary>
        /// <param name="first">first station</param>
        /// <param name="last">last station</param>
        /// <param name="busLineA">line 1</param>
        /// <param name="busLineB">line 2</param>
        /// <returns>return int>0 if line A longer return int==0 if they
        /// are equal and int>0 if lineA shorter</returns>
        public int ComparTwoLines(BusStation first, BusStation last,
            BusLine busLineA, BusLine busLineB)
        {
            BusLine SubLineA = busLineA.SubLine(first, last);
            BusLine SubLineB = busLineB.SubLine(first, last);
            return SubLineA.CompareTo(SubLineB);
        }
        /// <summary>
        /// compare 2 lines witch one total travel time is longer
        /// </summary>
        /// <param name="obj">line b</param>
        /// <returns>/// <returns>return int>0 if line A longer return int==0 if they
        /// are equal and int>0 if lineA shorter</returns></returns>
        public int CompareTo(object obj)
        {
            return GetTravelTime(firstStation, lastStation).CompareTo(((BusLine)obj)
                .GetTravelTime(((BusLine)obj).firstStation, ((BusLine)obj).lastStation));
        }

    }
}

[System.Serializable]
public class MinimumStationsException : Exception
{
    public MinimumStationsException() { }
    public MinimumStationsException(string message) : base(message) { }
    public MinimumStationsException(string message, Exception inner) : base(message, inner) { }
    protected MinimumStationsException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
