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
        //#region Bus
        //IEnumerable<Bus> GetAllBuses();
        //IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate);
        //Bus GetBus(string licenseId);
        //void AddBus(Bus bus);
        //void UpdateBus(Bus bus);
        //void UpdateBus(string licenceId, Action<Bus> update); //method that knows to updt specific fields in bus
        //void DeleteBus(string licenceId);
        //#endregion

        #region Line
        /// <summary>
        /// func to get all the lines from the data bale
        /// </summary>
        /// <returns>IEnumerable<Line></returns>
        IEnumerable<Line> GetAllBusLines();
        /// <summary>
        /// func to get all the lines by condition
        /// </summary>
        /// <param name="predicate">the condition to add the line to the collection</param>
        /// <returns>IEnumerable<Line></returns>
        IEnumerable<Line> GetAllBusLinesBy(Predicate<Line> predicate);
        /// <summary>
        /// func to get specific line from the data base
        /// </summary>
        /// <param name="lineId">the id of the line</param>
        /// <returns>Line</returns>
        Line GetBusLine(int lineId);
        /// <summary>
        /// func to add line to the data base
        /// </summary>
        /// <param name="busLine">the line to add</param>
        void AddBusLine(Line busLine);
        /// <summary>
        /// func to update the details of the line
        /// </summary>
        /// <param name="busLine">the updated line</param>
        void UpdateBusLine(Line busLine);
        /// <summary>
        /// func to delete line from the data base
        /// </summary>
        /// <param name="lineId">the id of the line to delete</param>
        void DeleteBusLine(int lineId);
        #endregion

        #region LineTrip
        /// <summary>
        /// func to get all the line trip from the data base
        /// </summary>
        /// <returns>IEnumerable<LineTrip></returns>
        IEnumerable<LineTrip> GetAllLinesTrip();
        /// <summary>
        /// func to get all the lines trip by condition
        /// </summary>
        /// <param name="predicate">the condition to chosse the line trip</param>
        /// <returns>IEnumerable<LineTrip></returns>
        IEnumerable<LineTrip> GetAllLinesTripBy(Predicate<LineTrip> predicate);
        /// <summary>
        /// func to get  specific line
        /// </summary>
        /// <param name="lineId">the line id</param>
        /// <param name="time">the start time</param>
        /// <returns></returns>
        LineTrip GetLineTrip(int lineId, TimeSpan time);
        /// <summary>
        /// func to add line trip
        /// </summary>
        /// <param name="lineTrip">the line trip to add</param>
        void AddLineTrip(LineTrip lineTrip);
        /// <summary>
        /// func to delete line trip
        /// </summary>
        /// <param name="lineId">the line id</param>
        /// <param name="time">the start time</param>
        void DeleteLineTrip(int lineId, TimeSpan time);
        #endregion

        #region PairOfConsecutiveStation
        /// <summary>
        /// func to get all the AdjacentStations
        /// </summary>
        /// <returns>IEnumerable<DO.AdjacentStations></returns>
        IEnumerable<AdjacentStations> GetAllPairs();
        /// <summary>
        /// func to get all the AdjacentStations by condition
        /// </summary>
        /// <param name="predicate">the condition</param>
        /// <returns>IEnumerable<DO.AdjacentStations></returns>
        IEnumerable<AdjacentStations> GetAllPairsBy(Predicate<AdjacentStations> predicate);
        /// <summary>
        /// func to get specific AdjacentStations
        /// </summary>
        /// <param name="id1">the exit station id</param>
        /// <param name="id2">the destination station id</param>
        /// <returns>DO.AdjacentStations</returns>
        AdjacentStations GetPair(int id1, int id2);
        /// <summary>
        /// func to add new AdjacentStations with all the details
        /// </summary>
        /// <param name="id1">the exit station id</param>
        /// <param name="id2">the destination station id</param>
        /// <param name="distance">the distance between them</param>
        /// <param name="time">the average travel time between them</param>
        void AddPair(int id1, int id2, double distance, TimeSpan time);
        /// <summary>
        /// func to update the details of AdjacentStations
        /// </summary>
        /// <param name="pair">the updated AdjacentStations</param>
        void UpdatePair(AdjacentStations pair);
        /// <summary>
        /// func to delete specific AdjacentStations
        /// </summary>
        /// <param name="id1">the exit station id</param>
        /// <param name="id2">the destination station id</param>
        void DeletePair(int id1, int id2);
        #endregion

        #region Station
        /// <summary>
        /// func to get all the station from the data base
        /// </summary>
        /// <returns>IEnumerable<DO.Station></returns>
        IEnumerable<Station> GetAllStations();
        /// <summary>
        /// func to get all the station from the data base by condition
        /// </summary>
        /// <param name="predicate">the condition</param>
        /// <returns>IEnumerable<DO.Station></returns>
        IEnumerable<Station> GetAllStationsBy(Predicate<Station> predicate);
        /// <summary>
        /// func to get specific ststion
        /// </summary>
        /// <param name="id">the id of the station</param>
        /// <returns>DO.Station</returns>
        Station GetStation(int id);
        /// <summary>
        /// func to add Station to the data base
        /// </summary>
        /// <param name="station">the station to add</param>
        void AddStation(Station station);
        /// <summary>
        /// func to updtae the details of the station
        /// </summary>
        /// <param name="station">the updated station</param>
        void UpdateStation(Station station);
        /// <summary>
        /// func to delete specific station
        /// </summary>
        /// <param name="id">the id of the station to delete</param>
        void DeleteStation(int id);
        #endregion

        #region StationLine
        /// <summary>
        /// Func to get all the stations line from the data base
        /// </summary>
        /// <returns>IEnumerable<DO.StationLine></returns>
        IEnumerable<StationLine> GetAllStationsLine();
        /// <summary>
        /// Func to get all the stations line from the data base by condition
        /// </summary>
        /// <param name="predicate">the condition</param>
        /// <returns>IEnumerable<DO.StationLine></returns>
        IEnumerable<StationLine> GetAllStationsLineBy(Predicate<StationLine> predicate);
        /// <summary>
        /// func to get specific station line
        /// </summary>
        /// <param name="lineId">the line id</param>
        /// <param name="stationId">the station id</param>
        /// <returns>DO.StationLine</returns>
        StationLine GetStationLine(int lineId, int stationId);
        /// <summary>
        /// func to get specifc station line
        /// </summary>
        /// <param name="lineId">the line id</param>
        /// <param name="numInLine">the number of the station in the line</param>
        /// <returns>StationLine</returns>
        StationLine GetStationLineBy(int lineId, int numInLine);
        /// <summary>
        /// func to add station line to the data base
        /// </summary>
        /// <param name="stationLine">the station line to add</param>
        void AddStationLine(StationLine stationLine);
        /// <summary>
        /// func to update the details of the station line
        /// </summary>
        /// <param name="stationLine">the updated station line</param>
        void UpdateStationLine(StationLine stationLine);
        /// <summary>
        /// func to delete station line
        /// </summary>
        /// <param name="id">the line id</param>
        /// <param name="sId">the station id</param>
        void DeleteStationLine(int id, int sId);
        #endregion

        #region User
        /// <summary>
        /// func to get the details of the user from the data base
        /// </summary>
        /// <param name="userName">the user name</param>
        /// <returns>User</returns>
        User GetUser(string userName);
        /// <summary>
        /// func to add user name to the data base
        /// </summary>
        /// <param name="user">the user to add</param>
        void AddUser(User user);
        #endregion
    }
}
