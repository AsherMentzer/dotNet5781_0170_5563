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
        IEnumerable<BO.Line> GetAllBusLines();
        IEnumerable<BO.Line> GetAllBusLinesBy(Predicate<BO.Line> predicate);
        BO.Line GetBusLine(int lineId);
        BO.Line CreateBusLine(int LineNum, int fId, int lId, BO.Areas area);
        void AddBusLine(BO.Line busLine);
        void UpdateBusLine(BO.Line busLine);
        void UpdateBusLine(int lineId, Action<BO.Line> update); //method that knows to updt specific fields in bus line
        void DeleteBusLine(BO.Line line);
        #endregion

        #region LineExist
        IEnumerable<BO.LineTrip> GetAllExistsLines();
        IEnumerable<BO.LineTrip> GetAllExistsLinesBy(Predicate<BO.LineTrip> predicate);
        BO.LineTrip GetLineExist(int lineId);
        void AddLineExist(BO.LineTrip lineExist);
        void UpdateLineExist(BO.LineTrip lineExist);
        void UpdateLineExist(int lineId, Action<BO.Line> update); //method that knows to updt specific fields in bus line
        void DeleteLineExist(int lineId);
        #endregion

        #region PairOfConsecutiveStation
        IEnumerable<BO.AdjacentStations> GetAllPairs();
        IEnumerable<BO.AdjacentStations> GetAllPairsBy(Predicate<BO.Station> predicate);
        BO.AdjacentStations GetPair(int id1, int id2);
        void AddPair(int id1, int id2, double distance, TimeSpan time);
        void UpdatePair(BO.AdjacentStations pair);
        void UpdatePair(int id, Action<BO.AdjacentStations> update); //method that knows to updt specific fields in Person
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
        BO.StationLine GetStationLine(int Lineid, int stationId);
        void AddStationLine(int lineId, int stationId, int numInLine);
        // void AddStationLine(int lineId, int stationId, int numInLined, BO.PairOfConsecutiveStation p);
        void UpdateStationLine(BO.StationLine stationLine);
        void UpdateStationLine(int id, Action<BO.StationLine> update); //method that knows to updt specific fields in Person
        void DeleteStationLine(int id, int sId);
        #endregion

        #region TravelBus
        IEnumerable<BO.BusOnTrip> GetAllTravelBuses();
        IEnumerable<BO.BusOnTrip> GetAllTravelBusesLineBy(Predicate<BO.BusOnTrip> predicate);
        BO.Station GetTravelBus(int id);
        void AddTravelBus(BO.BusOnTrip travelBus);
        void UpdateTravelBus(BO.BusOnTrip travelBus);
        void UpdateTravelBus(int id, Action<BO.BusOnTrip> update); //method that knows to updt specific fields in Person
        void DeleteTravelBus(int id);
        #endregion
        #region User
        BO.User GetUser(string userName);
        void AddUser(string userName, string paaword);
        #endregion
        #region Simulator
        void StartSimulator(TimeSpan startTime, int Rate, Action<TimeSpan> updateTime);
        void StopSimulator();
        void SetStationPanel(int station, Action<BO.LineTiming> updateBus);
        #endregion
    }
}
