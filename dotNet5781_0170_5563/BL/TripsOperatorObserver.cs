using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class TripsOperatorObserver
    {
        Action<BO.LineTiming> up;
        public TripsOperatorObserver(Action<BO.LineTiming> update)
        {
            up = update;
            TripsOperator.Instance.TimingChange += TimeChangeFunc;
        }
        void TimeChangeFunc(Object sender, EventArgs e)
        {
            //if (!(e is TimeChangedEventArgs))
            //    return;

            //TimeChangedEventArgs temp = (TimeChangedEventArgs)e;

            up(TripsOperator.Instance.LineTiming);
        }
    }
}
