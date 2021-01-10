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
        IBL bl = BLFactory.GetBL("1");
        int num, fId, lId;
        BO.Areas myArea;
        ObservableCollection<PO.BusLine> lines; 
        public AddLine(ObservableCollection<PO.BusLine>busLines)
        {
            lines = busLines;
            InitializeComponent();
            tbLineNumber.MaxLength = 3;
            tbFirstStation.MaxLength = 5;
            tbLastStation.MaxLength = 5;
        }

        private void bAddLine_Click(object sender, RoutedEventArgs e)
        {
            BO.BusLine line=new BO.BusLine();
            BO.PairOfConsecutiveStation pair=new BO.PairOfConsecutiveStation();
            PO.BusLine busLine = new PO.BusLine();
            try
            {
               pair= bl.GetPair(fId, lId);
            }
            catch (BO.BadPairIdException ex)
            {
               
                //line.DeepCopyTo(busLine);
                updateStation up = new updateStation(pair, 0);
                up.ShowDialog();
                bl.AddPair(fId, lId, pair.Distance, pair.AverageTravleTime);
            }
            try
            {
                line=bl.CreateBusLine(num, fId, lId, myArea);
            }
            catch(BO.BadStationIdException ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            if (line.FirstStation>0)
            {
                PO.BusLine l = new PO.BusLine();
                line.DeepCopyTo(l);
                lines.Add(l);
            }
            this.Close();
           
        }

        private void tbArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = sender as TextBox;
            int str = int.Parse(v.Text);
            myArea = (BO.Areas)str;
        }

        private void tbLastStation_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = sender as TextBox;
            int.TryParse(v.Text, out lId);
        }

        private void tbFirstStation_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = sender as TextBox;
            int.TryParse(v.Text, out fId);
        }

        private void tbLineNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = sender as TextBox;
            int.TryParse(v.Text, out num);
        }
    }
}
