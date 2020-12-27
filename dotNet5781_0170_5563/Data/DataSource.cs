using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  DO;

namespace Data
{
    public static class DataSource
    {
        public static List<Bus> buses;
        public static List<Station> stations;
        public static List<BusLine> lines;
        public static List<PairOfConsecutiveStation> pairs;
        public static List<StationLine> stationsLine;
        public static List<TravelBus> travelBuses;
        public static List<LineExist> linesExists;
        static DataSource()
        {
            InialAllList();
        }
        static void InialAllList()
        {

        }
    }
}
