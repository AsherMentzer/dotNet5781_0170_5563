using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
   public class AdjacentStations
    {
        public int StationId1 { get; set; }
        public int StationId2 { get; set; }
        public double Distance { get; set; }
        public TimeSpan AverageTravleTime { get; set; }
    }
}
