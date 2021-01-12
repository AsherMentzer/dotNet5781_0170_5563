using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class PairOfConsecutiveStation
    {
        public int StationId1 { get; set; }
        public int StationId2 { get; set; }
        public double Distance { get; set; }
        public TimeSpan AverageTravleTime { get; set; }
    }
}
