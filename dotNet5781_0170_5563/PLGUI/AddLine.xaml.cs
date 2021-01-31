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
using System.Collections.ObjectModel;
using BLAPI;
using ViewModel;
namespace PLGUI
{  
    /// <summary>
    /// Interaction logic for AddLine.xaml
    /// </summary>
    public partial class AddLine : Window
    {
       // IBL bl = BLFactory.GetBL("1");
        int num, fId, lId;
        BO.Areas myArea;
        ObservableCollection<PO.BusLine> lines;
        IEnumerable<BO.Station> stations;

        IBL bl;
        public AddLine(ObservableCollection<PO.BusLine>busLines,IBL _bl)
        {
            bl = _bl;
            lines = busLines;
            InitializeComponent();
            tbLineNumber.MaxLength = 3;
            stations = bl.GetAllStations();
            cbFStationId.ItemsSource = stations;
            cbLStationId.ItemsSource = stations;
            cbFStationId.DisplayMemberPath = "StationId";
            cbLStationId.DisplayMemberPath = "StationId";
            cbArea.ItemsSource = Enum.GetValues(typeof(BO.Areas));
        }

        private void bAddLine_Click(object sender, RoutedEventArgs e)
        {
            BO.Line line=new BO.Line();
            BO.AdjacentStations pair=new BO.AdjacentStations();
            PO.BusLine busLine = new PO.BusLine();
            try
            {
               pair= bl.GetPair(fId, lId);
            }
            catch (BO.BadPairIdException)
            {
                updateStation up = new updateStation(pair);
                up.ShowDialog();
                bl.AddPair(fId, lId, pair.Distance, pair.AverageTravleTime);
            }
            try
            {
                //BO.Line newLine = new BO.Line()
                //{
                //    area = myArea,
                //    FirstStation = fId,
                //    LastStation = lId,
                //    LineNumber = num
                //};
                //bl.AddStationLine()
                //bl.AddBusLine(newLine);
                line=bl.CreateBusLine(num, fId, lId, myArea);
            }
            catch(BO.BadBusLineIdException ex)
            {
                MessageBox.Show(ex.Message);
            }
           catch(BO.BadStationIdException)
            {
                MessageBox.Show("faild");
            }
            if (line.FirstStation>0)
            {
                PO.BusLine l = new PO.BusLine();
                line.DeepCopyTo(l);
                lines.Add(l);
            }
            this.Close();
           
        }
        private void cbFStationId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Station s= cbFStationId.SelectedItem as BO.Station;
            fId = s.StationId;
        }

        private void cbLStationId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Station s = cbLStationId.SelectedItem as BO.Station;
            lId = s.StationId;
        }

        private void cbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Areas area = (BO.Areas)cbArea.SelectedItem;
        }
        private void tbLineNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = sender as TextBox;
            int.TryParse(v.Text, out num);
        }
    }
}
