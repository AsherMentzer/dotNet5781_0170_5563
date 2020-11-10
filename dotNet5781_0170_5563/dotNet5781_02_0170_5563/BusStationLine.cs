using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
    public class BusStationLine
    {
        //BusStation stop;
        private int busStationNumber;
        private float latitude;
        private float longitude;
        private string busStationAddress;
        private double distanceFromLastStation;
        private double timeFromLastStation;

        public BusStationLine(BusStation station)
        {
            busStationNumber = station.BusStationNumber;
            latitude = station.Latitude;
            longitude = station.Longitude;
            busStationAddress = station.BusStationAddress;
           // distanceFromLastStation = distance;
            //timeFromLastStation = time;
        }
        //properties
        public int GetBusStationNumber{ get => busStationNumber; }
        public float GetLatitude { get => latitude; }
        public float GetLongitude { get=> longitude; }
        public string GetBusStationAddress { get => busStationAddress; }
        public double DistanceFromLastStation { get => distanceFromLastStation;
            set => distanceFromLastStation = value; }
        public double TimeFromLastStation { get => timeFromLastStation; set => timeFromLastStation = value; }

    }
}
