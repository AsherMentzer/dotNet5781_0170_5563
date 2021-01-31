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
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

namespace PLGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Stopwatch watch = new Stopwatch();
        #region get the data
        IBL bl = BLFactory.GetBL("1");
        void GetAllStations()
        {
            var v = bl.GetAllStations();
        }
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
        #endregion
        BackgroundWorker Simulatorworker;
        BackgroundWorker Operatorworker;
        int rate;
        TimeSpan startTime;
        string userName, password;
        ObservableCollection<BO.LineTiming> linesTiming = new ObservableCollection<BO.LineTiming>();
        ObservableCollection<PO.LineStation> board = new ObservableCollection<PO.LineStation>();
        public MainWindow()
        {
            this.Closed += MainWindow_Closed;
            getAllLines();
            InitializeComponent();
           // GetAllStations();           
            cbLineNum.ItemsSource = lines;            
            cbLineNum.DisplayMemberPath = "LineNumber";
            cbLineNum.SelectedValuePath = "LineId";
            cbstations.ItemsSource = stations;
            cbstations.DisplayMemberPath = "StationId";
            getAllStations();
            stationsDataGrid.DataContext = stations;
            lvPanel.DataContext = linesTiming;
            Simulatorworker = new BackgroundWorker();
            Simulatorworker.DoWork += Simulatorworker_DoWork;
            Simulatorworker.ProgressChanged += Simulatorworker_ProgressChanged;
            Simulatorworker.WorkerReportsProgress = true;
            Simulatorworker.WorkerSupportsCancellation = true;
            Operatorworker = new BackgroundWorker();
            Operatorworker.DoWork += Operatorworker_DoWork;
            Operatorworker.ProgressChanged += Operatorworker_ProgressChanged;
            Operatorworker.WorkerReportsProgress = true;
            Operatorworker.WorkerSupportsCancellation = true;

            
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
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
                    catch (BO.BadStationIdException)
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
        int newStationId = 0;
        string newStationName;
        double lon = 0, la = 0;
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
            catch (BO.BadStationIdException newSE)
            { MessageBox.Show(newSE.Message); return; }

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
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (userName == null || password == null)
            {
                MessageBox.Show("enter all the details");
                return;
            }
            BO.User user = new BO.User();
            try
            {
                user = bl.GetUser(userName);
            }
            catch (BO.BadUSerNameException)
            {
                MessageBox.Show("Wrong User Name");
            }
            if (user.UserName == userName && user.Password == password)
            {
                enterGrid.Visibility = Visibility.Hidden;
                mainGrid.Visibility = Visibility.Visible;
                Application.Current.MainWindow.Width = 960;
                Application.Current.MainWindow.Height = 590;
                this.Left = SystemParameters.PrimaryScreenWidth - 1200;
                this.Top = SystemParameters.PrimaryScreenHeight - 700;
            }
            else
                PsPassword.BorderBrush = Brushes.Red;
        }
        private void tbUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            userName = tbUserName.Text;
        }

        private void PsPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            password = PsPassword.Password;
        }

        private void bRegister_Click(object sender, RoutedEventArgs e)
        {
            enterGrid.Visibility = Visibility.Hidden;
            registerGrid.Visibility = Visibility.Visible;
            Application.Current.MainWindow.Height = 450;
        }
        string regName, regPassword, regConfirm;
        private void tbRegName_TextChanged(object sender, TextChangedEventArgs e)
        {
            regName = tbRegName.Text;
        }

        private void RegPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            regPassword = RegPassword.Password;
        }
        private void ConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            string a = ConfirmPassword.Password;
            if (a != regPassword)
            {
                ConfirmPassword.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                regConfirm = a;
                ConfirmPassword.BorderBrush = Brushes.Green;
            }
        }
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if (regName == null || regPassword == null || regConfirm == null)
            {
                MessageBox.Show("enter all the details");
                return;
            }

            try
            {
                bl.AddUser(regName, regPassword);
                enterGrid.Visibility = Visibility.Visible;
                registerGrid.Visibility = Visibility.Hidden;
                Application.Current.MainWindow.Height = 370;
            }
            catch (BO.BadUSerNameException)
            {
                MessageBox.Show("faild to add");
            }
        }

        private void bRegcancel_Click(object sender, RoutedEventArgs e)
        {
            enterGrid.Visibility = Visibility.Visible;
            registerGrid.Visibility = Visibility.Hidden;
            Application.Current.MainWindow.Height = 370;
        }
        #endregion

        #region simulator
        TimeSpan time;
        private void bsimulator_Click(object sender, RoutedEventArgs e)
        {
            if (startTime == null || rate < 1)
            {
                MessageBox.Show("enter the details");
                return;
            }

            //string str = "Start";
            if ((string)bsimulator.Content != "Stop")
            {
                bsimulator.Content = "Stop";
                tbrate.IsEnabled = false;
                tpTime.IsEnabled = false;
                linesTiming = new ObservableCollection<BO.LineTiming>();
                simulatorGrid.Visibility = Visibility.Visible;
                Operatorworker.RunWorkerAsync();
                Simulatorworker.RunWorkerAsync();
            }
            else
            {
                bsimulator.Content = "Start";
                tbrate.IsEnabled = true;
                tpTime.IsEnabled = true;
                tpTime.SelectedTime = new DateTime(0);
                tpTime.Text = "00:00:00";
                rate = 0;
                tbrate.Text = "";
                bl.StopSimulator();
                cbstations.SelectedIndex=-1;
                cbstations.Text = "";
                dgBoard.ItemsSource = null;
                simulatorGrid.Visibility = Visibility.Hidden;
                if (Simulatorworker.IsBusy)
                    Simulatorworker.CancelAsync();
                if (Operatorworker.IsBusy)
                    Operatorworker.CancelAsync();
                linesTiming = null;
                tblast.Text = "";
                lvPanel.ItemsSource = linesTiming;
            }
        }
        private void Simulatorworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            time = new TimeSpan(0, 0, progress);
            tpTime.SelectedTime = new DateTime((long)(time.TotalSeconds * 10000000));
            TimeSpan timePast = new TimeSpan(0, 0, 5);
            if (watch.ElapsedMilliseconds > timePast.TotalMilliseconds)
                tblast.Text = "";
        }

        private void Simulatorworker_DoWork(object sender, DoWorkEventArgs e)
        {
            bl.StartSimulator(startTime, rate, GetTime);
            while (!Simulatorworker.CancellationPending)
                Thread.Sleep(1000);
        }
        void GetTime(TimeSpan a)
        {
            if (a != null)
            {
                Simulatorworker.ReportProgress((int)a.TotalSeconds);
            }
        }
        
        private void Operatorworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //if(boarsSt !=null)
            //dgBoard.ItemsSource = boarsSt.Lines;
            BO.LineTiming timing = (BO.LineTiming)e.UserState;
            BO.LineTiming temp = null;
            if (linesTiming != null)
            {
                temp = linesTiming.FirstOrDefault(x => x.LineId == timing.LineId && x.StartTime == timing.StartTime);

                //if not exist add it
                if (temp == null)
                {
                    linesTiming.Add(timing);
                    sort(ref linesTiming);//sort the panel times
                    lvPanel.ItemsSource = linesTiming;//update the panel
                }
                else if (timing.ArriveTime == TimeSpan.Zero)//the time is 0 remove it the line reach the station
                {
                    linesTiming.Remove(temp);
                    sort(ref linesTiming);
                    string lastLine = $"passed recently:\nLine: {temp.LineNumber} to: {temp.LastStationName}";
                    tblast.Text = lastLine;
                    watch.Restart();
                }
                else if (timing.ArriveTime != TimeSpan.Zero)// need to update the time to arrive
                {
                    linesTiming.Remove(temp);
                    linesTiming.Add(timing);
                    sort(ref linesTiming);
                   
                }

                for (int i = 0; i < linesTiming.Count; i++)
                {
                    if (linesTiming[i].ArriveTime == TimeSpan.Zero)
                        linesTiming.Remove(linesTiming[i]);
                }
            }
        }
        
        private void Operatorworker_DoWork(object sender, DoWorkEventArgs e)
        {
            bl.SetStationPanel(-1, UpdateLineTiming);
            while (!Operatorworker.CancellationPending)
                Thread.Sleep(1000);
        }

        private void lvPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbstations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {         
                PO.Station st = cbstations.SelectedItem as PO.Station;
                linesTiming.Clear();
                lvPanel.DataContext = linesTiming;
            if (st != null)
            {
                bl.SetStationPanel(st.StationId, UpdateLineTiming);
                foreach (var l in st.Lines)
                {
                    BO.Station name = bl.GetStation(l.LastStationId);
                    l.LastStationName = name.StationName;
                }
                dgBoard.ItemsSource = st.Lines;
            }
            else
            {
                bl.SetStationPanel(-1, UpdateLineTiming);
            }
        }

        void UpdateLineTiming(BO.LineTiming timing)
        {
            if (timing != null)
                Operatorworker.ReportProgress(1, timing);
        }

        private void tbrate_TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(tbrate.Text, out rate);
        }

        private void tpTime_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            var v = tpTime.SelectedTime;
            if (v != null && startTime != null)
                startTime = v.Value.TimeOfDay;
        }
       
        /// <summary>
        /// funtion to sort the  list for the panel show
        /// </summary>
        /// <param name="lt"></param>
        void sort(ref ObservableCollection<BO.LineTiming> lt)
        {
            for (int i = 0; i < lt.Count - 1; i++)
            {
                for (int j = 0; j < lt.Count - 1; j++)
                {
                    if (lt[j].ArriveTime > lt[j + 1].ArriveTime)
                    {
                        BO.LineTiming temp1 = lt[j];
                        lt[j] = lt[j + 1];
                        lt[j + 1] = temp1;                    
                    }
                }
            }

        }
        #endregion

    }

}

