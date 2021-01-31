using BLAPI;
using System;
using System.Collections.Generic;
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
using ViewModel;
namespace PLGUI
{
    /// <summary>
    /// Interaction logic for StationName.xaml
    /// </summary>
    public partial class StationName : Window
    {
        string newName;
        PO.Station station;
        IBL bl;
        public StationName(PO.Station s,IBL _bl)
        {
            InitializeComponent();
            station = s;
            bl = _bl;
        }

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            newName = tbname.Text;
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            if (newName == null)
            {
                MessageBox.Show("enter new name");
                return;
            }

            BO.Station s = new BO.Station();
            station.DeepCopyTo(s);
            s.StationName = newName;
            try
            {
                bl.UpdateStation(s);
                
            }
            catch(BO.BadStationIdException) { }
            station.StationName = newName;
            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
