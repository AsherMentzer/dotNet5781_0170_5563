using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
    /// <summary>
    /// class for bus station line is logical station for that
    /// created for speciped line and contain all the details of the staion and
    /// the deatails for the line the time and the distance from last station
    /// </summary>
    public class BusStationLine
    {
        private int busStationNumber;
        private float latitude;
        private float longitude;
        private string busStationAddress;
        private double distanceFromLastStation;
        private TimeSpan timeFromLastStation=new TimeSpan();
        /// <summary>
        /// constructor who get station and update the details 
        /// for the bus station line
        /// </summary>
        /// <param name="station">real station</param>
        public BusStationLine(BusStation station)
        {
            busStationNumber = station.BusStationNumber;
            latitude = station.Latitude;
            longitude = station.Longitude;
            busStationAddress = station.BusStationAddress;
        }
        //properties
        public int GetBusStationNumber{ get => busStationNumber; }
        public float GetLatitude { get => latitude; }
        public float GetLongitude { get=> longitude; }
        public string GetBusStationAddress { get => busStationAddress; }
        public double DistanceFromLastStation { get => distanceFromLastStation;
            set => distanceFromLastStation = value; }
        public TimeSpan TimeFromLastStation { get => timeFromLastStation; set => timeFromLastStation = value; }
        override public string ToString()
        {
            return $"Bus Station Code: { busStationNumber}, {latitude}\u00b0N { longitude}\u00b0E"
              + $"  {timeFromLastStation.ToString(@"hh\:mm\:ss")} ";
        }

    }
}
