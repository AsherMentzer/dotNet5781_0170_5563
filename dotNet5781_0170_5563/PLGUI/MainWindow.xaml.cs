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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PO;
using BLAPI;
using ViewModel;
using BO;
using BL;
using System.Windows.Controls.Primitives;
using System.Collections.ObjectModel;

namespace PLGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        IBL bl = BLFactory.GetBL("1");
        PO.BusLine line;
        ObservableCollection<PO.BusLine> lines = new ObservableCollection<PO.BusLine>();
        ObservableCollection<PO.Station> stations = new ObservableCollection<PO.Station>();
        List<BO.Line> alines = new List<BO.Line>();
        void getAllLines()
        {
            foreach (BO.Line b in bl.GetAllBusLines())
            {
                // b.DeepCopyTo(line);
                alines.Add(b);
            }
            foreach (var b in alines)
            {
                line = new PO.BusLine();
                b.DeepCopyTo(line);
                lines.Add(line);
            }

            //  alines.DeepCopyTo(lines);
        }

        void getAllStations()
        {
            foreach (var item in bl.GetAllStations())
            {
                PO.Station station = new PO.Station();
                item.DeepCopyTo(station);
                //item.lines.DeepCopyTo(station.Lines);
                foreach (var i in item.lines)
                {
                    PO.LineStation ls = new PO.LineStation();
                    i.DeepCopyTo(ls);
                    station.Lines.Add(ls);
                }
                stations.Add(station);
            }
        }

        public MainWindow()
        {
            getAllLines();
            InitializeComponent();

            //lvStations.ItemsSource = lines;
            cbLineNum.ItemsSource = lines;
            // RefreshAllLinesComboBox();
            cbLineNum.DisplayMemberPath = "LineNumber";
            cbLineNum.SelectedValuePath = "LineId";
            getAllStations();
            stationsDataGrid.DataContext = stations;

        }

        #region Lines
        void RefreshAllLinesComboBox()
        {
            cbLineNum.ItemsSource = lines;
            // lines = (ObservableCollection<BusLine>)bl.GetAllBusLines();
            // lines = bl.GetAllBusLines().ToList(); //ObserListOfStudents;
        }


        private void cbLineNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            line = cbLineNum.SelectedItem as PO.BusLine;
            if (line != null)
            {
                BO.Line b = new BO.Line();
                b = bl.GetBusLine(line.LineId);
                b.DeepCopyTo(line);
                lineDataGrid.DataContext = line.Stations;

            }
        }

        private void btUpdateStation_Click(object sender, RoutedEventArgs e)
        {
            var v = sender as Button;
            PO.StationLine station = v.DataContext as PO.StationLine;
            BO.AdjacentStations pair = new BO.AdjacentStations();
            if (station != null)
            {
                pair.StationId1 = station.StationId;
                pair.StationId2 = line.Stations[station.NumInLine].StationId;
                //updateStation up = new updateStation(pair, station.NumInLine - 2);
                updateStation up = new updateStation(pair);
                up.ShowDialog();

                bl.UpdatePair(pair);

                BO.Line b = new BO.Line();
                b = bl.GetBusLine(line.LineId);
                b.DeepCopyTo(line);
                //getAllLines();

                //foreach(var l in lines)
                //{
                //    if
                //}


                DataGrid d = lineDataGrid;
                d.DataContext = line.Stations;
            }
            else MessageBox.Show("no station");
        }

        private void btDeleteStation_Click(object sender, RoutedEventArgs e)
        {
            var v = sender as Button;
            PO.StationLine station = v.DataContext as PO.StationLine;

            //MessageBox.Show(station.StationName);
            MessageBoxResult res = MessageBox.Show("Delete selected Station?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;

            if (line != null && station != null)
            {
                BO.AdjacentStations pair = new BO.AdjacentStations();
                //BO.StationLine stationLine = new BO.StationLine();
                try
                {
                    bl.DeleteStationLine(line.LineId, station.StationId);
                }
                catch (BO.BadPairIdException ex)
                {
                    try
                    {
                        BO.StationLine stationLine = bl.GetStationLine(line.LineId, ex.station1ID);
                        updateStation up = new updateStation(pair);
                    up.ShowDialog();
                    //get the previos station that nee to update
                    
                        stationLine.Distance = pair.Distance;
                        stationLine.AverageTravleTime = pair.AverageTravleTime;
                        bl.UpdateStationLine(stationLine);
                        bl.AddPair(ex.station1ID, ex.station2ID, pair.Distance, pair.AverageTravleTime);
                    }//is first station so no need to update
                    catch(BO.BadStationIdException)
                    { }
                    }
            }

            //update the presentation
            BO.Line lineBO = bl.GetBusLine(line.LineId);
            lineBO.DeepCopyTo(line);
            DataGrid d = lineDataGrid;
            d.DataContext = line.Stations;
            
        }

        private void btDeleteLine_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Delete selected Line?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;
            BO.Line l = new BO.Line();
            PO.BusLine p = new PO.BusLine();
            line.DeepCopyTo(p);

            try
            {
                if (line != null)
                {

                    lines.Remove(line);
                    RefreshAllLinesComboBox();
                    p.CopyPropertiesTo(l);
                    bl.DeleteBusLine(l);
                }
            }
            catch (BO.BadBusLineIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cbLineNum.SelectedItem = 0;
        }

        private void btAddLine_Click(object sender, RoutedEventArgs e)
        {
            AddLine add = new AddLine(lines, bl);
            add.Show();
            RefreshAllLinesComboBox();
        }

        private void btAddStation_Click(object sender, RoutedEventArgs e)
        {

            DataGrid d = new DataGrid();
            d = lineDataGrid;
            AddStationToLine add = new AddStationToLine(bl, line, d);
            add.ShowDialog();
            if (line != null)
            {
                BO.Line b = new BO.Line();
                b = bl.GetBusLine(line.LineId);
                b.DeepCopyTo(line);
                lineDataGrid.DataContext = line.Stations;

            }

        }

        private void cbLineNum_Scroll(object sender, ScrollEventArgs e)
        {
            RefreshAllLinesComboBox();
        }
        #endregion
        #region stations
        private void stationsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var v = stationsDataGrid.SelectedItem;
            if (v != null)
            {
                PO.Station station = v as PO.Station;
                int id = station.StationId;
                ShowLines seeLines = new ShowLines(id, bl);
                seeLines.ShowDialog();
            }
        }

        private void bUpdate_Click(object sender, RoutedEventArgs e)
        {
            PO.Station station = stationsDataGrid.SelectedItem as PO.Station;
            StationName name = new StationName(station, bl);
            name.ShowDialog();
            //foreach (PO.BusLine v in lines)
            //{
            //    foreach (PO.StationLine s in v.Stations)
            //    {
            //        if (s.StationId == station.StationId)
            //            s.StationName = station.StationName;
            //    }
            //}
        }
        private void bAddStation_Click(object sender, RoutedEventArgs e)
        {
            addStationGrid.Visibility = Visibility.Hidden;
            EnterStationGrid.Visibility = Visibility.Visible;
           
        }
        #region add station details
        int newStationId=0;
        string newStationName;
        double lon=0, la=0;
        private void tbstationId_TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(tbstationId.Text, out newStationId);
        }

        private void tbStationName_TextChanged(object sender, TextChangedEventArgs e)
        {
            newStationName = tbStationName.Text;
        }

        private void tbLatitude_TextChanged(object sender, TextChangedEventArgs e)
        {
            double.TryParse(tbLatitude.Text, out la);
        }

        private void tbLongitude_TextChanged(object sender, TextChangedEventArgs e)
        {
            double.TryParse(tbLongitude.Text, out lon);
        }

        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            if (newStationId == 0 || lon == 0 || la == 0 || newStationName == null)
            {
                MessageBox.Show("enter all details");
                return;
            }
            BO.Station newS = new BO.Station()
            {
                StationId = newStationId,
                StationName = newStationName,
                Latitude = la,
                Longitude = lon
            };
            try
            {
                bl.AddStation(newS);
                PO.Station station = new PO.Station();
                newS.DeepCopyTo(station);
                stations.Add(station);
                stationsDataGrid.DataContext = stations;
                addStationGrid.Visibility = Visibility.Visible;
                EnterStationGrid.Visibility = Visibility.Hidden;
                newStationName = null;
                newStationId = 0; lon = 0; la = 0;
                tbLatitude.Text = "";
                tbLongitude.Text = "";
                tbStationName.Text = "";
                tbstationId.Text = "";
            }
            catch(BO.BadStationIdException newSE)
            { MessageBox.Show(newSE.Message);return; }

        }
        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            addStationGrid.Visibility = Visibility.Visible;
            EnterStationGrid.Visibility = Visibility.Hidden;
            newStationName = null;
            newStationId = 0; lon = 0; la = 0;
            tbLatitude.Text = "";
            tbLongitude.Text = "";
            tbStationName.Text = "";
            tbstationId.Text = "";
        }

        #endregion
        #endregion
        #region user
        private void bUser_Click(object sender, RoutedEventArgs e)
        {

        }

      

       
        private void bAdmin_Click(object sender, RoutedEventArgs e)
        {
            enterGrid.Visibility = Visibility.Hidden;
            mainGrid.Visibility = Visibility.Visible;
        }

        #endregion
        
    }
}

