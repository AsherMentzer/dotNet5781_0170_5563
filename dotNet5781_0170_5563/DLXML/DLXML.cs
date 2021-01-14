using DLAPI;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DL
{
    sealed class DLXML : IDL    //internal
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files
        string BusPath = @"BusXml.xml"; //XElement
        string LinePath = @"LineXml.xml"; //XElement
        string StationPath = @"StationXml.xml"; //XElement
        string UserPath = @"UserXml.xml"; //XElement
        string BusOnTripPath = @"BusOnTripXml.xml"; //XElement
        string AdjacentStationsPath = @"AdjacentStationsXml.xml"; //XElement
        string LineTripPath = @"LineTripXml.xml"; //XElement
        string TripPath = @"TripXml.xml"; //XElement
        string StationLinePath = @"LineStationXml.xml"; //XElement
        string SerialNumbersPath = @"SerialNumbersXml.xml"; //XElement

        #endregion

        //empty
        #region Bus
        public IEnumerable<Bus> GetAllBuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public Bus GetBus(string licenseId)
        {
            throw new NotImplementedException();
        }

        public void AddBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(string licenceId, Action<Bus> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(string licenceId)
        {
            throw new NotImplementedException();
        }
        #endregion
        //full with linq to xml
        #region Line
        public IEnumerable<Line> GetAllBusLines()
        {
            XElement LineRootElem = XMLTools.LoadListFromXMLElement(LinePath);

            return (from l in LineRootElem.Elements()
                    select new Line()
                    {
                        LineId = int.Parse(l.Element("LineId").Value),
                        LineNumber = int.Parse(l.Element("LineNumber").Value),
                        FirstStation = int.Parse(l.Element("FirstStation").Value),
                        LastStation = int.Parse(l.Element("LastStation").Value),
                        area = (Areas)Enum.Parse(typeof(Areas), l.Element("area").Value)
                    });
        }

        public IEnumerable<Line> GetAllBusLinesBy(Predicate<Line> predicate)
        {
            XElement LineRootElem = XMLTools.LoadListFromXMLElement(LinePath);

            return (from l in LineRootElem.Elements()
                    let l1 = new Line()
                    {
                        LineId = int.Parse(l.Element("LineId").Value),
                        LineNumber = int.Parse(l.Element("LineNumber").Value),
                        FirstStation = int.Parse(l.Element("FirstStation").Value),
                        LastStation = int.Parse(l.Element("LastStation").Value),
                        area = (Areas)Enum.Parse(typeof(Areas), l.Element("area").Value)
                    }
                    where predicate(l1)
                    select l1);
        }

        public Line GetBusLine(int lineId)
        {
            XElement LineRootElem = XMLTools.LoadListFromXMLElement(LinePath);

            Line line = (from l in LineRootElem.Elements()
                         where int.Parse(l.Element("LineId").Value) == lineId
                         select new Line()
                         {
                             LineId = int.Parse(l.Element("LineId").Value),
                             LineNumber = int.Parse(l.Element("LineNumber").Value),
                             FirstStation = int.Parse(l.Element("FirstStation").Value),
                             LastStation = int.Parse(l.Element("LastStation").Value),
                             area = (Areas)Enum.Parse(typeof(Areas), l.Element("area").Value)
                         }
                        ).FirstOrDefault();

            if (line == null)
                throw new DO.BadBusLineIdException(lineId, $"bad line id: {lineId}");

            return line;
        }

        public void AddBusLine(Line busLine)
        {
            XElement lineRootElem = XMLTools.LoadListFromXMLElement(LinePath);

            XElement SerialNum = XMLTools.LoadListFromXMLElement(SerialNumbersPath);
            int id = int.Parse(SerialNum.Element("LineId").Value) + 1;            
            SerialNum.Element("LineId").Value = (int.Parse(SerialNum.Element("LineId").Value) + 1).ToString();
            SerialNum.Save(SerialNumbersPath);

                XElement bl = new XElement("Line",
                    new XElement("LineId", id),
                    new XElement("LineNumber", busLine.LineNumber), 
                    new XElement("FirstStation", busLine.FirstStation), 
                    new XElement("LastStation", busLine.LastStation), 
                    new XElement("area", busLine.area.ToString())
                    );

            lineRootElem.Add(bl);
            XMLTools.SaveListToXMLElement(lineRootElem, LinePath);
        }

    public void UpdateBusLine(Line busLine)
        {
            XElement LineRootElem = XMLTools.LoadListFromXMLElement(LinePath);

            XElement line = (from l in LineRootElem.Elements()
                             where int.Parse(l.Element("LineId").Value) == busLine.LineId
                             select l).FirstOrDefault();

            if (line != null)
            {
                line.Element("LineId").Value = busLine.LineId.ToString();
                line.Element("LineNumber").Value = busLine.LineNumber.ToString();
                line.Element("FirstStation").Value = busLine.FirstStation.ToString();
                line.Element("LastStation").Value = busLine.LastStation.ToString();
                line.Element("area").Value = busLine.area.ToString();
               
                XMLTools.SaveListToXMLElement(LineRootElem, LinePath);
            }
            else
                throw new DO.BadBusLineIdException(busLine.LineId, $"bad person id: {busLine.LineId}");
        }

        public void UpdateBusLine(int lineId, Action<Line> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBusLine(int lineId)
        {
            XElement LineRootElem = XMLTools.LoadListFromXMLElement(LinePath);

            XElement line = (from l in LineRootElem.Elements()
                             where int.Parse(l.Element("LineId").Value) == lineId
                             select l).FirstOrDefault();

            if(line != null)
            {
                line.Remove();
                XMLTools.SaveListToXMLElement(LineRootElem, LinePath);
            }
            else
                throw new DO.BadBusLineIdException(lineId, $"bad person id: {lineId}");
        }
        #endregion
        //full
        #region Station
        public IEnumerable<Station> GetAllStations()
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);
            return from station in ListStations
                   select station;
        }

        public IEnumerable<Station> GetAllStationsBy(Predicate<Station> predicate)
        {
            XElement StationRootElem = XMLTools.LoadListFromXMLElement(StationPath);
           return  from st in StationRootElem.Elements()
                   let station = new Station()
                   {
                       StationId=int.Parse(st.Element("StationId").Value),
                       Latitude=Double.Parse(st.Element("Latitude").Value),
                       Longitude = Double.Parse(st.Element("Longitude").Value),
                       StationName =st.Element("StationName").Value
                    }
                    where predicate(station)
                    select station;
        }

        public Station GetStation(int id)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);

            DO.Station st = ListStations.Find(s => s.StationId == id);
            if (st != null)
                return st; //no need to Clone()
            else
                throw new DO.BadStationIdException(id, $"bad station id: {id}");
        }

        public void AddStation(Station station)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);

            if (ListStations.FirstOrDefault(s => s.StationId == station.StationId) != null)
                throw new DO.BadStationIdException(station.StationId, "Duplicate statio ID");

            ListStations.Add(station); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListStations, StationPath);
        }

        public void UpdateStation(Station station)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);

            Station st = ListStations.Find(s => s.StationId == station.StationId);


            if (st != null)
            {
                ListStations.Remove(st);
                ListStations.Add(station); //no nee to clone()
            }
            else
                throw new DO.BadStationIdException(station.StationId, $"bad student id: {station.StationId}");

            XMLTools.SaveListToXMLSerializer(ListStations, StationPath);
        }

        public void UpdateStation(int id, Action<Station> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int id)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);

            Station st = ListStations.Find(s => s.StationId == id);


            if (st != null)
            {
                ListStations.Remove(st);
            }
            else
                throw new DO.BadStationIdException(id, $"bad student id: {id}");

            XMLTools.SaveListToXMLSerializer(ListStations, StationPath);
        }
        #endregion
        //empty
        #region LineTrip
        public IEnumerable<LineTrip> GetAllExistsLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineTrip> GetAllExistsLinesBy(Predicate<LineTrip> predicate)
        {
            throw new NotImplementedException();
        }

        public LineTrip GetLineExist(int lineId)
        {
            throw new NotImplementedException();
        }

        public void AddLineExist(LineTrip lineExist)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineExist(LineTrip lineExist)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineExist(int lineId, Action<Line> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineExist(int lineId)
        {
            throw new NotImplementedException();
        }
        #endregion
        //full
        #region AdjacentStations     
        public IEnumerable<AdjacentStations> GetAllPairs()
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(AdjacentStationsPath);
            return from ad in ListAdjacentStations
                   select ad;
        }

        public IEnumerable<AdjacentStations> GetAllPairsBy(Predicate<AdjacentStations> predicate)
        {
            XElement AdjacentStationsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);
            return from st in AdjacentStationsRootElem.Elements()
                   let pair = new AdjacentStations()
                   {
                       StationId1= int.Parse(st.Element("StationId1").Value),
                       StationId2= int.Parse(st.Element("StationId2").Value),
                       Distance = Double.Parse(st.Element("Distance").Value),
                      AverageTravleTime=new TimeSpan(),//--------------------------------need to fix
                   }
                   where predicate(pair)
                   select pair;
        }

        public AdjacentStations GetPair(int id1, int id2)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(AdjacentStationsPath);
            DO.AdjacentStations ad = ListAdjacentStations.Find(a => a.StationId1 == id1 && a.StationId2==id2);
            if (ad != null)
                return ad; //no need to Clone()
            else
                throw new DO.BadPairIdException(id1,id2, $"bad AdjacentStations id's: {id1} to: {id2}");
        }

        public void AddPair(int id1, int id2, double distance, TimeSpan time)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(AdjacentStationsPath);
            if( ListAdjacentStations.Find(a => a.StationId1 == id1 && a.StationId2 == id2)!=null)
                throw new DO.BadPairIdException(id1, id2, $"Duplicate AdjacentStations id's: {id1} to: {id2}");
           
            if (GetStation(id1) == null)
                throw new DO.BadStationIdException(id1, "Missing station ID");

            if (GetStation(id2) == null)
                throw new DO.BadStationIdException(id2, "Missing station ID");

            DO.AdjacentStations ad = new AdjacentStations()
            {
                StationId1 = id1,
                StationId2 = id2,
                Distance = distance,
                AverageTravleTime = time
            };
            ListAdjacentStations.Add(ad); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, AdjacentStationsPath);
        }

        public void UpdatePair(AdjacentStations pair)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(AdjacentStationsPath);
            DO.AdjacentStations ad = ListAdjacentStations.Find(a => a.StationId1 == pair.StationId1 && a.StationId2 == pair.StationId2);
            if (ad != null)
            {
                ListAdjacentStations.Remove(ad);
                ListAdjacentStations.Add(pair); //no nee to clone()
            }
            else
                throw new DO.BadPairIdException(pair.StationId1,pair.StationId2,
                    $"bad AdjacentStations id's: {pair.StationId1} to: {pair.StationId2}");

            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, AdjacentStationsPath);
        }

        public void UpdatePair(int id, Action<AdjacentStations> update)
        {
            throw new NotImplementedException();
        }

        public void DeletePair(int id1, int id2)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(AdjacentStationsPath);
            DO.AdjacentStations ad = ListAdjacentStations.Find(a => a.StationId1 == id1 && a.StationId2 == id2);
            if (ad != null)
            {
                ListAdjacentStations.Remove(ad);
            }
            else
                throw new DO.BadPairIdException(id1, id2, $"bad AdjacentStations id's: {id1} to: {id2}");

            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, AdjacentStationsPath);
        }
        #endregion
       //full
        #region StationLine
        public IEnumerable<StationLine> GetAllStationsLine()
        {
            List<StationLine> ListStationsLine = XMLTools.LoadListFromXMLSerializer<StationLine>(StationLinePath);
            return from station in ListStationsLine
                   select station;
        }

        public IEnumerable<StationLine> GetAllStationsLineBy(Predicate<StationLine> predicate)
        {
            XElement StationLineRootElem = XMLTools.LoadListFromXMLElement(StationLinePath);
            return from st in StationLineRootElem.Elements()
                   let station = new StationLine()
                   {
                       LineId = int.Parse(st.Element("LineId").Value),
                       NumInLine= int.Parse(st.Element("NumInLine").Value),
                       StationId = int.Parse(st.Element("StationId").Value),
                   }
                   where predicate(station)
                   select station;
        }

        public StationLine GetStationLine(int lineId, int stationId)
        {
            List<StationLine> ListStationsLine = XMLTools.LoadListFromXMLSerializer<StationLine>(StationLinePath);

            DO.StationLine st = ListStationsLine.Find(s => s.LineId==lineId && s.StationId == stationId);
            if (st != null)
                return st; //no need to Clone()
            else
                throw new DO.BadStatioLinenIdException(lineId,stationId,  $"bad stationLine id: station id{stationId} at line: {lineId}");
        }

        public StationLine GetStationLineBy(int lineId, int numInLine)
        {
            List<StationLine> ListStationsLine = XMLTools.LoadListFromXMLSerializer<StationLine>(StationLinePath);

            DO.StationLine st = ListStationsLine.Find(s => s.LineId == lineId && s.NumInLine==numInLine);
            if (st != null)
                return st; //no need to Clone()
            else
                throw new DO.BadStatioLinenIdException(lineId, numInLine, $"bad stationLine id: station index:{numInLine} at line: {lineId}");

        }

        public void AddStationLine(StationLine stationLine)
        {
            List<StationLine> ListStationsLine = XMLTools.LoadListFromXMLSerializer<StationLine>(StationLinePath);

            if (ListStationsLine.FirstOrDefault(s => s.LineId == stationLine.LineId && s.StationId == stationLine.StationId) != null)
                throw new DO.BadStatioLinenIdException(stationLine.LineId,
                    stationLine.StationId, "Duplicate statio ID station id{ stationLine.StationId} at line: {stationLine.LineId}");

            if (GetBusLine(stationLine.LineId) == null)
                throw new DO.BadBusLineIdException(stationLine.LineId, "Missing Line ID");

            if (GetStation(stationLine.StationId) == null)
                throw new DO.BadStationIdException(stationLine.StationId, "Missing station ID");

            ListStationsLine.Add(stationLine);

            XMLTools.SaveListToXMLSerializer(ListStationsLine, StationLinePath);
        }

        public void UpdateStationLine(StationLine stationLine)
        {
            List<StationLine> ListStationsLine = XMLTools.LoadListFromXMLSerializer<StationLine>(StationLinePath);
            DO.StationLine st = ListStationsLine.Find(s => s.LineId == stationLine.LineId && s.StationId == stationLine.StationId);
            if (st != null)
            {
                ListStationsLine.Remove(st);
                ListStationsLine.Add(stationLine); //no nee to clone()
            }
            else
                throw new DO.BadStatioLinenIdException(stationLine.LineId, stationLine.StationId,
                    $"bad stationLine id: station id{stationLine.StationId} at line: {stationLine.LineId}");

            XMLTools.SaveListToXMLSerializer(ListStationsLine, StationLinePath);
        }

        public void UpdateStationLine(int id, Action<StationLine> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteStationLine(int id, int sId)
        {
            List<StationLine> ListStationsLine = XMLTools.LoadListFromXMLSerializer<StationLine>(StationLinePath);
            DO.StationLine st = ListStationsLine.Find(s => s.LineId == id && s.StationId == sId);
            if (st != null)
            {
                ListStationsLine.Remove(st);
            }
            else
                throw new DO.BadStatioLinenIdException(id, sId, $"bad stationLine id: station id{sId} at line: {id}");

            XMLTools.SaveListToXMLSerializer(ListStationsLine, StationLinePath);
        }
        #endregion ///full
        //empty
        #region BusOnTrip
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
        #endregion


    }
}
