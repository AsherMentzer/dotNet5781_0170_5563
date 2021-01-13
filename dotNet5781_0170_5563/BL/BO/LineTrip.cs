using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class LineTrip:Line
    {
        //public int LineId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan Frequency { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
