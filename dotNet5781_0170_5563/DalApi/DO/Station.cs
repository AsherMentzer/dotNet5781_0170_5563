using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// class with only properties
    /// for defenition of Station
    /// </summary>
    public class Station 
    {
        public int StationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string StationName { get; set; }
    }
}
