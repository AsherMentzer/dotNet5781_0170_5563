using System;
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
        public IEnumerable<BusLine> GetAllBusLines()
        {
            return (IEnumerable<BusLine>)(from busLine in DataSource.lines
                                          select busLine.Clone());
        }

        public IEnumerable<BusLine> GetAllBusLinesBy(Predicate<BusLine> predicate)
        {
            return from busLine in DataSource.lines
                   where predicate(busLine)
                   select busLine.Clone();
        }

        public BusLine GetBusLine(int id)
        {
            BusLine busLine = DataSource.lines.Find(l => l.LineId == id);

            if (busLine != null)
                return busLine.Clone();
            else
                throw new BadBusLineIdException(id, $"bad Bus LicenceId: {id}");
        }

        public void AddBusLine(BusLine busLine)
        {
            if (DataSource.lines.FirstOrDefault(l => l.LineId == busLine.LineId) != null)
                throw new BadBusLineIdException(busLine.LineNumber, "Duplicate bus Line number");
            DataSource.lines.Add(busLine.Clone());
        }

        public void UpdateBusLine(BusLine busLine)
        {
            BusLine line = DataSource.lines.FirstOrDefault(l => l.LineNumber == busLine.LineNumber);
            if (line != null)
            {
                DataSource.lines.Remove(line);
                DataSource.lines.Add(busLine.Clone());
            }
            else
                throw new BadBusLineIdException(busLine.LineNumber, $"bad Bus Line Id: {busLine.LineNumber}");
        }

        public void UpdateBusLine(int lineId, Action<BusLine> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBusLine(int lineId)
        {
            BusLine line = DataSource.lines.FirstOrDefault(l => l.LineId == lineId);
            if (line != null)
            {
                DataSource.lines.Remove(line);
            }
            else
                throw new BadBusLineIdException(lineId, $"bad Bus Line Id: {lineId}");
        }
        #endregion

        #region LineExist CRUD
        public IEnumerable<LineExist> GetAllExistsLines()
        {
            return (IEnumerable<LineExist>)(from l in DataSource.linesExists
                                            select l.Clone());
        }

        public IEnumerable<LineExist> GetAllExistsLinesBy(Predicate<LineExist> predicate)
        {
            throw new NotImplementedException();
        }

        public LineExist GetLineExist(int id)
        {
            LineExist line = DataSource.linesExists.Find(l => l.LineId == id);

            if (line != null)
                return line.Clone();
            else
                throw new BadLineExistException(id, $"bad Bus LicenceId: {id}");
        }

        public void AddLineExist(LineExist lineExist)
        {
            if (DataSource.linesExists.FirstOrDefault(l => l.LineId == lineExist.LineId) != null)
                throw new BadLineExistException(lineExist.LineId, "Duplicate  Line exist number");
            DataSource.linesExists.Add(lineExist.Clone());
        }

        public void UpdateLineExist(LineExist lineExist)
        {
            LineExist line = DataSource.linesExists.FirstOrDefault(l => l.LineId == lineExist.LineId);
            if (line != null)
            {
                DataSource.linesExists.Remove(line);
                DataSource.linesExists.Add(lineExist.Clone());
            }
            else
                throw new BadLineExistException(lineExist.LineId, $"bad Bus Line Id: {lineExist.LineId}");
        }

        public void UpdateLineExist(int lineId, Action<BusLine> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineExist(int lineId)
        {
            LineExist line = DataSource.linesExists.FirstOrDefault(l => l.LineId == lineId);
            if (line != null)
            {
                DataSource.linesExists.Remove(line);
            }
            else
                throw new BadLineExistException(lineId, $"bad Bus Line Id: {lineId}");
        }
        #endregion

        #region Pairs CRUD
        public IEnumerable<PairOfConsecutiveStation> GetAllPairs()
        {
            return (IEnumerable<PairOfConsecutiveStation>)(from pair in DataSource.pairs
                                                           select pair.Clone());
        }

        public IEnumerable<PairOfConsecutiveStation> GetAllPairsBy(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        public PairOfConsecutiveStation GetPair(int id1, int id2)
        {
            PairOfConsecutiveStation pair = DataSource.pairs.Find(p => p.StationId1 == id1 && p.StationId2 == id2);

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
            DO.PairOfConsecutiveStation pair= new PairOfConsecutiveStation { StationId1 = id1, StationId2 = id2, Distance = distance, AverageTravleTime = time };
            DataSource.pairs.Add(pair);
        }

        public void UpdatePair(PairOfConsecutiveStation pair)
        {
            PairOfConsecutiveStation nPair = DataSource.pairs.FirstOrDefault(p => p.StationId1 == pair.StationId1 && p.StationId2==pair.StationId2);
            if (nPair != null)
            {
                DataSource.pairs.Remove(nPair);
                DataSource.pairs.Add(pair.Clone());
            }
            else
                throw new BadPairIdException(pair.StationId1, pair.StationId2,
                    $"bad  pair:  Id1: {pair.StationId1}, id2{pair.StationId2}");
        }

        public void UpdatePair(int id, Action<PairOfConsecutiveStation> update)
        {
            throw new NotImplementedException();
        }

        public void DeletePair(int id1, int id2)
        {
            PairOfConsecutiveStation pair = DataSource.pairs.Find(p => p.StationId1 == id1 && p.StationId2 == id2);

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
                DataSource.stations.Remove(nStation);
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
                return null;
            //throw new BadBusLicenceIdException(id, $"bad Bus LicenceId: {id}");
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
              
            foreach(var s in DataSource.stationsLine)
            {
                if (s.LineId == lineId && s.NumInLine > station.NumInLine)
                    s.NumInLine--;
            }
            DataSource.stationsLine.Remove(station);
        }
        #endregion

        #region TravelBus CRUD
        public IEnumerable<TravelBus> GetAllTravelBuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TravelBus> GetAllTravelBusesLineBy(Predicate<TravelBus> predicate)
        {
            throw new NotImplementedException();
        }

        public Station GetTravelBus(int id)
        {
            throw new NotImplementedException();
        }

        public void AddTravelBus(TravelBus travelBus)
        {
            throw new NotImplementedException();
        }

        public void UpdateTravelBus(TravelBus travelBus)
        {
            throw new NotImplementedException();
        }

        public void UpdateTravelBus(int id, Action<TravelBus> update)
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
    }
}
