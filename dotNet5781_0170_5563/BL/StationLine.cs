using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StationLine
    {
        public int LineId { get; set; }
        public int StationId { get; set; }
        public int NumInLine { get; set; }
        public double Distance { get; set; }
        public TimeSpan AverageTravleTime { get; set; }
        
    }
}
