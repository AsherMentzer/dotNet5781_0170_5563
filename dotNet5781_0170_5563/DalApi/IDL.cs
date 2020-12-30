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

        #region BusLine
        IEnumerable<BusLine> GetAllBusLines();
        IEnumerable<BusLine> GetAllBusLinesBy(Predicate<BusLine> predicate);
        BusLine GetBusLine(int lineId);
        void AddBusLine(BusLine busLine);
        void UpdateBusLine(BusLine busLine);
        void UpdateBusLine(int lineId, Action<BusLine> update); //method that knows to updt specific fields in bus line
        void DeleteBusLine(int lineId);
        #endregion

        #region LineExist
        IEnumerable<LineExist> GetAllExistsLines();
        IEnumerable<LineExist> GetAllExistsLinesBy(Predicate<LineExist> predicate);
        LineExist GetLineExist(int lineId);
        void AddLineExist(LineExist lineExist);
        void UpdateLineExist(LineExist lineExist);
        void UpdateLineExist(int lineId, Action<BusLine> update); //method that knows to updt specific fields in bus line
        void DeleteLineExist(int lineId);
        #endregion

        #region PairOfConsecutiveStation
        IEnumerable<PairOfConsecutiveStation> GetAllPairs();
        IEnumerable<PairOfConsecutiveStation> GetAllPairsBy(Predicate<Station> predicate);
        PairOfConsecutiveStation GetPair(int id1,int id2);
        void AddPair(int id1, int id2, double distance, TimeSpan time);
        void UpdatePair(PairOfConsecutiveStation pair);
        void UpdatePair(int id, Action<PairOfConsecutiveStation> update); //method that knows to updt specific fields in Person
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
        Station GetStationLine(int id);
        void AddStationLine(StationLine stationLine);
        void UpdateStationLine(StationLine stationLine);
        void UpdateStationLine(int id, Action<StationLine> update); //method that knows to updt specific fields in Person
        void DeleteStationLine(int id);
        #endregion

        #region TravelBus
        IEnumerable<TravelBus> GetAllTravelBuses();
        IEnumerable<TravelBus> GetAllTravelBusesLineBy(Predicate<TravelBus> predicate);
        Station GetTravelBus(int id);
        void AddTravelBus(TravelBus travelBus);
        void UpdateTravelBus(TravelBus travelBus);
        void UpdateTravelBus(int id, Action<TravelBus> update); //method that knows to updt specific fields in Person
        void DeleteTravelBus(int id);
        #endregion
    }
}
