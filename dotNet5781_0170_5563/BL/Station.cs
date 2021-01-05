using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Station
    {
        public int StationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string StationName { get; set; }
        public IEnumerable<BusLine> lines { get; set; }

        public override string ToString()
        {
            string str = "ststion id: " + StationId + ", " + "latitude: " + Latitude + ", " + "longitude: " + Longitude +
                ", " + "station name: " + StationName + ":\n     ";
            foreach (var b in lines)
                str += b.LineNumber + ", ";
            return str;
        }
    }

}
