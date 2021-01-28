using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// class with only properties
    /// for defenition of station in line 
    /// </summary>
    public class StationLine 
    {
        public int LineId { get; set; }
        public int StationId { get; set; }
        public int NumInLine { get; set; }
    }
}
