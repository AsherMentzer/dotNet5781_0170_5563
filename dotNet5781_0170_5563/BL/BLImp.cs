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
    class BLImp:IBL
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
            try {
               busDO= dl.GetBus(licenseId);
            }
            catch(DO.BadBusLicenceIdException ex)
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
                DO.PairOfConsecutiveStation p=new DO.PairOfConsecutiveStation();
                try
                {
                    p = dl.GetPair(d.StationId, s.StationId);
                }
                catch(DO.BadPairIdException ex) { }

                if(p !=null)
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
                DO.PairOfConsecutiveStation p = new DO.PairOfConsecutiveStation();
                try
                {
                    p = dl.GetPair(d.StationId, s.StationId);
                }
                catch (DO.BadPairIdException ex) { }
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
        BO.BusLine BusLineDoBoADapter(DO.BusLine line)
        {
            BO.BusLine bl = new BO.BusLine();
            line.CopyPropertiesTo(bl);
            bl.Stations = from sl in dl.GetAllStationsLineBy(sl => sl.LineId == line.LineId)
                               let station =new BO.StationLine()
                               {
                                   StationId = sl.StationId,
                                   LineId = sl.LineId,
                                   NumInLine = sl.NumInLine,
                                   StationName=getName(sl.StationId),
                                   AverageTravleTime = getTime(sl),
                                   Distance = getDistance(sl),
                               }
           
                                       orderby station.NumInLine
                               select station;
                                 
            return bl;
        }

        BO.BusLine IBL.CreateBusLine(int LineNum, int fId, int lId, BO.Areas myArea)
        {
             try
            {
                DO.Station s = dl.GetStation(fId);
            }
            catch (DO.BadStationIdException ex)
            {
               throw new BO.BadStationIdException(fId,"Station ID is illegal",ex);
            }
            try
            {
                DO.Station s = dl.GetStation(lId);
            }
            catch (DO.BadStationIdException ex)
            {
                throw new BO.BadStationIdException(lId, "Station ID is illegal", ex);
            }
            AddStationLine(Data.DataSource.linesId , fId, 1);
            AddStationLine(Data.DataSource.linesId, lId, 2);
            DO.BusLine line = new DO.BusLine { FirstStation = fId, LastStation = lId, LineNumber = LineNum,LineId=Data.DataSource.linesId++ ,area=(DO.Areas)myArea};
            BO.BusLine newLine = BusLineDoBoADapter(line);
            dl.AddBusLine(line);
            return newLine;
        }
        public IEnumerable<BO.BusLine> GetAllBusLines()
        {
            return from l in dl.GetAllBusLines()
                   select BusLineDoBoADapter(l);
        }

        public IEnumerable<BO.BusLine> GetAllBusLinesBy(Predicate<BO.BusLine> predicate)
        {
            throw new NotImplementedException();
        }

        public BO.BusLine GetBusLine(int lineId)
        {
            DO.BusLine line;
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

        public void AddBusLine(BO.BusLine busLine)
        {
            DO.BusLine line=new DO.BusLine();
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
                DO.PairOfConsecutiveStation p = dl.GetPair(s1.StationId, s2.StationId);
                if (p == null)
                {
                    p = new DO.PairOfConsecutiveStation
                    {
                        StationId1 = s1.StationId,
                        StationId2 = s2.StationId,
                        AverageTravleTime = s1.AverageTravleTime,
                        Distance = s1.Distance
                    };
                    dl.AddPair(p);
                }
            }
        }

        public void UpdateBusLine(BO.BusLine busLine)
        {
            throw new NotImplementedException();
        }

        public void UpdateBusLine(int lineId, Action<BO.BusLine> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBusLine(BO.BusLine line)
        {
            int id = line.LineId;
            try
            {
                dl.DeleteBusLine(id);
            }
            catch(DO.BadBusLineIdException ex)
            {
                throw new BO.BadBusLineIdException( id,"Station ID is illegal", ex);
            }

            
        }
        #endregion
        #region Line Exist
        public IEnumerable<BO.LineExist> GetAllExistsLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.LineExist> GetAllExistsLinesBy(Predicate<BO.LineExist> predicate)
        {
            throw new NotImplementedException();
        }

        public BO.LineExist GetLineExist(int lineId)
        {
            throw new NotImplementedException();
        }

        public void AddLineExist(BO.LineExist lineExist)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineExist(BO.LineExist lineExist)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineExist(int lineId, Action<BO.BusLine> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineExist(int lineId)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Pairs
        public IEnumerable<BO.PairOfConsecutiveStation> GetAllPairs()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.PairOfConsecutiveStation> GetAllPairsBy(Predicate<BO.Station> predicate)
        {
            throw new NotImplementedException();
        }

        public BO.PairOfConsecutiveStation GetPair(int id1, int id2)
        {
            DO.PairOfConsecutiveStation p;
            try
            {
                p = dl.GetPair(id1,id2);
            }
            catch (DO.BadPairIdException ex)
            {
                throw new BO.BadPairIdException(id1,id2);
            }
            BO.PairOfConsecutiveStation pair=new BO.PairOfConsecutiveStation();
            if (p != null)
            p.CopyPropertiesTo(pair);
            return pair;
        }

        public void AddPair(int id1, int id2, double distance, TimeSpan time)
        {
            throw new NotImplementedException();
        }

        public void UpdatePair(BO.PairOfConsecutiveStation pair)
        {
            throw new NotImplementedException();
        }

        public void UpdatePair(int id, Action<BO.PairOfConsecutiveStation> update)
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
            stationBO.lines = (from sl in dl.GetAllStationsLineBy(sl => sl.StationId == stationBO.StationId)
                              let line = dl.GetBusLine(sl.LineId)
                              select BusLineDoBoADapter(line));

            
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
            throw new NotImplementedException();
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
            DO.StationLine s2 = dl.GetStationLine(stDO.LineId, stDO.NumInLine + 1);
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

        public BO.Station GetStationLine(int id)
        {
            throw new NotImplementedException();
        }

        public void AddStationLine(int lineid,int stationId,int numInLined)
        {
            DO.StationLine station = new DO.StationLine {LineId=lineid,StationId=stationId,NumInLine=numInLined };
            
            dl.AddStationLine(station);
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

        public void DeleteStationLine(int lId,int stId)
        {
            try
            {
                dl.DeleteStationLine(lId,stId);
            }
            catch (DO.BadBusLineIdException ex)
            {
                
            }
        }
        #endregion
        #region travle Line
        public IEnumerable<BO.TravelBus> GetAllTravelBuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.TravelBus> GetAllTravelBusesLineBy(Predicate<BO.TravelBus> predicate)
        {
            throw new NotImplementedException();
        }

        public BO.Station GetTravelBus(int id)
        {
            throw new NotImplementedException();
        }

        public void AddTravelBus(BO.TravelBus travelBus)
        {
            throw new NotImplementedException();
        }

        public void UpdateTravelBus(BO.TravelBus travelBus)
        {
            throw new NotImplementedException();
        }

        public void UpdateTravelBus(int id, Action<BO.TravelBus> update)
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
