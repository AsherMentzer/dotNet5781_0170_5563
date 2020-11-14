using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
    /// <summary>
    /// this class is for bus station and contain the number of the station
    /// the location and the address
    /// </summary>
    public class BusStation
    {
        static Random r = new Random(DateTime.Now.Millisecond);
        private int busStationNumber;
        private float latitude;
        private float longitude;
        private string busStationAddress;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="_busStationNumber">update the station number</param>
        /// <param name="_latitude">updtae the latitude</param>
        /// <param name="_longitude">update the longtitude</param>
        /// <param name="_busStationAddress">update the address</param>
        public BusStation(int _busStationNumber, float _latitude, float _longitude, string _busStationAddress = null)
        {
            busStationNumber = _busStationNumber;
            latitude = _latitude;
            longitude = _longitude;
            busStationAddress = _busStationAddress;
        }
        /// <summary>
        /// constructor that get only number and address and randomaly set the location
        /// </summary>
        /// <param name="_busStationNumber">station number</param>
        /// <param name="_busStationAddress">station addresss</param>
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
        /// <summary>
        /// override tostring fnc 
        /// </summary>
        /// <returns>all the deatails of the station</returns>
        override public string ToString()
        {
            return $"Bus Station Code: { busStationNumber}, {latitude}\u00b0N { longitude}\u00b0E";
        }


    }
}
