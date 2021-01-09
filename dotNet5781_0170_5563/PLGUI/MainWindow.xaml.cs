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
        List<BO.BusLine> alines = new List<BO.BusLine>();
        void createBuses()
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
       
        //public ViewModel.MainWindow viewModel;
        public MainWindow()
        {
            InitializeComponent();
            createBuses();
            //lvStations.ItemsSource = lines;
            cbLineNum.ItemsSource = lines;
           // RefreshAllLinesComboBox();
            cbLineNum.DisplayMemberPath = "LineNumber";
            cbLineNum.SelectedValuePath = "LineId";
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
           // RefreshAllLinesComboBox();
            line = (cbLineNum.SelectedItem as PO.BusLine);
            if(line!=null)
            lineDataGrid.DataContext = line.Stations;
            
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
            MessageBox.Show(station.StationName);
        }

        private void btDeleteStation_Click(object sender, RoutedEventArgs e)
        {
            var v = sender as Button;
            PO.StationLine station =v.DataContext as PO.StationLine;

            //MessageBox.Show(station.StationName);
            MessageBoxResult res = MessageBox.Show("Delete selected Station?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;

            try
            {
                if (line != null && station !=null)
                {
                    bl.DeleteStationLine(line.LineId, station.StationId);
                    BO.BusLine b = new BusLine();
                   // b=bl.get
                    line.Stations.Remove(station);
                    //lineDataGrid.DataContext = lines;
                    if(station.NumInLine==1)
                    {
                        BO.BusLine l=new BO.BusLine();
                        line.DeepCopyTo(l);
                        bl.UpdateBusLine(l);

                    }
                    if (station.NumInLine == line.Stations.Count)
                    {
                        BO.BusLine l = new BO.BusLine();
                        line.DeepCopyTo(l);
                        bl.UpdateBusLine(l);

                    }
                    //createBuses();
                    //RefreshAllLinesComboBox();
                  //  DataGrid d = lineDataGrid;
                    //d.DataContext = line.Stations;
                }

            }
            catch (Exception EX) { }
        }

        private void btDeleteLine_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Delete selected Line?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;
            BO.BusLine l = new BO.BusLine();
            try
            {
                if (line != null)
                {
                    line.DeepCopyTo(l);
                    bl.DeleteBusLine(l);
                    lines.Remove(line);
                    RefreshAllLinesComboBox();                                                    
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
           // var v = sender as Button;
           // PO.BusLine line =v.DataContext as PO.BusLine;
            //MessageBox.Show(line.LineNumber.ToString());
            DataGrid d = new DataGrid();
            d = lineDataGrid;
            AddStationToLine add = new AddStationToLine(bl, line ,d);
            add.Show();
            //var addStopLine = new addStopLine(bl, Lines, ListViewStopsOfLine)
            //{
            //    DataContext = ListViewLines.SelectedItem
            //};
            //addStopLine.ShowDialog();
        }

        private void cbLineNum_Scroll(object sender, ScrollEventArgs e)
        {
            RefreshAllLinesComboBox();
        }

        //private void cbStudentID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    curStu = (cbStudentID.SelectedItem as BO.Student);
        //    gridOneStudent.DataContext = curStu;

        //    if (curStu != null)
        //    {
        //        //list of courses of selected student
        //        RefreshAllRegisteredCoursesGrid();
        //        //list of all courses (that selected student is not registered to it)
        //        RefreshAllNotRegisteredCoursesGrid();
        //    }
        //}

        //private void btUpdateStudent_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (curStu != null)
        //            bl.UpdateStudentPersonalDetails(curStu);
        //    }
        //    catch (BO.BadStudentIdException ex)
        //    {
        //        MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private void btDeleteStudent_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBoxResult res = MessageBox.Show("Delete selected student?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //    if (res == MessageBoxResult.No)
        //        return;

        //    try
        //    {
        //        if (curStu != null)
        //        {
        //            bl.DeleteStudent(curStu.ID);
        //            BO.Student stuToDel = curStu;

        //            RefreshAllRegisteredCoursesGrid();
        //            RefreshAllNotRegisteredCoursesGrid();
        //            RefreshAllStudentComboBox();
        //        }
        //    }
        //    catch (BO.BadStudentIdCourseIDException ex)
        //    {
        //        MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    catch (BO.BadStudentIdException ex)
        //    {
        //        MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
    }
    }

