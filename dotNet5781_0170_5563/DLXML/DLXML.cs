using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;

namespace DL
{
    sealed class DLXML : IDL    //internal
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files
        string BusPath = @"BusXml.xml"; //XElement
        string LinePath = @"LineXml.xml"; //XElement
        string StationPath = @"StationXml.xml"; //XElement
        string UserPath = @"UserXml.xml"; //XElement
        string BusOnTripPath = @"BusOnTripXml.xml"; //XElement
        string AdjacentStationsPath = @"AdjacentStationsXml.xml"; //XElement
        string LineTripPath = @"LineTripXml.xml"; //XElement
        string TripPath = @"TripXml.xml"; //XElement
        string LineStationPath = @"LineStationXml.xml"; //XElement
        #endregion


        #region Bus
        public IEnumerable<Bus> GetAllBuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public Bus GetBus(string licenseId)
        {
            throw new NotImplementedException();
        }

        public void AddBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(string licenceId, Action<Bus> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(string licenceId)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Line
        public IEnumerable<Line> GetAllBusLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> GetAllBusLinesBy(Predicate<Line> predicate)
        {
            throw new NotImplementedException();
        }

        public Line GetBusLine(int lineId)
        {
            throw new NotImplementedException();
        }

        public void AddBusLine(Line busLine)
        {
            throw new NotImplementedException();
        }

        public void UpdateBusLine(Line busLine)
        {
            throw new NotImplementedException();
        }

        public void UpdateBusLine(int lineId, Action<Line> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBusLine(int lineId)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region LineTrip
        public IEnumerable<LineTrip> GetAllExistsLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineTrip> GetAllExistsLinesBy(Predicate<LineTrip> predicate)
        {
            throw new NotImplementedException();
        }

        public LineTrip GetLineExist(int lineId)
        {
            throw new NotImplementedException();
        }

        public void AddLineExist(LineTrip lineExist)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineExist(LineTrip lineExist)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineExist(int lineId, Action<Line> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineExist(int lineId)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region AdjacentStations     
        public IEnumerable<AdjacentStations> GetAllPairs()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AdjacentStations> GetAllPairsBy(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        public AdjacentStations GetPair(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public void AddPair(int id1, int id2, double distance, TimeSpan time)
        {
            throw new NotImplementedException();
        }

        public void UpdatePair(AdjacentStations pair)
        {
            throw new NotImplementedException();
        }

        public void UpdatePair(int id, Action<AdjacentStations> update)
        {
            throw new NotImplementedException();
        }

        public void DeletePair(int id1, int id2)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Station
        public IEnumerable<Station> GetAllStations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetAllStationsBy(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int id)
        {
            throw new NotImplementedException();
        }

        public void AddStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(int id, Action<Station> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region StationLine
        public IEnumerable<StationLine> GetAllStationsLine()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StationLine> GetAllStationsLineBy(Predicate<StationLine> predicate)
        {
            throw new NotImplementedException();
        }

        public StationLine GetStationLine(int lineId, int stationId)
        {
            throw new NotImplementedException();
        }

        public StationLine GetStationLineBy(int lineId, int numInLine)
        {
            throw new NotImplementedException();
        }

        public void AddStationLine(StationLine stationLine)
        {
            throw new NotImplementedException();
        }

        public void UpdateStationLine(StationLine stationLine)
        {
            throw new NotImplementedException();
        }

        public void UpdateStationLine(int id, Action<StationLine> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteStationLine(int id, int sId)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region BusOnTrip
        public IEnumerable<BusOnTrip> GetAllTravelBuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BusOnTrip> GetAllTravelBusesLineBy(Predicate<BusOnTrip> predicate)
        {
            throw new NotImplementedException();
        }

        public Station GetTravelBus(int id)
        {
            throw new NotImplementedException();
        }

        public void AddTravelBus(BusOnTrip travelBus)
        {
            throw new NotImplementedException();
        }

        public void UpdateTravelBus(BusOnTrip travelBus)
        {
            throw new NotImplementedException();
        }

        public void UpdateTravelBus(int id, Action<BusOnTrip> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteTravelBus(int id)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
