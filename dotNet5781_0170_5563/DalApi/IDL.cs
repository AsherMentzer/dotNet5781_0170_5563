using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DO;

namespace DLAPI
{
    //CRUD Logic:
    // Create - add new instance
    // Request - ask for an instance or for a collection
    // Update - update properties of an instance
    // Delete - delete an instance
    public interface IDL
    {
        #region Bus
        IEnumerable<Bus> GetAllBuses();
        IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate);
        Bus GetBus(string licenseId);
        void AddBus(Bus bus);
        void UpdateBus(Bus bus);
        void UpdateBus(string licenceId, Action<Bus> update); //method that knows to updt specific fields in bus
        void DeleteBus(string licenceId);
        #endregion

        #region Line
        IEnumerable<Line> GetAllBusLines();
        IEnumerable<Line> GetAllBusLinesBy(Predicate<Line> predicate);
        Line GetBusLine(int lineId);
        void AddBusLine(Line busLine);
        void UpdateBusLine(Line busLine);
        void UpdateBusLine(int lineId, Action<Line> update); //method that knows to updt specific fields in bus line
        void DeleteBusLine(int lineId);
        #endregion

        #region LineExist
        IEnumerable<LineTrip> GetAllExistsLines();
        IEnumerable<LineTrip> GetAllExistsLinesBy(Predicate<LineTrip> predicate);
        LineTrip GetLineExist(int lineId);
        void AddLineExist(LineTrip lineExist);
        void UpdateLineExist(LineTrip lineExist);
        void UpdateLineExist(int lineId, Action<Line> update); //method that knows to updt specific fields in bus line
        void DeleteLineExist(int lineId);
        #endregion

        #region PairOfConsecutiveStation
        IEnumerable<AdjacentStations> GetAllPairs();
        IEnumerable<AdjacentStations> GetAllPairsBy(Predicate<Station> predicate);
        AdjacentStations GetPair(int id1,int id2);
        void AddPair(int id1, int id2, double distance, TimeSpan time);
        void UpdatePair(AdjacentStations pair);
        void UpdatePair(int id, Action<AdjacentStations> update); //method that knows to updt specific fields in Person
        void DeletePair(int id1,int id2);
        #endregion

        #region Station
        IEnumerable<Station> GetAllStations();
        IEnumerable<Station> GetAllStationsBy(Predicate<Station> predicate);
        Station GetStation(int id);
        void AddStation(Station station);
        void UpdateStation(Station station);
        void UpdateStation(int id, Action<Station> update); //method that knows to updt specific fields in Person
        void DeleteStation(int id);
        #endregion

        #region StationLine
        IEnumerable<StationLine> GetAllStationsLine();
        IEnumerable<StationLine> GetAllStationsLineBy(Predicate<StationLine> predicate);
        StationLine GetStationLine(int lineId,int stationId);
        StationLine GetStationLineBy(int lineId,int numInLine);

        void AddStationLine(StationLine stationLine);
        void UpdateStationLine(StationLine stationLine);
        void UpdateStationLine(int id, Action<StationLine> update); //method that knows to updt specific fields in Person
        void DeleteStationLine(int id,int sId);
        #endregion

        #region TravelBus
        IEnumerable<BusOnTrip> GetAllTravelBuses();
        IEnumerable<BusOnTrip> GetAllTravelBusesLineBy(Predicate<BusOnTrip> predicate);
        Station GetTravelBus(int id);
        void AddTravelBus(BusOnTrip travelBus);
        void UpdateTravelBus(BusOnTrip travelBus);
        void UpdateTravelBus(int id, Action<BusOnTrip> update); //method that knows to updt specific fields in Person
        void DeleteTravelBus(int id);
        #endregion
    }
}
