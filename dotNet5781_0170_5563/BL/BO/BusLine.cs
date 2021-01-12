using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public enum Areas { General = 1, North, West, Center, Jerusalem };
    public class BusLine
    {
        public int LineId { get; set; }
        public int LineNumber { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
        public Areas area { get; set; }
        public IEnumerable<StationLine> Stations { get; set; }
        public override string ToString()
        {
            string str = "Line ID:" + LineId + ",  LineNumber:" + LineNumber + ", first station:" + FirstStation +
                ", last station:" + LastStation + ", Area:" + area+"\n";
            foreach (var s in Stations)
                str += "station id:" + s.StationId + ", num in line:" + s.NumInLine + "time to next" + s.AverageTravleTime;
            return str;
        }
    }
}
