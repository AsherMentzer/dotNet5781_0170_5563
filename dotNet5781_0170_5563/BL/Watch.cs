﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BL
{
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
        internal int Rate { get => rate; private set => rate = value; }
        internal TimeSpan startTime;
        private Stopwatch stopwatch = new Stopwatch();
        private event Action<TimeSpan> observer = null;
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
                while (!cancel)
                {
                    var watch = new Watch(startTime + new TimeSpan(stopwatch.ElapsedTicks * rate));
                    observer(new TimeSpan(watch.Time.Hours, watch.Time.Minutes, watch.Time.Seconds));
                    Thread.Sleep(100);
                }
            }).Start();
        }


    }








      //  prev version
    //    private event EventHandler timeChange;
    //    public event EventHandler TimeChange
    //    {
    //        add { timeChange = value; }
    //        remove { timeChange -= value; }
    //    }
    //    protected void OnTimeChanged(TimeChangedEventArgs args)
    //    {
    //        if (timeChange != null)
    //        {
    //            timeChange(this, args);
    //        }
    //    }

    //    public TimeSpan Time
    //    {
    //        get { return time; }

    //        set
    //        {
    //            TimeChangedEventArgs args = new TimeChangedEventArgs(time, value);
    //            time = value;
    //            OnTimeChanged(args);
    //        }
    //    }
    //}

    //public class TimeChangedEventArgs : EventArgs
    //{
    //    public readonly TimeSpan Oldtime, Newtime;

    //    public TimeChangedEventArgs(TimeSpan oldTemp, TimeSpan newTemp)
    //    {
    //        Oldtime = oldTemp;
    //        Newtime = newTemp;
    //    }
    //}
}

