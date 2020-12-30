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
        IDL dl = DLFactory.GetDal();

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
            catch(BadBusLicenceIdException ex)
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
            throw new NotImplementedException();
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

        public IEnumerable<BO.BusLine> GetAllBusLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.BusLine> GetAllBusLinesBy(Predicate<BO.BusLine> predicate)
        {
            throw new NotImplementedException();
        }

        public BO.BusLine GetBusLine(int lineId)
        {
            throw new NotImplementedException();
        }

        public void AddBusLine(BO.BusLine busLine)
        {
            throw new NotImplementedException();
        }

        public void UpdateBusLine(BO.BusLine busLine)
        {
            throw new NotImplementedException();
        }

        public void UpdateBusLine(int lineId, Action<BO.BusLine> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBusLine(int lineId)
        {
            throw new NotImplementedException();
        }

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
            throw new NotImplementedException();
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

        public IEnumerable<BO.Station> GetAllStations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.Station> GetAllStationsBy(Predicate<BO.Station> predicate)
        {
            throw new NotImplementedException();
        }

        public BO.Station GetStation(int id)
        {
            throw new NotImplementedException();
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

        public IEnumerable<BO.StationLine> GetAllStationsLine()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.StationLine> GetAllStationsLineBy(Predicate<BO.StationLine> predicate)
        {
            throw new NotImplementedException();
        }

        public BO.Station GetStationLine(int id)
        {
            throw new NotImplementedException();
        }

        public void AddStationLine(BO.StationLine stationLine)
        {
            throw new NotImplementedException();
        }

        public void UpdateStationLine(BO.StationLine stationLine)
        {
            throw new NotImplementedException();
        }

        public void UpdateStationLine(int id, Action<BO.StationLine> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteStationLine(int id)
        {
            throw new NotImplementedException();
        }

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
