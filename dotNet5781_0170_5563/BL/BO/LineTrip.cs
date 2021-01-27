using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class LineTrip:Line,IComparable<LineTrip>
    {
        //public int LineId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan Frequency { get; set; }
        public TimeSpan EndTime { get; set; }

        public int CompareTo(LineTrip other)
        {
            return StartTime.TotalSeconds.CompareTo(other.StartTime.TotalSeconds);
        }
    }
}
