using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLAPI
{
    public interface IBL
    {

        #region Bus
        IEnumerable<BO.Bus> GetAllBuses();
        IEnumerable<BO.Bus> GetAllBusesBy(Predicate<BO.Bus> predicate);
        BO.Bus GetBus(string licenseId);
        void AddBus(BO.Bus bus);
        void UpdateBus(BO.Bus bus);
        void UpdateBus(string licenceId, Action<BO.Bus> update); //method that knows to updt specific fields in bus
        void DeleteBus(string licenceId);
        #endregion

        #region BusLine
        IEnumerable<BO.BusLine> GetAllBusLines();
        IEnumerable<BO.BusLine> GetAllBusLinesBy(Predicate<BO.BusLine> predicate);
        BO.BusLine GetBusLine(int lineId);
        void AddBusLine(BO.BusLine busLine);
        void UpdateBusLine(BO.BusLine busLine);
        void UpdateBusLine(int lineId, Action<BO.BusLine> update); //method that knows to updt specific fields in bus line
        void DeleteBusLine(int lineId);
        #endregion

        #region LineExist
        IEnumerable<BO.LineExist> GetAllExistsLines();
        IEnumerable<BO.LineExist> GetAllExistsLinesBy(Predicate<BO.LineExist> predicate);
        BO.LineExist GetLineExist(int lineId);
        void AddLineExist(BO.LineExist lineExist);
        void UpdateLineExist(BO.LineExist lineExist);
        void UpdateLineExist(int lineId, Action<BO.BusLine> update); //method that knows to updt specific fields in bus line
        void DeleteLineExist(int lineId);
        #endregion

        #region PairOfConsecutiveStation
        IEnumerable<BO.PairOfConsecutiveStation> GetAllPairs();
        IEnumerable<BO.PairOfConsecutiveStation> GetAllPairsBy(Predicate<BO.Station> predicate);
        BO.PairOfConsecutiveStation GetPair(int id1, int id2);
        void AddPair(int id1, int id2, double distance, TimeSpan time);
        void UpdatePair(BO.PairOfConsecutiveStation pair);
        void UpdatePair(int id, Action<BO.PairOfConsecutiveStation> update); //method that knows to updt specific fields in Person
        void DeletePair(int id1, int id2);
        #endregion

        #region Station
        IEnumerable<BO.Station> GetAllStations();
        IEnumerable<BO.Station> GetAllStationsBy(Predicate<BO.Station> predicate);
        BO.Station GetStation(int id);
        void AddStation(BO.Station station);
        void UpdateStation(BO.Station station);
        void UpdateStation(int id, Action<BO.Station> update); //method that knows to updt specific fields in Person
        void DeleteStation(int id);
        #endregion

        #region StationLine
        IEnumerable<BO.StationLine> GetAllStationsLine();
        IEnumerable<BO.StationLine> GetAllStationsLineBy(Predicate<BO.StationLine> predicate);
        BO.Station GetStationLine(int id);
        void AddStationLine(BO.StationLine stationLine);
        void UpdateStationLine(BO.StationLine stationLine);
        void UpdateStationLine(int id, Action<BO.StationLine> update); //method that knows to updt specific fields in Person
        void DeleteStationLine(int id);
        #endregion

        #region TravelBus
        IEnumerable<BO.TravelBus> GetAllTravelBuses();
        IEnumerable<BO.TravelBus> GetAllTravelBusesLineBy(Predicate<BO.TravelBus> predicate);
        BO.Station GetTravelBus(int id);
        void AddTravelBus(BO.TravelBus travelBus);
        void UpdateTravelBus(BO.TravelBus travelBus);
        void UpdateTravelBus(int id, Action<BO.TravelBus> update); //method that knows to updt specific fields in Person
        void DeleteTravelBus(int id);
        #endregion
    }
}
