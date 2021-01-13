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

namespace BL
{
    class BLImp : IBL
    {
        IDL dl = DLFactory.GetDL();

        #region Bus
        BO.Bus BusDoBoADapter(DO.Bus busDO)
        {
            BO.Bus busBO = new BO.Bus();
            busDO.CopyPropertiesTo(busBO);
            return busBO;
        }
        IEnumerable<BO.Bus> GetAllBuses()
        {
            return from item in dl.GetAllBuses()
                   select BusDoBoADapter(item);
        }
        IEnumerable<BO.Bus> GetAllBusesBy(Predicate<BO.Bus> predicate)
        {
            throw new NotImplementedException();
        }

        BO.Bus GetBus(string licenseId)
        {
            DO.Bus busDO;
            try
            {
                busDO = dl.GetBus(licenseId);
            }
            catch (DO.BadBusLicenceIdException ex)
            {
                return null;///////need to throw exception
            }
            return BusDoBoADapter(busDO);
        }

        void AddBus(BO.Bus bus)
        {
            throw new NotImplementedException();
        }
        void UpdateBus(BO.Bus bus)
        {
            throw new NotImplementedException();
        }
        //method that knows to updt specific fields in bus
        void UpdateBus(string licenceId, Action<BO.Bus> update)
        {
            throw new NotImplementedException();
        }
        void DeleteBus(string licenceId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<BO.Bus> IBL.GetAllBuses()
        {
            return from item in dl.GetAllBuses()
                   select BusDoBoADapter(item);
        }

        IEnumerable<BO.Bus> IBL.GetAllBusesBy(Predicate<BO.Bus> predicate)
        {
            throw new NotImplementedException();
        }

        BO.Bus IBL.GetBus(string licenseId)
        {
            throw new NotImplementedException();
        }

        void IBL.AddBus(BO.Bus bus)
        {
            throw new NotImplementedException();
        }

        void IBL.UpdateBus(BO.Bus bus)
        {
            throw new NotImplementedException();
        }

        void IBL.UpdateBus(string licenceId, Action<BO.Bus> update)
        {
            throw new NotImplementedException();
        }

        void IBL.DeleteBus(string licenceId)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Bus Line
        TimeSpan getTime(DO.StationLine d)
        {
            DO.StationLine s = dl.GetStationLineBy(d.LineId, d.NumInLine + 1);
            if (s != null)
            {
                DO.AdjacentStations p = new DO.AdjacentStations();
                try
                {
                    p = dl.GetPair(d.StationId, s.StationId);
                }
                catch (DO.BadPairIdException ex)
                {
                    throw new BO.BadPairIdException(d.StationId, s.StationId, "no pair enter details");
                }

                if (p != null)
                    return p.AverageTravleTime;
                else return new TimeSpan();
            }
            else return new TimeSpan();
        }
        double getDistance(DO.StationLine d)
        {
            DO.StationLine s = dl.GetStationLineBy(d.LineId, d.NumInLine + 1);
            if (s != null)
            {
                DO.AdjacentStations p = new DO.AdjacentStations();
                try
                {
                    p = dl.GetPair(d.StationId, s.StationId);
                }
                catch (DO.BadPairIdException ex)
                {
                    throw new BO.BadPairIdException(d.StationId, s.StationId, "no pair enter details");
                }
                if (p != null)
                    return p.Distance;
                else return 0;
            }
            else return 0;
        }
        string getName(int id)
        {
            DO.Station s = dl.GetStation(id);
            if (s != null)
                return s.StationName;
            return null;
        }
        BO.Line BusLineDoBoADapter(DO.Line line)
        {
            BO.Line bl = new BO.Line();
            line.CopyPropertiesTo(bl);
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
            AddStationLine(Data.DataSource.linesId, fId, 1);
            AddStationLine(Data.DataSource.linesId, lId, 2);
            DO.Line line = new DO.Line { FirstStation = fId, LastStation = lId, LineNumber = LineNum, LineId = Data.DataSource.linesId++, area = (DO.Areas)myArea };
            BO.Line newLine = BusLineDoBoADapter(line);
            dl.AddBusLine(line);
            return newLine;
        }
        public IEnumerable<BO.Line> GetAllBusLines()
        {
            return from l in dl.GetAllBusLines()
                   select BusLineDoBoADapter(l);
        }

        public IEnumerable<BO.Line> GetAllBusLinesBy(Predicate<BO.Line> predicate)
        {
            throw new NotImplementedException();
        }

        public BO.Line GetBusLine(int lineId)
        {
            DO.Line line;
            try
            {
                line = dl.GetBusLine(lineId);
            }
            catch (DO.BadBusLicenceIdException ex)
            {
                return null;///////need to throw exception
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
            catch (DO.BadBusLicenceIdException e) { }//-----------------------need to add exception
            DO.StationLine s = new DO.StationLine();
            foreach (var sl in busLine.Stations)
            {
                sl.CopyPropertiesTo(s);
                try
                {
                    dl.AddStationLine(s);
                }
                catch (DO.BadBusLicenceIdException e) { }//-----------------------need to add exception
            }
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
            //throw new NotImplementedException();
        }

        public void UpdateBusLine(int lineId, Action<BO.Line> update)
        {
            throw new NotImplementedException();
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
   
        }
        #endregion
        #region Line Exist
        public IEnumerable<BO.LineTrip> GetAllExistsLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.LineTrip> GetAllExistsLinesBy(Predicate<BO.LineTrip> predicate)
        {
            throw new NotImplementedException();
        }

        public BO.LineTrip GetLineExist(int lineId)
        {
            throw new NotImplementedException();
        }

        public void AddLineExist(BO.LineTrip lineExist)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineExist(BO.LineTrip lineExist)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineExist(int lineId, Action<BO.Line> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineExist(int lineId)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Pairs
        public IEnumerable<BO.AdjacentStations> GetAllPairs()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.AdjacentStations> GetAllPairsBy(Predicate<BO.Station> predicate)
        {
            throw new NotImplementedException();
        }

        public BO.AdjacentStations GetPair(int id1, int id2)
        {
            DO.AdjacentStations p;
            try
            {
                p = dl.GetPair(id1, id2);
            }
            catch (DO.BadPairIdException ex)
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

        public void UpdatePair(int id, Action<BO.AdjacentStations> update)
        {
            throw new NotImplementedException();
        }

        public void DeletePair(int id1, int id2)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region station
        BO.Station StationDoBoADapter(DO.Station stationDO)
        {
            BO.Station stationBO = new BO.Station();
            stationDO.CopyPropertiesTo(stationBO);
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

        public IEnumerable<BO.Station> GetAllStationsBy(Predicate<BO.Station> predicate)
        {
            throw new NotImplementedException();
        }

        public BO.Station GetStation(int id)
        {
            DO.Station station;
            try
            {
                station = dl.GetStation(id);
            }
            catch (DO.BadBusLicenceIdException ex)
            {
                return null;///////need to throw exception
            }
            return StationDoBoADapter(station);
        }

        public void AddStation(BO.Station station)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(BO.Station station)
        {
            DO.Station s = new DO.Station();
            station.CopyPropertiesTo(s);
            try
            {
                dl.UpdateStation(s);
            }
            catch (DO.BadStationIdException ex)
            {
                throw new BO.BadStationIdException(s.StationId);
            }
        }

        public void UpdateStation(int id, Action<BO.Station> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region station Line
        BO.StationLine StationLineDoBoADapter(DO.StationLine stDO)
        {
            BO.StationLine stBO = new BO.StationLine();
            stDO.CopyPropertiesTo(stBO);
            //DO.StationLine s2 = dl.GetStationLine(stDO.LineId, stDO.NumInLine + 1);
            return stBO;
        }
        public IEnumerable<BO.StationLine> GetAllStationsLine()
        {
            return from s in dl.GetAllStationsLine()
                   select StationLineDoBoADapter(s);
        }

        public IEnumerable<BO.StationLine> GetAllStationsLineBy(Predicate<BO.StationLine> predicate)
        {
            throw new NotImplementedException();
        }

        public BO.StationLine GetStationLine(int LineId, int StationId)
        {
            DO.StationLine st;/// = new BO.StationLine();

            try
            {
                st = dl.GetStationLine(LineId, StationId);
            }
            catch (DO.BadStatioLinenIdException)
            {
                throw new BO.BadStationIdException(StationId);////////change the exception
            }
            return StationLineDoBoADapter(st);
        }

        public void AddStationLine(int lineId, int stationId, int numInLined)
        {
            BO.Line line = GetBusLine(lineId);
            //check if the index in ligal
            if (numInLined < 1 || numInLined > line.Stations.Count()+1)
            {
                throw new IndexOutOfRangeException($"index can be only between: 0 - {line.Stations.Count()+1}");
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
            DO.Station st= dl.GetStation(stationId);
            BO.StationLine newSt = new BO.StationLine()
            {
                LineId = lineId,
                NumInLine = numInLined,
                StationId = stationId,
                StationName=st.StationName
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
            if (numInLined == line.Stations.Count()+1)
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
            //add the new station
            DO.StationLine stationLine = new DO.StationLine()
            {
                LineId = lineId,
                NumInLine = numInLined,
                StationId = stationId,
            };
            dl.AddStationLine(stationLine);


            DO.AdjacentStations pair=new DO.AdjacentStations();
            //search if there is pair between the previos station to the new one 
            if(prevId !=0)
            try
            {
               pair= dl.GetPair(prevId, stationId);
            }
            catch (DO.BadPairIdException) { update1 = true; }
            //if the pair exist update the previos station with the details
            if (!update1)
            {
                DO.Station stat= dl.GetStation(prevId);
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
            //search if there is pair between the previos station to the new one 
            if (nextId != 0)
                try
                {
                   pair= dl.GetPair(stationId,nextId);
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
                UpdateStationLine(stationLine1);                  //(stationLine);
            }          

            if (update1 && update2)
                throw new BO.BadPairIdException(prevId,stationId,nextId);
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

        public void UpdateStationLine(int id, Action<BO.StationLine> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteStationLine(int lId, int stId)
        {
            BO.Line line = GetBusLine(lId);
            BO.StationLine st = GetStationLine(lId, stId);
            int size = line.Stations.Count();
            int prevId = 0;
            int nextId=0;

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
            catch (DO.BadBusLineIdException ex)
            { }
            //in case is the last station in line so no need 
            //to get details for the next station only the prev station to 0
            //need yo update the line :the last station
            if (st.NumInLine == size)
            {
                line.LastStation = prevId;
                BO.StationLine LSt=GetStationLine(lId, prevId);
                st.Distance = 0;
                st.AverageTravleTime = new TimeSpan();
                UpdateStationLine(LSt);
                return;
            }
            ///if delete the first station in line 
            if (st.NumInLine == 1)
            {
                line.FirstStation = nextId;
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
        #region travle Line
        public IEnumerable<BO.BusOnTrip> GetAllTravelBuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.BusOnTrip> GetAllTravelBusesLineBy(Predicate<BO.BusOnTrip> predicate)
        {
            throw new NotImplementedException();
        }

        public BO.Station GetTravelBus(int id)
        {
            throw new NotImplementedException();
        }

        public void AddTravelBus(BO.BusOnTrip travelBus)
        {
            throw new NotImplementedException();
        }

        public void UpdateTravelBus(BO.BusOnTrip travelBus)
        {
            throw new NotImplementedException();
        }

        public void UpdateTravelBus(int id, Action<BO.BusOnTrip> update)
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
