using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
  public  class TravelBus
    {
        public int TravleId { get; set; }
        public string LicenseId { get; set; }
        public int LineId { get; set; }
        public TimeSpan LineTimwe { get; set; }
        public TimeSpan StartTime { get; set; }
        public int LastStation { get; set; }
        public TimeSpan TimeLastStation { get; set; }
        public TimeSpan TimeToNextStation { get; set; }
    }
}
