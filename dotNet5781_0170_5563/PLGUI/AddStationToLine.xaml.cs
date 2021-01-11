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

using BLAPI;
using ViewModel;
namespace PLGUI
{

    /// <summary>
    /// Interaction logic for AddStationToLine.xaml
    /// </summary>
    public partial class AddStationToLine : Window
    {
        PO.BusLine line = new PO.BusLine();
        DataGrid d = new DataGrid();
        IBL bl;
        int index, id, mp, sp, mn, sn;
        double disPrev, disNext;
        TimeSpan tPrev, tNext;
        BO.Station s = new BO.Station();
        BO.PairOfConsecutiveStation p = new BO.PairOfConsecutiveStation();
        BO.PairOfConsecutiveStation p1 = new BO.PairOfConsecutiveStation();
        IEnumerable<BO.Station> stations;

        public AddStationToLine(IBL _bl, PO.BusLine _line, DataGrid _d)
        {

            d = _d;
            bl = _bl;
            line = _line;
            InitializeComponent();
            stations = bl.GetAllStations();
            cbStationId.ItemsSource = stations;
            cbStationId.DisplayMemberPath = "StationId";
            List<int> ab = new List<int>();
            for (int i = 0; i < 60; ++i)
            {
                ab.Add(i);

            }
            cbMFromPrev.ItemsSource = ab;
            cbSFromPrev.ItemsSource = ab;

            cbMForNext.ItemsSource = ab;
            cbSForNext.ItemsSource = ab;

        }

        private void tbStationIndexNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            var a = sender as TextBox;
            int.TryParse(a.Text, out index);

        }

        private void cbStationId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var v = sender as ComboBox;
            s = cbStationId.SelectedItem as BO.Station;
            id = s.StationId;
            int id1;
            if (index != 1)
            {
                id1 = line.Stations[index - 2].StationId;

                try
                {
                    p = bl.GetPair(id1, id);
                }
                catch (BO.BadPairIdException ex)
                {
                    tbDistancefromPrev.IsEnabled = true;
                    cbMFromPrev.IsEnabled = true;
                    cbSFromPrev.IsEnabled = true;
                }
            }
            //check is not the last station so no next station
            if (index - 1 != line.Stations.Count)
            {
                id1 = line.Stations[index - 1].StationId;
                try
                {
                    p1 = bl.GetPair(id, id1);
                }
                catch (BO.BadPairIdException ex)
                {
                    tbDistanceforNext.IsEnabled = true;
                    cbMForNext.IsEnabled = true;
                    cbSForNext.IsEnabled = true;
                }
            }
        }

        private void cbMFromPrev_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = sender as ComboBox;
            mp = (int)cbMFromPrev.SelectedItem;
        }

        private void cbSFromPrev_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = sender as ComboBox;
            sp = (int)cbSFromPrev.SelectedItem;
        }


        private void cbMForNext_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = sender as ComboBox;
            mn = (int)cbMForNext.SelectedItem;
        }

        private void cbSForNext_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = sender as ComboBox;
            sn = (int)cbSForNext.SelectedItem;
        }

        private void tbDistancefromPrev_TextChanged(object sender, TextChangedEventArgs e)
        {
            var a = sender as TextBox;
            Double.TryParse(a.Text, out disPrev);
        }

        private void tbDistanceforNext_TextChanged(object sender, TextChangedEventArgs e)
        {
            var a = sender as TextBox;
            Double.TryParse(a.Text, out disNext);
        }
        private void bAddStation_Click(object sender, RoutedEventArgs e)
        {
            if (p.StationId1 != 0)
            {
                disPrev = p.Distance;
                tPrev = p.AverageTravleTime;
            }
            else
                tNext = new TimeSpan(0, mn, sn);


            if (p1.StationId1 != 0)
            {
                disNext = p1.Distance;
                tNext = p1.AverageTravleTime;
            }
            else
                tPrev = new TimeSpan(0, mp, sp);

          
            try
            {
                bl.AddStationLine(line.LineId, s.StationId, index);
            }
            catch(BO. BadBusLineIdException)
            {
                MessageBox.Show("the index is out of limits");
            }
                //update all other stations
                for (int i = index - 1; i < line.Stations.Count; ++i)
            {
                line.Stations[i].NumInLine++;
                BO.StationLine news = new BO.StationLine();
                line.Stations[i].DeepCopyTo(news);
                bl.UpdateStationLine(news);
            }
           
            line.Stations[index - 2].AverageTravleTime = tPrev;
            line.Stations[index - 2].Distance = disPrev;
            BO.StationLine st = new BO.StationLine()
            {
                LineId = line.LineId,
                StationId = s.StationId,
                StationName = s.StationName,
                NumInLine = index,
                AverageTravleTime = tNext,
                Distance = disNext
            };
            PO.StationLine ns = new PO.StationLine();
            st.DeepCopyTo(ns);
            line.Stations.Insert(index - 1, ns);
            d.DataContext = line.Stations;
            bl.AddStationLine(st.LineId, st.StationId, st.NumInLine);
            this.Close();
        }
    }
}
