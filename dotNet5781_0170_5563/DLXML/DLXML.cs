using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;

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
        string LineStationPath = @"LineStationXml.xml"; //XElement
        #endregion


    }
}
