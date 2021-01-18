using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
  public  class Watch
    {
        #region singelton
        static readonly Watch instance = new Watch();
        static Watch() { }// static ctor to ensure instance init is done just before first usage
        Watch() { } // default => private
        public static Watch Instance { get => instance; }// The public Instance property to use
        #endregion
        internal volatile bool cancel;
        private TimeSpan time;
        private event EventHandler timeChange;
        public event EventHandler TimeChange
        {
            add { timeChange = value; }
            remove { timeChange -= value; }
        }
        protected void OnTimeChanged(TimeChangedEventArgs args)
        {
            if (timeChange != null)
            {
                timeChange(this, args);
            }
        }

        public TimeSpan Time
        {
            get { return time; }

            set
            {
                TimeChangedEventArgs args = new TimeChangedEventArgs(time, value);
                time = value;
                OnTimeChanged(args);
            }
        }
    }

    public class TimeChangedEventArgs : EventArgs
    {
        public readonly TimeSpan Oldtime, Newtime;

        public TimeChangedEventArgs(TimeSpan oldTemp, TimeSpan newTemp)
        {
            Oldtime = oldTemp;
            Newtime = newTemp;
        }
    }
}

