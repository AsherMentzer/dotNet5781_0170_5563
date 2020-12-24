using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class StationLine : IClonable
    {
        public int LineId { get; set; }
        public int StationId { get; set; }
        public int NumInLine { get; set; }
    }
}
