using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLAPI
{
    public interface IBL
    {

        //#region Bus
        //IEnumerable<BO.Bus> GetAllBuses();
        //IEnumerable<BO.Bus> GetAllBusesBy(Predicate<BO.Bus> predicate);
        //BO.Bus GetBus(string licenseId);
        //void AddBus(BO.Bus bus);
        //void UpdateBus(BO.Bus bus);
        //void UpdateBus(string licenceId, Action<BO.Bus> update); //method that knows to updt specific fields in bus
        //void DeleteBus(string licenceId);
        //#endregion

        #region BusLine
        /// <summary>
        /// get all the lines from the data Base
        /// </summary>
        /// <returns> IEnumerable<BO.Line> all the lines</returns>
        IEnumerable<BO.Line> GetAllBusLines();
        /// <summary>
        /// get spesific line from the data base
        /// </summary>
        /// <param name="lineId">to search the line with this uniq id</param>
        /// <returns>BO.Line</returns>
        BO.Line GetBusLine(int lineId);
        /// <summary>
        /// func to create new bus line
        /// </summary>
        /// <param name="LineNum">the line number</param>
        /// <param name="fId">first station id</param>
        /// <param name="lId">last station id</param>
        /// <param name="area">the area of the line</param>
        /// <returns> BO.Line(the new line)</returns>
        BO.Line CreateBusLine(int LineNum, int fId, int lId, BO.Areas area);
        /// <summary>
        /// func to add line to the data base
        /// </summary>
        /// <param name="busLine">the line to add</param>
        void AddBusLine(BO.Line busLine);
        /// <summary>
        /// update line in the data base
        /// </summary>
        /// <param name="busLine">the updated line</param>
        void UpdateBusLine(BO.Line busLine);
        /// <summary>
        /// delete line from the data base
        /// </summary>
        /// <param name="line">the line to delete</param>
        void DeleteBusLine(BO.Line line);
        #endregion

        #region PairOfConsecutiveStation
        /// <summary>
        /// func to get  BO.AdjacentStations 
        /// </summary>
        /// <param name="id1">the exit station id</param>
        /// <param name="id2">the destanation station id</param>
        /// <returns> BO.AdjacentStations</returns>
        BO.AdjacentStations GetPair(int id1, int id2);
        /// <summary>
        /// func to add pair with all the details
        /// </summary>
        /// <param name="id1">the exit station id</param>
        /// <param name="id2">the destanation station id</param>
        /// <param name="distance">the diatance between this 2 stations</param>
        /// <param name="time">the travel time from exist station to destansion station</param>
        void AddPair(int id1, int id2, double distance, TimeSpan time);
        /// <summary>
        ///func to update pair 
        /// </summary>
        /// <param name="pair">the updated pair</param>
        void UpdatePair(BO.AdjacentStations pair);
        #endregion

        #region Station
        /// <summary>
        /// func to get all the stations from the data base
        /// </summary>
        /// <returns>IEnumerable<BO.Station>(all the stations)</returns>
        IEnumerable<BO.Station> GetAllStations();
        /// <summary>
        /// func to get spesific station by station id
        /// </summary>
        /// <param name="id">the id of the station</param>
        /// <returns> BO.Station</returns>
        BO.Station GetStation(int id);
        /// <summary>
        /// func to add station to the data base
        /// </summary>
        /// <param name="station">the station to add</param>
        void AddStation(BO.Station station);
        /// <summary>
        /// func to update details of station
        /// </summary>
        /// <param name="station">the updated station</param>
        void UpdateStation(BO.Station station);
        #endregion

        #region StationLine
        /// <summary>
        /// func to get all the stations line of all the lines
        /// </summary>
        /// <returns> IEnumerable<BO.StationLine></returns>
        IEnumerable<BO.StationLine> GetAllStationsLine();
        /// <summary>
        /// func to get spesific station line by the line id and the station id
        /// </summary>
        /// <param name="Lineid">the line id</param>
        /// <param name="stationId">the station id</param>
        /// <returns></returns>
        BO.StationLine GetStationLine(int Lineid, int stationId);
        /// <summary>
        /// func to add station line with all the details
        /// </summary>
        /// <param name="lineId">the line id</param>
        /// <param name="stationId">the station id</param>
        /// <param name="numInLine">the number of the station in the line</param>
        void AddStationLine(int lineId, int stationId, int numInLine);
        /// <summary>
        /// func to update the details of station line
        /// </summary>
        /// <param name="stationLine">the updated station line</param>
        void UpdateStationLine(BO.StationLine stationLine);
        /// <summary>
        /// func to delete station line by the station id and line id
        /// </summary>
        /// <param name="LineId">the line id</param>
        /// <param name="StaionId">the station id</param>
        void DeleteStationLine(int LineId, int StaionId);
        #endregion

      
        #region User
        /// <summary>
        /// func to get the detauls of the user from the data
        /// </summary>
        /// <param name="userName">the user name</param>
        /// <returns></returns>
        BO.User GetUser(string userName);
        /// <summary>
        /// func to add user
        /// </summary>
        /// <param name="userName">the user name</param>
        /// <param name="paaword">the password</param>
        void AddUser(string userName, string paaword);
        #endregion
        #region Simulator
        /// <summary>
        /// func to start runing the simulator
        /// </summary>
        /// <param name="startTime">the time to start the simulator from</param>
        /// <param name="Rate">the simulator speed rununig</param>
        /// <param name="updateTime">func to add to observ the watch</param>
        void StartSimulator(TimeSpan startTime, int Rate, Action<TimeSpan> updateTime);
        /// <summary>
        /// func to stop the simulator
        /// </summary>
        void StopSimulator();
        /// <summary>
        /// func to set the station panel 
        /// </summary>
        /// <param name="station">the spesific station</param>
        /// <param name="updateBus">the func to observ the line trip</param>
        void SetStationPanel(int station, Action<BO.LineTiming> updateBus);
        #endregion
    }
}
