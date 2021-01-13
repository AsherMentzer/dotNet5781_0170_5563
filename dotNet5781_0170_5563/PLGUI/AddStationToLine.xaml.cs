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
        int index = 0, id, id1, mp, sp, mn, sn;
        double disPrev = 0, disNext = 0;
        TimeSpan tPrev, tNext;
        bool flag = false;
        BO.Station s = new BO.Station() { StationId = 0 };
        BO.AdjacentStations p = new BO.AdjacentStations();
        BO.AdjacentStations p1 = new BO.AdjacentStations();
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
            cbMFrom.ItemsSource = ab;
            cbSFrom.ItemsSource = ab;
        }
        #region select station and index
        private void tbStationIndexNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            var a = sender as TextBox;
            int.TryParse(a.Text, out index);

        }

        private void cbStationId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var v = sender as ComboBox;
            s = cbStationId.SelectedItem as BO.Station;
        }
     

        private void bAddStation_Click(object sender, RoutedEventArgs e)
        {
            //check if all fields are full
            if (index == 0 || s.StationId == 0)
            {
                MessageBox.Show("enter all the details");
                return;
            }

            try
            {
                bl.AddStationLine(line.LineId, s.StationId, index);
            }
            catch (IndexOutOfRangeException indexEX)
            {
                MessageBox.Show(indexEX.Message);
                return;
            }
            catch (BO.BadStationIdException exe)
            {
                MessageBox.Show(exe.Message);
                return;
                //MessageBox.Show("the index is out of limits");
            }
            catch (BO.BadPairIdException exe)
            {
                if (exe.station2ID == s.StationId)
                {
                    enterGrid.Visibility = Visibility.Hidden;
                    Update2Grid.Visibility = Visibility.Visible;
                    id = exe.station1ID;
                    id1 = exe.station3ID;
                }
                else if (exe.station1ID == s.StationId)
                {
                    enterGrid.Visibility = Visibility.Hidden;
                    Update1Grid.Visibility = Visibility.Visible;
                    id = exe.station1ID;
                    flag = true;
                }
                else if (exe.station2ID == s.StationId)
                {
                    enterGrid.Visibility = Visibility.Hidden;
                    Update1Grid.Visibility = Visibility.Visible;
                    id = exe.station3ID;

                    return;
                }
                //update the presentation
                BO.Line b = bl.GetBusLine(line.LineId);
                b.DeepCopyTo(line);
                d.DataContext = line.Stations;
                this.Close();
            }

        }
        #endregion
        #region 2update
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
        private void b2Update_Click(object sender, RoutedEventArgs e)
        {
            tPrev = new TimeSpan(0, mp, sp);
            tNext = new TimeSpan(0, mn, sn);
            if (disPrev == 0 || tPrev.TotalSeconds == 0 || disNext == 0 || tNext.TotalSeconds == 0)
            {
                MessageBox.Show("enter all the details");
                return;
            }
            bl.AddPair(id, s.StationId, disPrev, tPrev);
            bl.AddPair(s.StationId, id1, disPrev, tPrev);
            BO.StationLine newSt1 = bl.GetStationLine(line.LineId, id);
            bl.UpdateStationLine(newSt1);
            BO.StationLine newSt2 = bl.GetStationLine(line.LineId, id1);
            bl.UpdateStationLine(newSt2);

            //update the presentation
            BO.Line b = bl.GetBusLine(line.LineId);
            b.DeepCopyTo(line);
            d.DataContext = line.Stations;
            this.Close();
        }
        #endregion
        #region update1


        private void tbDistance_TextChanged(object sender, TextChangedEventArgs e)
        {
            var a = sender as TextBox;
            Double.TryParse(a.Text, out disPrev);
        }

        private void cbMFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = sender as ComboBox;
            mp = (int)cbMFrom.SelectedItem;
        }

        private void cbSFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = sender as ComboBox;
            sp = (int)cbSFrom.SelectedItem;
        }

        private void b1Update_Click(object sender, RoutedEventArgs e)
        {
            if (disPrev == 0 || tPrev.TotalSeconds == 0)
            {
                MessageBox.Show("enter all the details");
                return;
            }

            if (flag)
            {
                bl.AddPair(id, s.StationId, disPrev, tPrev);
                BO.StationLine newSt = bl.GetStationLine(line.LineId, id);
                bl.UpdateStationLine(newSt);
            }
            else
            {
                bl.AddPair(s.StationId, id, disPrev, tPrev);
                BO.StationLine newSt = bl.GetStationLine(line.LineId, id1);
                bl.UpdateStationLine(newSt);

            }
            //update the presentation
            BO.Line b = bl.GetBusLine(line.LineId);
            b.DeepCopyTo(line);
            d.DataContext = line.Stations;
            this.Close();
        }
        #endregion
    }
}
