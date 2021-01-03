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
       // public IEnumerable<StationLine> BusLines { get; set; }

    }
}
