using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// class that create watch to update the presention time by the selected rate
    /// </summary>
    public class WatchSimulator
    {
        #region singelton
        static readonly WatchSimulator instance = new WatchSimulator();
        static WatchSimulator() { }// static ctor to ensure instance init is done just before first usage
        WatchSimulator() { } // default => private
        public static WatchSimulator Instance { get => instance; }// The public Instance property to use
        #endregion
        internal class Watch
        {
            internal TimeSpan Time;
            internal Watch(TimeSpan time) => Time = time;
        }
        private volatile Watch simulator = null;
        internal Watch Simulator { get => simulator; private set => simulator = value; }
        internal volatile bool cancel;
        private int rate;
        private TimeSpan curTime;
        internal TimeSpan CurTime { get { curTime = new TimeSpan(stopwatch.ElapsedTicks) + startTime; return curTime; } }
        internal int Rate { get => rate; private set => rate = value; }
        internal TimeSpan startTime;
        private Stopwatch stopwatch = new Stopwatch();
        private event Action<TimeSpan> observer = null;
        /// <summary>
        /// only 1 func can register to be observer
        /// </summary>
        internal event Action<TimeSpan> Observer
        {
            add { observer = value; }
            remove { observer = null; }
        }
        internal void StartWatch(TimeSpan startTime, int rate)
        {
            this.startTime = startTime;
            this.rate = rate;
            Simulator = new Watch(startTime);
            cancel = false;
            stopwatch.Restart();
            new Thread(() =>
            {
                //update the time in the pl overy 1/10  second by the observ func
                //until the user will cancel the simulator
                while (!cancel)
                {
                    var watch = new Watch(startTime + new TimeSpan(stopwatch.ElapsedTicks * rate));
                    observer(new TimeSpan(watch.Time.Hours, watch.Time.Minutes, watch.Time.Seconds));
                    Thread.Sleep(100);
                }
            }).Start();
            TripsOperator.Instance.startPanel();
        }


    }
}






 
