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
        List<BO.BusLine> alines = new List<BO.BusLine>();
        void getAllLines()
        {
            foreach (BO.BusLine b in bl.GetAllBusLines())
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
            foreach ( var item in bl.GetAllStations())
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

        //public ViewModel.MainWindow viewModel;
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
            //viewModel = new ViewModel.MainWindow();
            //viewModel.Reset();
            //DataContext = viewModel;
        }


        void RefreshAllLinesComboBox()
        {
            cbLineNum.ItemsSource = lines;
            // lines = (ObservableCollection<BusLine>)bl.GetAllBusLines();
            // lines = bl.GetAllBusLines().ToList(); //ObserListOfStudents;
        }

        //void RefreshAllRegisteredCoursesGrid()
        //{
        //    studentCourseDataGrid.DataContext = bl.GetAllCoursesPerStudent(curStu.ID);
        //}

        //void RefreshAllNotRegisteredCoursesGrid()
        //{
        //    List<BO.Course> listOfUnRegisteredCourses = bl.GetAllCourses().Where(c1 => bl.GetAllCoursesPerStudent(curStu.ID).All(c2 => c2.ID != c1.ID)).ToList();
        //    courseDataGrid.DataContext = listOfUnRegisteredCourses;
        //}


        private void lbBuses_Scroll(object sender, ScrollEventArgs e)
        {

        }



        private void cbLineNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            line = cbLineNum.SelectedItem as PO.BusLine;
            if (line != null)
            {
                BO.BusLine b = new BusLine();
                b = bl.GetBusLine(line.LineId);
                b.DeepCopyTo(line);
                lineDataGrid.DataContext = line.Stations;

            }
            //if (line != null)
            //{
            //    //list of courses of selected student
            //    RefreshAllRegisteredCoursesGrid();
            //    //list of all courses (that selected student is not registered to it)
            //    RefreshAllNotRegisteredCoursesGrid();
            //}
        }

        private void btUpdateStation_Click(object sender, RoutedEventArgs e)
        {
            var v = sender as Button;
            PO.StationLine station = v.DataContext as PO.StationLine;
            BO.PairOfConsecutiveStation pair = new BO.PairOfConsecutiveStation();
            if (station != null)
            {
                pair.StationId1 = station.StationId;
                pair.StationId2 = line.Stations[station.NumInLine].StationId;
                updateStation up = new updateStation(pair, station.NumInLine - 2);
                up.ShowDialog();

                bl.UpdatePair(pair);

                BO.BusLine b = new BusLine();
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

            //try
            //{
            if (line != null && station != null)
            {
                bl.DeleteStationLine(line.LineId, station.StationId);
                BO.BusLine b = new BusLine();
                BO.PairOfConsecutiveStation pair = new BO.PairOfConsecutiveStation();
                PO.BusLine p = new PO.BusLine();
                //line.Stations.Remove(station);

                try
                {
                    b = bl.GetBusLine(line.LineId);
                    b.DeepCopyTo(p);
                }
                catch (BO.BadPairIdException ex)
                {
                    // line = p;
                    //  int dis = 0;
                    // TimeSpan time = new TimeSpan();
                    updateStation up = new updateStation(pair, station.NumInLine - 2);
                    up.ShowDialog();

                    int index = station.NumInLine;
                    line.Stations[index - 2].Distance = pair.Distance;
                    line.Stations[index - 2].AverageTravleTime = pair.AverageTravleTime;
                    int id1 = line.Stations[index - 2].StationId;
                    int id2 = line.Stations[index].StationId;

                    bl.AddPair(id1, id2, pair.Distance, pair.AverageTravleTime);

                    // line.Stations.RemoveAt(station.NumInLine - 1);
                    b = bl.GetBusLine(line.LineId);
                    b.DeepCopyTo(p);

                }

                line = p;
                DataGrid d = lineDataGrid;
                d.DataContext = line.Stations;
                if (station.NumInLine == 1)
                {

                    b.FirstStation = line.Stations[1].StationId;
                    //BO.BusLine l=new BO.BusLine();
                    //line.DeepCopyTo(l);
                    bl.UpdateBusLine(b);

                }
                if (station.NumInLine == line.Stations.Count)
                {
                    //BO.BusLine l = new BO.BusLine();
                    //line.DeepCopyTo(l);
                    b.LastStation = line.Stations[line.Stations.Count - 2].StationId;
                    bl.UpdateBusLine(b);

                }
            }


            //}
            //catch (Exception EX) { }
        }

        private void btDeleteLine_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Delete selected Line?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;
            BO.BusLine l = new BO.BusLine();
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
            AddLine add = new AddLine(lines);
            add.Show();
            RefreshAllLinesComboBox();
        }

        private void btAddStation_Click(object sender, RoutedEventArgs e)
        {

            DataGrid d = new DataGrid();
            d = lineDataGrid;
            AddStationToLine add = new AddStationToLine(bl, line, d);
            add.Show();

        }

        private void cbLineNum_Scroll(object sender, ScrollEventArgs e)
        {
            RefreshAllLinesComboBox();
        }

        private void stationsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var v = stationsDataGrid.SelectedItem;
            PO.Station station = v as PO.Station;
        }
    }
}

