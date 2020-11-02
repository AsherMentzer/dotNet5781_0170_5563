using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
    class Location
    {
        static Random r = new Random(DateTime.Now.Millisecond);
        private float latitude;
        private float longitude;
        public Location()
        {
            latitude = (float)((r.NextDouble() * 2.3) + 31);
            longitude =(float)((r.NextDouble() * 1.2) + 34.3);
        }
        public Location(float _latitude, float _longitude)
        {
            latitude = _latitude;
            longitude = _longitude;
        }
        public float GetLatitude { get => latitude; }
        public float GetLongitude { get => longitude; }
    }

    class BusStop
    {
        private int busStopNumber;
        private Location busStopLocation=new Location();
        private string busStopAddress;
         override public string ToString ()
        {
            string busStopDetails = "Bus Station Code: " + busStopNumber + ", " +
               busStopLocation.GetLatitude + "\u00b0N " + busStopLocation.GetLongitude + "\u00b0E" ;
            return busStopDetails;
        }
    }
}
