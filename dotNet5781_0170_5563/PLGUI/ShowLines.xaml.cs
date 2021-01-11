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

            linesDataGrid.DataContext = s.Lines;
        }
    }
}
