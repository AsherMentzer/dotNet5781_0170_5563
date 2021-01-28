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


        internal void startPanel()
        {
            new Thread(() =>
                {

                }).Start();
        }
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

