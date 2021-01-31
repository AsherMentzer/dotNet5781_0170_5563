using DLAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BO;
namespace BL
{
    /// <summary>
    /// class that start to operate all the lines that we have 
    /// and if we get station id show all the arriving lines 
    /// and update the arrival time every second
    /// </summary>
    public class TripsOperator
    {
        #region singelton
        static readonly TripsOperator instance = new TripsOperator();
        static TripsOperator() { }// static ctor to ensure instance init is done just before first usage
        TripsOperator() { } // default => private
        public static TripsOperator Instance { get => instance; }// The public Instance property to use
        #endregion
        Random rand = new Random();
        BLImp bl = new BLImp();
        IDL dl = DLFactory.GetDL();
        internal volatile bool cancel;
        private BO.LineTiming lineTiming;
        internal int stationId = -1;
        private event Action<BO.LineTiming> observer;
        //add only 1 func to be ocserver
        internal event Action<BO.LineTiming> Observer
        {
            add { observer = value; }
            remove { observer -= value; }
        }
       //get all the details that needed to operate all the lines
        List<BO.Line> lines = new List<Line>();
        List<BO.Station> LastStations = new List<Station>();
        List<List<StationLine>> allStations = new List<List<StationLine>>();
        internal void startPanel()
        {
            new Thread(() =>
                {
                    List<DO.LineTrip> allTrips = (from item in dl.GetAllLinesTrip()
                                                  orderby item.StartTime
                                                  select item).ToList();

                    List<BO.LineTrip> trips = new List<LineTrip>();
                    foreach (var line in allTrips)
                    {
                        for (TimeSpan time = line.StartTime; time < line.EndTime; time += line.Frequency)
                        {

                            BO.LineTrip trip = new LineTrip { LineId = line.LineId, StartTime = time };
                            trips.Add(trip);
                        }
                    }
                    trips.Sort((x, y) => (int)(x.StartTime - y.StartTime).TotalMilliseconds);

                    lines = (from item in allTrips
                             select bl.GetBusLine(item.LineId)).ToList();
                            
                    
                    foreach(var item in lines)
                    {
                        lines.Distinct();
                    }
                    foreach(var item in lines)
                    {
                        BO.Station st= bl.GetStation(item.LastStation);
                        List<BO.StationLine> stations = item.Stations.ToList();
                        LastStations.Add(st);
                        allStations.Add(stations);
                    }
                    int i = 0;
                    if (WatchSimulator.Instance.CurTime > trips[0].StartTime)
                    {
                        while (WatchSimulator.Instance.CurTime > trips[i].StartTime)
                        {
                            if (i < trips.Count - 1)
                                i++;
                            else
                                break;
                        }
                        Thread.Sleep((int)(trips[i].StartTime.TotalMilliseconds - WatchSimulator.Instance.CurTime.TotalMilliseconds) / WatchSimulator.Instance.Rate);
                    }
                    else
                        Thread.Sleep((int)(trips[i].StartTime.TotalMilliseconds - WatchSimulator.Instance.CurTime.TotalMilliseconds) / WatchSimulator.Instance.Rate);

                    for (; i < trips.Count; ++i)
                    {
                        if (WatchSimulator.Instance.cancel)
                            break;

                        new Thread(() =>
                        {
                            tripThread(trips[i]);
                        }).Start();
                        if (i < trips.Count - 1)
                            Thread.Sleep((int)(trips[i + 1].StartTime.TotalMilliseconds - trips[i].StartTime.TotalMilliseconds) / WatchSimulator.Instance.Rate);
                        else
                        {
                            Thread.Sleep((int)(trips[0].StartTime.TotalMilliseconds + new TimeSpan(23, 59, 59).TotalMilliseconds - trips[i].StartTime.TotalMilliseconds) / WatchSimulator.Instance.Rate);
                            i = -1;
                        }
                    }
                }).Start();

        }
        /// <summary>
        /// operate one trip in  new thread
        /// </summary>
        /// <param name="trip">the trip to operate</param>
        private void tripThread(BO.LineTrip trip)
        {
            //get all the details to Show in the panel
            BO.Line line = lines.FirstOrDefault(x=>x.LineId==trip.LineId);
            BO.Station st = LastStations.FirstOrDefault(x => x.StationId == line.LastStation);
            List<BO.StationLine> stations = allStations.FirstOrDefault(x => x.Any(y => y.LineId == trip.LineId));
            BO.LineTiming timing = new BO.LineTiming()
            {
                LineId = line.LineId,
                StartTime = trip.StartTime,
                LineNumber = line.LineNumber,
                LastStationName = st.StationName,
                ArriveTime = TimeSpan.Zero
            };
            Thread.CurrentThread.Name = $"{line.LineId},{line.LineNumber},{trip.StartTime}";

            TimeSpan time = TimeSpan.Zero;
            int id=-1;
            int j;
            //go for all the stations in the line
            for (int i = 0; i < stations.Count && !WatchSimulator.Instance.cancel; ++i)
            {
                if (stations[i].StationId != stationId)
                {
                    bool flag = false;
                    //each  station in the line clculate the time to arrive to any station
                    for (j = i; j < stations.Count && !WatchSimulator.Instance.cancel; ++j)
                    {
                        if (stations[j].StationId == stationId)
                        {
                            id = stations[j].StationId;
                            timing.ArriveTime = time;
                            flag = true;
                            break;
                        }
                        time += stations[j].AverageTravleTime;
                    }
                    //add random time to the travel from this station to the other by random number that
                    //multiply the average travel time by random number between  0.9-2    
                    if (i != stations.Count - 1 && flag && stations[j].StationId == stationId)
                    {
                        double d = rand.NextDouble();
                        d = (d * 1.1) + 0.9;
                        int temp = (int)(stations[i].AverageTravleTime.TotalMilliseconds * d / WatchSimulator.Instance.Rate);
                        timing.ArriveTime += new TimeSpan(0, 0, (int)(stations[i].AverageTravleTime.TotalSeconds * d - stations[i].AverageTravleTime.TotalSeconds));
                        if (!WatchSimulator.Instance.cancel)
                            observer(timing);
                        //update the time every second
                        for (int k = 0; k < temp && !WatchSimulator.Instance.cancel && id == stationId; k += 1000)
                        {
                            if (temp - k > 1000)
                            {
                                Thread.Sleep(1000);
                                timing.ArriveTime = new TimeSpan(0, 0, 0, 0, (int)(timing.ArriveTime.TotalMilliseconds - 1000 * WatchSimulator.Instance.Rate));
                                if (!WatchSimulator.Instance.cancel)
                                    observer(timing);
                            }
                            else
                                Thread.Sleep(temp - k);
                        }
                    }
                    time = TimeSpan.Zero;
                    if (id != stationId)
                    {
                        timing.ArriveTime = TimeSpan.Zero;
                        break;
                        observer(timing);
                    }
                }
                else
                {
                    timing.ArriveTime = TimeSpan.Zero;
                    if (!WatchSimulator.Instance.cancel)
                        observer(timing);
                    break;
                }


            }
        }
    }
}

