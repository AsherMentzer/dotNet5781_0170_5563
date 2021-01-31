using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using BLAPI;
using System.Threading;
using BO;
using DO;
using System.Diagnostics;
using System.ComponentModel;

namespace BL
{
    class BLImp : IBL
    {
        IDL dl = DLFactory.GetDL();

        //#region Bus
        //BO.Bus BusDoBoADapter(DO.Bus busDO)
        //{
        //    BO.Bus busBO = new BO.Bus();
        //    busDO.CopyPropertiesTo(busBO);
        //    return busBO;
        //}
        //IEnumerable<BO.Bus> GetAllBuses()
        //{
        //    return from item in dl.GetAllBuses()
        //           select BusDoBoADapter(item);
        //}
        //IEnumerable<BO.Bus> GetAllBusesBy(Predicate<BO.Bus> predicate)
        //{
        //    throw new NotImplementedException();
        //}

        //BO.Bus GetBus(string licenseId)
        //{
        //    DO.Bus busDO;
        //    try
        //    {
        //        busDO = dl.GetBus(licenseId);
        //    }
        //    catch (DO.BadBusLicenceIdException ex)
        //    {
        //        return null;///////need to throw exception
        //    }
        //    return BusDoBoADapter(busDO);
        //}

        //void AddBus(BO.Bus bus)
        //{
        //    throw new NotImplementedException();
        //}
        //void UpdateBus(BO.Bus bus)
        //{
        //    throw new NotImplementedException();
        //}
        ////method that knows to updt specific fields in bus
        //void UpdateBus(string licenceId, Action<BO.Bus> update)
        //{
        //    throw new NotImplementedException();
        //}
        //void DeleteBus(string licenceId)
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerable<BO.Bus> IBL.GetAllBuses()
        //{
        //    return from item in dl.GetAllBuses()
        //           select BusDoBoADapter(item);
        //}

        //IEnumerable<BO.Bus> IBL.GetAllBusesBy(Predicate<BO.Bus> predicate)
        //{
        //    throw new NotImplementedException();
        //}

        //BO.Bus IBL.GetBus(string licenseId)
        //{
        //    throw new NotImplementedException();
        //}

        //void IBL.AddBus(BO.Bus bus)
        //{
        //    throw new NotImplementedException();
        //}

        //void IBL.UpdateBus(BO.Bus bus)
        //{
        //    throw new NotImplementedException();
        //}

        //void IBL.UpdateBus(string licenceId, Action<BO.Bus> update)
        //{
        //    throw new NotImplementedException();
        //}

        //void IBL.DeleteBus(string licenceId)
        //{
        //    throw new NotImplementedException();
        //}
        //#endregion

        #region Bus Line
        /// <summary>
        /// private func to get the average travel time to next station in line
        /// </summary>
        /// <param name="d">the current station</param>
        /// <returns>TimeSpan</returns>
        private TimeSpan getTime(DO.StationLine d)
        {
            DO.StationLine s;
            try
            {
                s = dl.GetStationLineBy(d.LineId, d.NumInLine + 1);
            }
            catch (DO.BadStatioLinenIdException)
            {
                return new TimeSpan();/////////////-----------
            }
            if (s != null)
            {
                DO.AdjacentStations p = new DO.AdjacentStations();
                try
                {
                    p = dl.GetPair(d.StationId, s.StationId);
                }
                catch (DO.BadPairIdException)
                {
                    throw new BO.BadPairIdException(d.StationId, s.StationId, "no pair enter details");
                }

                if (p != null)
                    return p.AverageTravleTime;
                else return new TimeSpan();
            }
            else return new TimeSpan();
        }
        /// <summary>
        /// private func to get the distance to next station in line
        /// </summary>
        /// <param name="d">the current station</param>
        /// <returns>double</returns>
        private double getDistance(DO.StationLine d)
        {
            DO.StationLine s;
            try
            {
                s = dl.GetStationLineBy(d.LineId, d.NumInLine + 1);
            }
            catch (DO.BadStatioLinenIdException)
            {
                return 0;
            }
            if (s != null)
            {
                DO.AdjacentStations p = new DO.AdjacentStations();
                try
                {
                    p = dl.GetPair(d.StationId, s.StationId);
                }
                catch (DO.BadPairIdException)
                {
                    throw new BO.BadPairIdException(d.StationId, s.StationId, "no pair enter details");
                }
                if (p != null)
                    return p.Distance;
                else return 0;
            }
            else return 0;
        }
        /// <summary>
        /// private func to get the name of the station in line
        /// </summary>
        /// <param name="id">the id of the station</param>
        /// <returns>string</returns>
        private string getName(int id)
        {
            DO.Station s=null;
            try
            {
                 s = dl.GetStation(id);
            }
            catch(DO.BadStationIdException)
            {
                throw new BO.BadStationIdException(id);
            }
            if (s != null)
                return s.StationName;
            return null;
        }
        /// <summary>
        /// func that get DO.BusLine and creat BO.BusLine with all the details that needed
        /// </summary>
        /// <param name="line">the DO.BusLine</param>
        /// <returns>BO.BusLine</returns>
        BO.Line BusLineDoBoADapter(DO.Line line)
        {
            BO.Line bl = new BO.Line();
            line.CopyPropertiesTo(bl);
            //get all the stations line of this line to add to BO.BusLine
            bl.Stations = from sl in dl.GetAllStationsLineBy(sl => sl.LineId == line.LineId)
                          let station = new BO.StationLine()
                          {
                              StationId = sl.StationId,
                              LineId = sl.LineId,
                              NumInLine = sl.NumInLine,
                              StationName = getName(sl.StationId),
                              AverageTravleTime = getTime(sl),
                              Distance = getDistance(sl),
                          }
                          orderby station.NumInLine
                          select station;

            return bl;
        }
        /// <summary>
        /// func that get all the details of the line and create new line
        /// </summary>
        /// <param name="LineNum">the line number</param>
        /// <param name="fId">the first station id</param>
        /// <param name="lId">the last station id</param>
        /// <param name="myArea">the area of the line</param>
        /// <returns>BO.Line</returns>
        BO.Line IBL.CreateBusLine(int LineNum, int fId, int lId, BO.Areas myArea)
        {
            try
            {
                DO.Station s = dl.GetStation(fId);
            }
            catch (DO.BadStationIdException ex)
            {
                throw new BO.BadStationIdException(fId, "Station ID is illegal", ex);
            }
            try
            {
                DO.Station s = dl.GetStation(lId);
            }
            catch (DO.BadStationIdException ex)
            {
                throw new BO.BadStationIdException(lId, "Station ID is illegal", ex);
            }
            Random rand = new Random();
            int id = rand.Next(10, 999);
            bool flag=true;
            while(flag)
            {
                DO.Line l = null;
                try
                {
                    l = dl.GetBusLine(id);
                }
                catch (DO.BadBusLineIdException) { }
                if (l == null)
                    flag = false;
                else
                    id = rand.Next();
            }
            
            DO.Line line = new DO.Line
            {
                FirstStation = fId, LastStation = lId,
                LineNumber = LineNum,
                LineId = id,
                area = (DO.Areas)myArea
            };
            BO.Line newLine = BusLineDoBoADapter(line);
            dl.AddBusLine(line);
            try
            {
                AddStationLine(id, fId, 1);
                AddStationLine(id, lId, 2);
            }
            catch(DO.BadStatioLinenIdException)
            {
                throw new BO.BadStationIdException(id);
            }
            return newLine;
        }

        public IEnumerable<BO.Line> GetAllBusLines()
        {
            return from l in dl.GetAllBusLines()
                   select BusLineDoBoADapter(l);
        }

        public BO.Line GetBusLine(int lineId)
        {
            DO.Line line;
            try
            {
                line = dl.GetBusLine(lineId);
            }
            catch (DO.BadBusLicenceIdException)
            {
                throw new BO.BadBusLineIdException(lineId);
            }
            return BusLineDoBoADapter(line);
        }

        public void AddBusLine(BO.Line busLine)
        {
            DO.Line line = new DO.Line();
            busLine.CopyPropertiesTo(line);
            try
            {
                dl.AddBusLine(line);
            }
            catch (DO.BadBusLicenceIdException) 
            {
                throw new BO.BadBusLineIdException(busLine.LineId);
            }
            DO.StationLine s = new DO.StationLine();
            //need to add all the stations in the new line
            foreach (var sl in busLine.Stations)
            {
                sl.CopyPropertiesTo(s);
                try
                {
                    dl.AddStationLine(s);
                }
                catch (DO.BadBusLicenceIdException)
                {
                    throw new BO.BadStationIdException(s.StationId);
                }
            }
            //UpdateBusLine all the details for all the station line in the line
            for (int i = 1; i < busLine.Stations.Count(); ++i)
            {
                BO.StationLine s1 = new BO.StationLine();
                BO.StationLine s2 = new BO.StationLine();
                foreach (var sl in busLine.Stations)
                {
                    if (s.NumInLine == i)
                        s1 = sl;
                    if (s.NumInLine == i + 1)
                        s2 = sl;
                }
                DO.AdjacentStations p = dl.GetPair(s1.StationId, s2.StationId);
                if (p == null)
                {
                    dl.AddPair(s1.StationId, s2.StationId, s1.Distance, s1.AverageTravleTime);
                }
            }
        }

        public void UpdateBusLine(BO.Line busLine)
        {
            DO.Line line = new DO.Line();
            busLine.CopyPropertiesTo(line);
            dl.UpdateBusLine(line);
        }

        public void DeleteBusLine(BO.Line line)
        {
            int id = line.LineId;
            try
            {
                dl.DeleteBusLine(id);
            }
            catch (DO.BadBusLineIdException ex)
            {
                throw new BO.BadBusLineIdException(id, "Station ID is illegal", ex);
            }

            IEnumerable<DO.StationLine> stations = from s in dl.GetAllStationsLineBy(sl => sl.LineId == line.LineId)
                                                   orderby s.NumInLine
                                                   select s;
            foreach (var s in stations)
            {
                dl.DeleteStationLine(s.LineId, s.StationId);
            }

            IEnumerable<DO.LineTrip> trips = from t in dl.GetAllLinesTripBy(lt => lt.LineId == line.LineId)
                                             select t;
            foreach(var t in trips)
            {
                dl.DeleteLineTrip(t.LineId, t.StartTime);
            }
        }
        #endregion
        
        #region Pairs
        public BO.AdjacentStations GetPair(int id1, int id2)
        {
            DO.AdjacentStations p;
            try
            {
                p = dl.GetPair(id1, id2);
            }
            catch (DO.BadPairIdException)
            {
                throw new BO.BadPairIdException(id1, id2);
            }
            BO.AdjacentStations pair = new BO.AdjacentStations();
            if (p != null)
                p.CopyPropertiesTo(pair);
            return pair;
        }

        public void AddPair(int id1, int id2, double distance, TimeSpan time)
        {
            try
            {
                dl.AddPair(id1, id2, distance, time);
            }
            catch (DO.BadPairIdException ex)
            {
                throw new BO.BadPairIdException(id1, id2, ex.Message);
            }
        }

        public void UpdatePair(BO.AdjacentStations pair)
        {
            DO.AdjacentStations p = new DO.AdjacentStations();
            pair.CopyPropertiesTo(p);
            dl.UpdatePair(p);
        }

        #endregion

        #region station
        /// <summary>
        /// func to get DO.station and make BO.station with all other details that needed
        /// </summary>
        /// <param name="stationDO">the DO.station</param>
        /// <returns>the BO.station</returns>
        BO.Station StationDoBoADapter(DO.Station stationDO)
        {
            BO.Station stationBO = new BO.Station();
            stationDO.CopyPropertiesTo(stationBO);
            //get all the lines that  stop in this station and add them to the BO.station
            stationBO.lines = from sl in dl.GetAllStationsLineBy(sl => sl.StationId == stationBO.StationId)
                              let line = new LineStation()
                              {
                                  LineId = sl.LineId,
                                  LineNumber = dl.GetBusLine(sl.LineId).LineNumber,
                                  LastStationId = dl.GetBusLine(sl.LineId).LastStation
                              }
                              orderby line.LineNumber
                              select line;

            return stationBO;
        }

        public IEnumerable<BO.Station> GetAllStations()
        {
            return from item in dl.GetAllStations()
                   select StationDoBoADapter(item);
        }
    
        public BO.Station GetStation(int id)
        {
            DO.Station station;
            try
            {
                station = dl.GetStation(id);
            }
            catch (DO.BadStationIdException)
            {
                throw new BO.BadStationIdException(id);
            }
            return StationDoBoADapter(station);
        }

        public void AddStation(BO.Station station)
        {
            DO.Station st = new DO.Station();
            station.CopyPropertiesTo(st);
            try
            {
                dl.AddStation(st);
            }
            catch (DO.BadStationIdException e)
            {
                throw new BO.BadStationIdException(station.StationId, e.Message);
            }
        }

        public void UpdateStation(BO.Station station)
        {
            DO.Station s = new DO.Station();
            station.CopyPropertiesTo(s);
            try
            {
                dl.UpdateStation(s);
            }
            catch (DO.BadStationIdException)
            {
                throw new BO.BadStationIdException(s.StationId);
            }
        }      
        #endregion

        #region station Line
        BO.StationLine StationLineDoBoADapter(DO.StationLine stDO)
        {
            BO.StationLine stBO = new BO.StationLine();
            stDO.CopyPropertiesTo(stBO);
            return stBO;
        }
        public IEnumerable<BO.StationLine> GetAllStationsLine()
        {
            return from s in dl.GetAllStationsLine()
                   select StationLineDoBoADapter(s);
        }
        public BO.StationLine GetStationLine(int LineId, int StationId)
        {
            DO.StationLine st;

            try
            {
                st = dl.GetStationLine(LineId, StationId);
            }
            catch (DO.BadStatioLinenIdException)
            {
                throw new BO.BadStationIdException(StationId);
            }
            return StationLineDoBoADapter(st);
        }

        public void AddStationLine(int lineId, int stationId, int numInLined)
        {
            BO.Line line = GetBusLine(lineId);
            int size = line.Stations.Count() + 1;
            //check if the index in ligal
            if (numInLined < 1 || numInLined > size)
            {
                throw new IndexOutOfRangeException($"index can be only between: 0 - {line.Stations.Count() + 1}");
            }
            //check if this station already exist un this line
            foreach (var s in line.Stations)
            {
                if (s.StationId == stationId)
                    throw new BO.BadStationIdException(stationId, "this satation already exist in this line");
            }

            bool update1 = false, update2 = false;
            int prevId = 0, nextId = 0;


            foreach (var s in line.Stations)
            {
                if (s.NumInLine == numInLined - 1)
                    prevId = s.StationId;
                if (s.NumInLine == numInLined)
                    nextId = s.StationId;
                if (s.NumInLine >= numInLined)
                {
                    BO.StationLine newStationLine = s;
                    newStationLine.NumInLine++;
                    UpdateStationLine(newStationLine);
                }
            }
            DO.Station st = dl.GetStation(stationId);
            BO.StationLine newSt = new BO.StationLine()
            {
                LineId = lineId,
                NumInLine = numInLined,
                StationId = stationId,
                StationName = st.StationName
            };
            //in case is the first station
            if (numInLined == 1)
            {
                BO.Line newLine = new BO.Line
                {
                    LineId = line.LineId,
                    LineNumber = line.LineNumber,
                    area = line.area,
                    LastStation = line.LastStation,
                    FirstStation = stationId
                };
                UpdateBusLine(newLine);
            }
            //in case is the last station
            if (numInLined == size)
            {
                BO.Line newLine = new BO.Line
                {
                    LineId = line.LineId,
                    LineNumber = line.LineNumber,
                    area = line.area,
                    LastStation = stationId,
                    FirstStation = line.FirstStation,
                };
                UpdateBusLine(newLine);
            }

            // add the new station
            DO.StationLine stationLine = new DO.StationLine()
            {
                LineId = lineId,
                NumInLine = numInLined,
                StationId = stationId,
            };
            try
            {
                dl.AddStationLine(stationLine);
            }
            catch (DO.BadStatioLinenIdException) { }

            DO.AdjacentStations pair = new DO.AdjacentStations();
            //search if there is pair between the previos station to the new one 
            if (prevId != 0)
            {
                try
                {
                    pair = dl.GetPair(prevId, stationId);
                }
                catch (DO.BadPairIdException) { update1 = true; }
                //if the pair exist update the previos station with the details
                if (!update1)
                {
                    DO.Station stat = dl.GetStation(prevId);
                    BO.StationLine stationLine1 = new BO.StationLine()
                    {
                        LineId = lineId,
                        NumInLine = numInLined - 1,
                        StationId = prevId,
                        Distance = pair.Distance,
                        AverageTravleTime = pair.AverageTravleTime,
                        StationName = stat.StationName
                    };
                    UpdateStationLine(stationLine1);
                }
            }
            //search if there is pair between the previos station to the new one 
            if (nextId != 0)
            {
                try
                {
                    pair = dl.GetPair(stationId, nextId);
                }
                catch (DO.BadPairIdException) { update2 = true; }
                //update the details of the new station
                if (!update2)
                {
                    DO.Station stat = dl.GetStation(stationId);
                    BO.StationLine stationLine1 = new BO.StationLine()
                    {
                        LineId = lineId,
                        NumInLine = numInLined,
                        StationId = stationId,
                        Distance = pair.Distance,
                        AverageTravleTime = pair.AverageTravleTime,
                        StationName = stat.StationName
                    };
                    UpdateStationLine(stationLine1);  
                }
            }
            if (update1 && update2)
                throw new BO.BadPairIdException(prevId, stationId, nextId);
            else if (update1 && !update2)
                throw new BO.BadPairIdException(prevId, stationId);
            else if (!update1 && update2)
                throw new BO.BadPairIdException(nextId, stationId);

        }

        public void UpdateStationLine(BO.StationLine stationLine)
        {
            DO.StationLine s = new DO.StationLine();
            stationLine.CopyPropertiesTo(s);
            dl.UpdateStationLine(s);
        }

        public void DeleteStationLine(int lId, int stId)
        {
            BO.Line line = GetBusLine(lId);
            BO.StationLine st = GetStationLine(lId, stId);
            int size = line.Stations.Count();
            int prevId = 0;
            int nextId = 0;

            ///update all the numbers of stations in line  
            foreach (var s in line.Stations)
            {
                if (s.NumInLine == st.NumInLine - 1)
                    prevId = s.StationId;
                if (s.NumInLine == st.NumInLine + 1)
                    nextId = s.StationId;
                if (s.NumInLine > st.NumInLine)
                {
                    BO.StationLine newStationLine = s;
                    newStationLine.NumInLine--;
                    UpdateStationLine(newStationLine);
                }
            }
            try
            {
                dl.DeleteStationLine(lId, stId);
            }
            catch (DO.BadBusLineIdException)
            { }
            //in case is the last station in line so no need 
            //to get details for the next station only the prev station to 0
            //need yo update the line :the last station
            if (st.NumInLine == size)
            {
                line.LastStation = prevId;
                UpdateBusLine(line);
                BO.StationLine LSt = GetStationLine(lId, prevId);
                st.Distance = 0;
                st.AverageTravleTime = new TimeSpan();
                UpdateStationLine(LSt);
                return;
            }
            ///if delete the first station in line 
            if (st.NumInLine == 1)
            {
                line.FirstStation = nextId;
                UpdateBusLine(line);
                return;
            }
            DO.AdjacentStations pair;
            try
            {
                pair = dl.GetPair(prevId, nextId);
            }
            catch (DO.BadPairIdException)
            {
                throw new BO.BadPairIdException(prevId, nextId);
            }
            //update the details between the station 
            BO.StationLine newSt = GetStationLine(lId, prevId);
            st.Distance = pair.Distance;
            st.AverageTravleTime = pair.AverageTravleTime;
            UpdateStationLine(newSt);
        }
        #endregion
     
        #region simulator
        public void StartSimulator(TimeSpan startTime, int Rate, Action<TimeSpan> updateTime)
        {
            WatchSimulator.Instance.Observer += updateTime;//add the func to observ
            WatchSimulator.Instance.StartWatch(startTime, Rate);
        }
   
        public void StopSimulator()
        {
            WatchSimulator.Instance.cancel = true;
        }

        public void SetStationPanel(int station, Action<LineTiming> updateBus)
        {
            TripsOperator.Instance.stationId = station;
            TripsOperator.Instance.Observer += updateBus;//add the func to observ
        }
        #endregion

        #region User
        public BO.User GetUser(string userName)
        {
            DO.User user;
            try
            {
                user = dl.GetUser(userName);
            }
            catch (DO.BadUSerNameException e)
            {
                throw new BO.BadUSerNameException(e.Message);
            }
            BO.User user1 = new BO.User();
            user.CopyPropertiesTo(user1);
            return user1;
        }

        public void AddUser(string userName, string password)
        {
            DO.User user = new DO.User { UserName = userName, Password = password, isAdmin = true };
            try
            {
                dl.AddUser(user);
            }
            catch (DO.BadUSerNameException e)
            {
                throw new BO.BadUSerNameException(e.Message);
            }
        }
        #endregion
    }
}
