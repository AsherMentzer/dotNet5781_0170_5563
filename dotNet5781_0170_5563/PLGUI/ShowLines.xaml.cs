using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using BLAPI;
using ViewModel;
using Microsoft.Maps.MapControl.WPF;
using System.Globalization;
using System.Device.Location;
using PLGUI.PO;
//using System.Device.Location;
//using System.Globalization;

namespace PLGUI
{

    /// <summary>
    /// Interaction logic for ShowLines.xaml
    /// </summary>
    public partial class ShowLines : Window
    {

        PO.Station s = new PO.Station();
        PO.LineStation ls = new PO.LineStation();
        BO.Station station = new BO.Station();
        ObservableCollection<PO.LineStation> lines = new ObservableCollection<PO.LineStation>();
        IBL bl;
        public ShowLines(int id, IBL _bl)
        {

            InitializeComponent();
            //map.Center.Latitude = 32.091435;
            //map.Center.Longitude = 34.825758;
            // "32.091435,34.825758"
            bl = _bl;
            station = bl.GetStation(id);
            station.DeepCopyTo(s);
            foreach (var i in station.lines)
            {
                PO.LineStation ls = new PO.LineStation();
                i.DeepCopyTo(ls);
                s.Lines.Add(ls);
            }
            lines = s.Lines;
           PO.MapLocation m = new PO.MapLocation()
            {
                Location = s.Location,
            };
            if (m != null)
            s.Locations.Add(m);
              
            //push.Location = s.Location;
            linesDataGrid.DataContext = s.Lines;
            //map.DataContext = s.Locations;
            //map.Center.Altitude = s.Location.Altitude;
            map.Center.Latitude = s.Location.Latitude;
            map.Center.Longitude = s.Location.Longitude;
            
            //cmap.mapLayer
                //=(Microsoft.Maps.MapControl.WPF.Location) s.Location;
        }
        //public ObservableCollection<MapLocation> Locations { get; private set; }
        //public class MapLocation
        //{
        //    public GeoCoordinate Location { get; set; }
        //    // public string Name { get; set; }
        //}

    }
   
    //[ValueConversion(typeof(string), typeof(Microsoft.Maps.MapControl.WPF.Location))]
    //public class DateConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        string loc = (string)value;
    //        return loc.ToString();
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        Microsoft.Maps.MapControl.WPF.Location strValue = value as Microsoft.Maps.MapControl.WPF.Location;
    //        GeoCoordinate resultDateTime;
    //        if (GeoCoordinate.(strValue, out resultDateTime))
    //        {
    //            return resultDateTime;
    //        }
    //        return DependencyProperty.UnsetValue;
    //    }

    //    object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
