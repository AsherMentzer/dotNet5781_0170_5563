using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using Data;
namespace Dal
{
    sealed class DLObject:IDal
    {
        #region singelton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }// static ctor to ensure instance init is done just before first usage
        DLObject() { } // default => private
        public static DLObject Instance { get => instance; }// The public Instance property to use
        #endregion

        //Implement IDL methods, CRUD


        public string GetLicenseId() { return null; }

        public IEnumerable<Bus> GetAllBuses()
        {
            return (IEnumerable<Bus>)(from bus in DataSource.buses
                   select bus.Clone());
        }

        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public Bus GetBus(string licenseId)
        {
            Bus bus = DataSource.buses.Find(p => p.LicenseId == licenseId);

            if (bus != null)
                return (Bus)bus.Clone();
            else
                throw new BadPersonIdException(licenseId, $"bad person id: {licenseId}");
        }

        public void AddBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(int licenceId, Action<Bus> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(int licenceId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BusLine> GetAllBusLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BusLine> GetAllBusLinesBy(Predicate<BusLine> predicate)
        {
            throw new NotImplementedException();
        }

        public BusLine GetBusLine(int lineId)
        {
            throw new NotImplementedException();
        }

        public void AddBusLine(BusLine busLine)
        {
            throw new NotImplementedException();
        }

        public void UpdateBusLine(BusLine busLine)
        {
            throw new NotImplementedException();
        }

        public void UpdateBusLine(int lineId, Action<BusLine> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBusLine(int lineId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineExist> GetAllExistsLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineExist> GetAllExistsLinesBy(Predicate<LineExist> predicate)
        {
            throw new NotImplementedException();
        }

        public BusLine GetLineExist(int lineId)
        {
            throw new NotImplementedException();
        }

        public void AddLineExist(LineExist lineExist)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineExist(LineExist lineExist)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineExist(int lineId, Action<BusLine> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineExist(int lineId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PairOfConsecutiveStation> GetAllPairs()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PairOfConsecutiveStation> GetAllPairsBy(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        public Station GetPair(int id)
        {
            throw new NotImplementedException();
        }

        public void AddPair(PairOfConsecutiveStation pair)
        {
            throw new NotImplementedException();
        }

        public void UpdatePair(PairOfConsecutiveStation pair)
        {
            throw new NotImplementedException();
        }

        public void UpdatePair(int id, Action<PairOfConsecutiveStation> update)
        {
            throw new NotImplementedException();
        }

        public void DeletePair(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetAllStations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetAllStationsBy(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int id)
        {
            throw new NotImplementedException();
        }

        public void AddStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(int id, Action<Station> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StationLine> GetAllStationsLine()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StationLine> GetAllStationsLineBy(Predicate<StationLine> predicate)
        {
            throw new NotImplementedException();
        }

        public Station GetStationLine(int id)
        {
            throw new NotImplementedException();
        }

        public void AddStationLine(StationLine stationLine)
        {
            throw new NotImplementedException();
        }

        public void UpdateStationLine(StationLine stationLine)
        {
            throw new NotImplementedException();
        }

        public void UpdateStationLine(int id, Action<StationLine> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteStationLine(int id)
        {
            throw new NotImplementedException();
        }

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
    }
}
