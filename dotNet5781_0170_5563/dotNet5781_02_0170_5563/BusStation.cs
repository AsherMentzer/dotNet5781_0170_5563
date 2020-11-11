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

    public class BusStation
    {
        static Random r = new Random(DateTime.Now.Millisecond);
        private int busStationNumber;
        private float latitude;
        private float longitude;
        private string busStationAddress;

        public BusStation(int _busStationNumber, float _latitude, float _longitude, string _busStationAddress=null)
        {
            busStationNumber = _busStationNumber;
            latitude = _latitude;
            longitude = _longitude;
            busStationAddress = _busStationAddress;
        }
        public BusStation(int _busStationNumber, string _busStationAddress = null)
        {
            busStationNumber = _busStationNumber;
            latitude = (float)((r.NextDouble() * 2.3) + 31);
            longitude = (float)((r.NextDouble() * 1.2) + 34.3);
            busStationAddress = _busStationAddress;
        }
      //properties
        public int BusStationNumber { get => busStationNumber; }
        public float Latitude { get => latitude; }
        public float Longitude { get => longitude; }
        public string BusStationAddress { get => busStationAddress; }
        override public string ToString()
        {
            return $"Bus Station Code: { busStationNumber}, {latitude}\u00b0N { longitude}\u00b0E";
        }

        
    }
}
