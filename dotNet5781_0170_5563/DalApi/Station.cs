using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Station : IClonable
    {
        public int StationId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string StationName { get; set; }
    }
}
