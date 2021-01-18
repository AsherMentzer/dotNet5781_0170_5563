﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using DLAPI;
using DO;
using Data;
namespace DL
{
    sealed class DLObject : IDL
    {
        #region singelton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }// static ctor to ensure instance init is done just before first usage
        DLObject() { } // default => private
        public static DLObject Instance { get => instance; }// The public Instance property to use
        #endregion

        //Implement IDL methods, CRUD
        #region bus crud


        public IEnumerable<Bus> GetAllBuses()
        {
            return from bus in DataSource.buses
                   select bus.Clone();
        }

        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            return from bus in DataSource.buses
                   where predicate(bus)
                   select bus.Clone();
        }

        public Bus GetBus(string id)
        {
            Bus bus = DataSource.buses.Find(b => b.LicenseId == id);

            if (bus != null)
                return bus.Clone();
            else
                throw new BadBusLicenceIdException(id, $"bad Bus LicenceId: {id}");
        }

        public void AddBus(Bus bus)
        {
            if (DataSource.buses.FirstOrDefault(b => b.LicenseId == bus.LicenseId) != null)
                throw new BadBusLicenceIdException(bus.LicenseId, "Duplicate bus Licence id");
            DataSource.buses.Add(bus.Clone());
        }

        public void UpdateBus(Bus newBus)
        {
            Bus bus = DataSource.buses.Find(b => b.LicenseId == newBus.LicenseId);

            if (newBus != null)
            {
                DataSource.buses.Remove(bus);
                DataSource.buses.Add(newBus.Clone());
            }
            else
                throw new BadBusLicenceIdException(newBus.LicenseId, $"bad Bus LicenceId: {newBus.LicenseId}");
        }

        public void UpdateBus(string licenceId, Action<Bus> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(string id)
        {
            Bus bus = DataSource.buses.Find(b => b.LicenseId == id);

            if (bus != null)
            {
                DataSource.buses.Remove(bus);
            }
            else
                throw new BadBusLicenceIdException(bus.LicenseId, "bad bus Licence id:{id}");
        }
        #endregion

        #region BusLine CRUD
        public IEnumerable<Line> GetAllBusLines()
        {
            return (IEnumerable<Line>)(from busLine in DataSource.lines
                                          select busLine.Clone());
        }

        public IEnumerable<Line> GetAllBusLinesBy(Predicate<Line> predicate)
        {
            return from busLine in DataSource.lines
                   where predicate(busLine)
                   select busLine.Clone();
        }

        public Line GetBusLine(int id)
        {
            Line busLine = DataSource.lines.Find(l => l.LineId == id);

            if (busLine != null)
                return busLine.Clone();
            else
                throw new BadBusLineIdException(id, $"bad Bus LicenceId: {id}");
        }

        public void AddBusLine(Line busLine)
        {
            if (DataSource.lines.FirstOrDefault(l => l.LineId == busLine.LineId) != null)
                throw new BadBusLineIdException(busLine.LineNumber, "Duplicate bus Line number");
            DataSource.lines.Add(busLine.Clone());
        }

        public void UpdateBusLine(Line busLine)
        {
            Line line = DataSource.lines.FirstOrDefault(l => l.LineNumber == busLine.LineNumber);
            if (line != null)
            {
                DataSource.lines.Remove(line);
                DataSource.lines.Add(busLine.Clone());
            }
            else
                throw new BadBusLineIdException(busLine.LineNumber, $"bad Bus Line Id: {busLine.LineNumber}");
        }

        public void UpdateBusLine(int lineId, Action<Line> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBusLine(int lineId)
        {
            Line line = DataSource.lines.FirstOrDefault(l => l.LineId == lineId);
            if (line != null)
            {
                DataSource.lines.Remove(line);
            }
            else
                throw new BadBusLineIdException(lineId, $"bad Bus Line Id: {lineId}");
        }
        #endregion

        #region LineTrip CRUD
        public IEnumerable<LineTrip> GetAllLinesTrip()
        {
            return (IEnumerable<LineTrip>)(from l in DataSource.linesExists
                                            select l.Clone());
        }

        public IEnumerable<LineTrip> GetAllLinesTripBy(Predicate<LineTrip> predicate)
        {
            throw new NotImplementedException();
        }

        public LineTrip GetLineTrip(int id, TimeSpan time)
        {
            LineTrip line = DataSource.linesExists.Find(l => l.LineId == id);

            if (line != null)
                return line.Clone();
            else
                throw new BadLineTripException(id, time,$"bad Bus LicenceId: {id}");
        }

        public void AddLineTrip(LineTrip lineTrip)
        {
            if (DataSource.linesExists.FirstOrDefault(l => l.LineId == lineTrip.LineId) != null)
                throw new BadLineTripException(lineTrip.LineId,lineTrip.StartTime ,"Duplicate  Line exist number");
            DataSource.linesExists.Add(lineTrip.Clone());
        }

        public void UpdateLineTrip(LineTrip lineTrip)
        {
            LineTrip line = DataSource.linesExists.FirstOrDefault(l => l.LineId == lineTrip.LineId);
            if (line != null)
            {
                DataSource.linesExists.Remove(line);
                DataSource.linesExists.Add(lineTrip.Clone());
            }
            else
                throw new BadLineTripException(lineTrip.LineId,lineTrip.StartTime ,$"bad Bus Line Id: {lineTrip.LineId}");
        }

        public void UpdateLineTrip(int lineId, Action<Line> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineTrip(int lineId, TimeSpan time)
        {
            LineTrip line = DataSource.linesExists.FirstOrDefault(l => l.LineId == lineId);
            if (line != null)
            {
                DataSource.linesExists.Remove(line);
            }
            else
                throw new BadLineTripException(lineId, time,$"bad Bus Line Id: {lineId}");
        }
        #endregion

        #region Pairs CRUD
        public IEnumerable<AdjacentStations> GetAllPairs()
        {
            return (IEnumerable<AdjacentStations>)(from pair in DataSource.pairs
                                                           select pair.Clone());
        }

        public IEnumerable<AdjacentStations> GetAllPairsBy(Predicate<AdjacentStations> predicate)
        {
            throw new NotImplementedException();
        }

        public AdjacentStations GetPair(int id1, int id2)
        {
            AdjacentStations pair = DataSource.pairs.Find(p => p.StationId1 == id1 && p.StationId2 == id2);

            if (pair != null)
                return pair.Clone();
            else               
            throw new BadPairIdException(id1,id2);
        }

        public void AddPair(int id1,int id2,double distance,TimeSpan time)
        {

            //if (DataSource.pairs.Find(p => p.StationId1 == id1 && p.StationId2 == id2) != null) ;
            //Station s1 = DataSource.stations.FirstOrDefault(s => s.StationId == id1);
            //Station s2 = DataSource.stations.FirstOrDefault(s => s.StationId == id2);
            //PairOfConsecutiveStation pair = new PairOfConsecutiveStation { StationId1 = id1, StationId2 = id2, Distance = distance, AverageTravleTime = time };
            if (DataSource.pairs.Find(p => p.StationId1 == id1 && p.StationId2 == id2) != null)       
            throw new DO.BadPairIdException(id1,id2, "Duplicate  pair");
            DO.AdjacentStations pair= new AdjacentStations { StationId1 = id1, StationId2 = id2, Distance = distance, AverageTravleTime = time };
            DataSource.pairs.Add(pair);
        }

        public void UpdatePair(AdjacentStations pair)
        {
            AdjacentStations nPair = DataSource.pairs.FirstOrDefault(p => p.StationId1 == pair.StationId1 && p.StationId2==pair.StationId2);
            if (nPair != null)
            {
                DataSource.pairs.Remove(nPair);
                DataSource.pairs.Add(pair.Clone());
            }
            else
                throw new BadPairIdException(pair.StationId1, pair.StationId2,
                    $"bad  pair:  Id1: {pair.StationId1}, id2{pair.StationId2}");
        }

        public void UpdatePair(int id, Action<AdjacentStations> update)
        {
            throw new NotImplementedException();
        }

        public void DeletePair(int id1, int id2)
        {
            AdjacentStations pair = DataSource.pairs.Find(p => p.StationId1 == id1 && p.StationId2 == id2);

            if (pair != null)
                DataSource.pairs.Remove(pair);


        }
        #endregion

        #region Station CRUD
        public IEnumerable<Station> GetAllStations()
        {
            return (IEnumerable<Station>)(from station in DataSource.stations
                                          select station.Clone());
        }

        public IEnumerable<Station> GetAllStationsBy(Predicate<Station> predicate)
        {
            return (IEnumerable<Station>)(from station in DataSource.stations
                                          where predicate(station)
                                          select station.Clone());
        }

        public Station GetStation(int id)
        {
            Station station = DataSource.stations.Find(s => s.StationId == id);

            if (station != null)
                return station.Clone();
            else return null;
                //throw new BadStationIdException(id, $"bad Bus LicenceId: {id}");
        }

        public void AddStation(Station station)
        {
            if (DataSource.stations.FirstOrDefault(s => s.StationId == station.StationId) != null)
                throw new BadStationIdException(station.StationId, "Duplicate station id");
            DataSource.stations.Add(station.Clone());
        }

        public void UpdateStation(Station nStation)
        {
            Station station = DataSource.stations.Find(s => s.StationId == nStation.StationId);

            if (nStation != null)
            {
                DataSource.stations.Remove(station);
                DataSource.stations.Add(nStation.Clone());
            }
            else
                throw new BadStationIdException(nStation.StationId, $"bad Bus LicenceId: {nStation.StationId}");
        }
        public void UpdateStation(int id, Action<Station> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int id)
        {
            Station station = DataSource.stations.Find(s => s.StationId == id);

            if (station != null)
            {
                DataSource.stations.Remove(station);
            }
            else
                throw new BadStationIdException(station.StationId, $"bad Bus LicenceId: {station.StationId}");
        }
        #endregion

        #region StationLine CRUD
        public IEnumerable<StationLine> GetAllStationsLine()
        {
          return from station in DataSource.stationsLine
            select station.Clone();
        }

        public IEnumerable<StationLine> GetAllStationsLineBy(Predicate<StationLine> predicate)
        {
            return from station in DataSource.stationsLine
                                              where predicate(station)
                                              select station.Clone();
        }

        public StationLine GetStationLine(int lineId, int stationId)
        {
            StationLine s = DataSource.stationsLine.Find(b => b.LineId == lineId && b.StationId == stationId);

            if (s != null)
                return s.Clone();
            else
                return null;
            //throw new BadBusLicenceIdException(id, $"bad Bus LicenceId: {id}");
        }
        StationLine IDL.GetStationLineBy(int lineId, int numInLine)
        {
            StationLine s = DataSource.stationsLine.Find(b => b.LineId == lineId && b.NumInLine == numInLine);

            if (s != null)
                return s.Clone();
            else
               throw new DO.BadStatioLinenIdException(lineId, numInLine, $"bad station Line id: line id: {lineId}");
        }
        //StationLine GetStationLineBy(int lineId, int numInLine)
        //{
        //    StationLine s = DataSource.stationsLine.Find(b => b.LineId == lineId && b.NumInLine == nu);

        //    if (s != null)
        //        return s.Clone();
        //    else
        //        return null;
        //    //throw new BadBusLicenceIdException(id, $"bad Bus LicenceId: {id}");
        //}
        public void AddStationLine(StationLine stationLine)
        {
            if (DataSource.stationsLine.FirstOrDefault(s => s.StationId == stationLine.StationId && s.LineId==stationLine.LineId) == null)
               // throw new BadStationLineIdException(station.StationId, "Duplicate station id");
            DataSource.stationsLine.Add(stationLine.Clone());
        }

        public void UpdateStationLine(StationLine stationLine)
        {
            StationLine station = DataSource.stationsLine.FirstOrDefault(s => s.LineId == stationLine.LineId && s.StationId==stationLine.StationId );
            if (station != null)
            {
                DataSource.stationsLine.Remove(station);
                DataSource.stationsLine.Add(stationLine.Clone());
            }
            else
                throw new BadStatioLinenIdException(stationLine.LineId,stationLine.StationId,
                    $"bad  Line Id: {stationLine.LineId},bad station id{stationLine.StationId}");
        }

        public void UpdateStationLine(int id, Action<StationLine> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteStationLine(int lineId,int stationId)
        {
            DO.StationLine station;
            station = DataSource.stationsLine.FirstOrDefault(s => s.LineId == lineId && s.StationId == stationId);
            if (station != null)
              
            //foreach(var s in DataSource.stationsLine)
            //{
            //    if (s.LineId == lineId && s.NumInLine > station.NumInLine)
            //        s.NumInLine--;
            //}
            DataSource.stationsLine.Remove(station);
        }
        #endregion

        #region TravelBus CRUD
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

        public Bus GetBus(int licenseId)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region User
        public User GetUser(string userName)
        {
            User user = DataSource.users.FirstOrDefault(u => u.UserName == userName);
            if (user != null)
                return user;
            else
                throw new BadUSerNameException(userName);
        }

        public void AddUser(User user)
        {
            User us = DataSource.users.FirstOrDefault(u => u.UserName ==user.UserName);
            if (user != null)
                throw new BadUSerNameException($"Duplicate User Name: {user.UserName}");
            else
                DataSource.users.Add(user);
        }


        #endregion
    }
}
