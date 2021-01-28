using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BL
{
    public class TripsOperator
    {
        #region singelton
        static readonly TripsOperator instance = new TripsOperator();
        static TripsOperator() { }// static ctor to ensure instance init is done just before first usage
        TripsOperator() { } // default => private
        public static TripsOperator Instance { get => instance; }// The public Instance property to use
        #endregion

        internal volatile bool cancel;
        private BO.LineTiming lineTiming;
        internal int stationId;
        private event Action<BO.LineTiming> observer;
        internal event Action<BO.LineTiming> Observer
        {
            add { observer += value; }
            remove { observer -= value; }
        }


        //internal void startPanel()
        //{
        //    new Thread(() =>
        //        {
        //            DO.LineTrip trip = (DO.LineTrip)e.Argument;
        //            BO.Line line = GetBusLine(trip.LineId);
        //            DO.Station station = dl.GetStation(line.LastStation);
        //            TimeSpan time = TimeSpan.Zero;
        //            List<BO.StationLine> st = line.Stations.ToList();
        //            BO.LineTiming TripBO = new BO.LineTiming()
        //            {
        //                StartTime = trip.StartTime,
        //                LineId = trip.LineId,
        //                LineNumber = line.LineNumber,
        //                LastStationName = station.StationName,
        //                ArriveTime = time
        //            };
        //            for (int i = 0; i < st.Count; ++i)
        //            {
        //                if (st[i].StationId == id)
        //                {
        //                    TripsOperator.Instance.LineTiming = TripBO;
        //                }
        //                TripBO.ArriveTime += st[i].AverageTravleTime;
        //            }
        //            int counter = TripBO.ArriveTime.Minutes;
        //            for (int i = 0; i < counter; ++i)
        //            {
        //                Thread.Sleep(3000);
        //                TripBO.ArriveTime -= new TimeSpan(0, 1, 0);
        //                TripsOperator.Instance.LineTiming = TripBO;
        //            }
        //            TripBO.ArriveTime = TimeSpan.Zero;
        //            TripsOperator.Instance.LineTiming = TripBO;
        //        for (int i = 0; i < st.Count; ++i)
        //        {
        //            for (int j = i; j < st.Count; ++j)
        //            {
        //                //if you reach to the selected station
        //                if (st[j].StationId == id)
        //                {
        //                    //for soft stop to this thread
        //                    //while (!watch.cancel)

        //                    TripsOperator.Instance.LineTiming = TripBO;
        //                    int min = (int)(st[i].AverageTravleTime.TotalMilliseconds * 0.9);
        //                    int max = (int)(st[i].AverageTravleTime.TotalMilliseconds * 2);
        //                    // Thread.Sleep(r.Next(min, max)/60*10);
        //                    Thread.Sleep((int)(st[i].AverageTravleTime.TotalMilliseconds / 20));
        //                    //break;

        //                    // break;
        //                }
        //                TripBO.ArriveTime += st[j].AverageTravleTime;

        //            }
        //            TripBO.ArriveTime = TimeSpan.Zero;
        //            if (st[i].StationId == id)
        //            {
        //                //TripBO.ArriveTime = TimeSpan.Zero;
        //                TripsOperator.Instance.LineTiming = TripBO;
        //                Thread th = Thread.CurrentThread;
        //                th.Abort();
        //                break;







        //            }).Start();
        //}
        //    protected void OnTimeChanged()
        //    {
        //        if (TimingChange != null)
        //        {
        //            TimingChange(this, EventArgs.Empty);
        //        }
        //    }

        //    public BO.LineTiming LineTiming
        //    {
        //        get { return lineTiming; }

        //        set
        //        {
        //            //TimeChangedEventArgs args = new TimeChangedEventArgs(lineTiming, value);
        //            lineTiming = value;
        //            OnTimeChanged();
        //        }
        //    }
        //}

        //public class TimeChangedEventArgs : EventArgs
        //{
        //    public readonly BO.LineTiming Oldtime;
        //    public readonly BO.LineTiming Newtime;
        //    //, Newtime;
        //    public TimeChangedEventArgs(BO.LineTiming oldTemp, BO.LineTiming newTemp)
        //    {
        //        Oldtime = oldTemp;
        //        Newtime = newTemp;
        //    }
        //}
    }
}

