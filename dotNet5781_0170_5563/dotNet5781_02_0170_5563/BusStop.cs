using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
    /*public class Location
    {
        static Random r = new Random(DateTime.Now.Millisecond);
        private float latitude;
        private float longitude;
        public Location()
        {
            latitude = (float)((r.NextDouble() * 2.3) + 31);
            longitude = (float)((r.NextDouble() * 1.2) + 34.3);
        }
        public Location(float _latitude, float _longitude)
        {
            latitude = _latitude;
            longitude = _longitude;
        }
        public float GetLatitude { get => latitude; }
        public float GetLongitude { get => longitude; }
    }*/

    public class BusStop
    {
        static Random r = new Random(DateTime.Now.Millisecond);
        private int busStopNumber;
        // private Location busStopLocation = new Location();
        private float latitude;
        private float longitude;
        private string busStopAddress;

        public BusStop(int _busStopNumber, float _latitude, float _longitude, string _busStopAddress=null)
        {
            busStopNumber = _busStopNumber;
            latitude = _latitude;
            longitude = _longitude;
            busStopAddress = _busStopAddress;
        }
       
        public int BusStopNumber { get => busStopNumber; }
        public float Latitude { get => latitude; }
        public float Longitude { get => longitude; }
        public string BusStopAddress { get => busStopAddress; }
        override public string ToString()
        {
            return $"Bus Station Code: { busStopNumber}, {latitude}\u00b0N { longitude}\u00b0E";
        }

        
    }
}
