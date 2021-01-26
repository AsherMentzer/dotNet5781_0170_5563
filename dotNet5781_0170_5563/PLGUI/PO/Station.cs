using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace PLGUI
{
    namespace PO
    {
       public class Station : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
            ObservableCollection<PO.LineStation> lines = new ObservableCollection<PO.LineStation>();
            ObservableCollection<PO.MapLocation> lo = new ObservableCollection<PO.MapLocation>();
            //string loc;
             GeoCoordinate loc;
            int stationId;
            double latitude;
            double longitude;
            string stationName;
            //string location;
            public int StationId { get { return stationId; } set { if (stationId != value) { stationId = value; OnPropertyChanged(); } } }
            public double Latitude { get { return latitude; } set { if (latitude != value) { latitude = value; OnPropertyChanged(); } } }
            public double Longitude { get { return longitude; } set { if (longitude != value) { longitude = value; OnPropertyChanged(); } } }
            public string StationName { get { return stationName; } set { if (stationName != value) { stationName = value; OnPropertyChanged(); } } }
           public GeoCoordinate Location { get { GeoCoordinate str = new GeoCoordinate(latitude, longitude); return str; }set { if (loc != value) { loc = value; OnPropertyChanged(); } } }
           //public string Location { get { string str = $"{latitude} , {longitude}"; return str; }set { if (loc != value) { loc = value; OnPropertyChanged(); } } }
            public ObservableCollection<PO.LineStation> Lines { get { return lines; } set { lines = new ObservableCollection<PO.LineStation>(value); } }
            public ObservableCollection<MapLocation> Locations { get { return lo; }set{ lo = new ObservableCollection<PO.MapLocation>(value); } }
            //public IEnumerable<LineStation> lines { get; set; }
        }
        public class MapLocation
        {
            public GeoCoordinate Location { get; set; }
            // public string Name { get; set; }
        }
    }
   
}
