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

namespace dotNet5781_03B_0170_5563
{
    /// <summary>
    /// Interaction logic for busDetails.xaml
    /// </summary>
    public partial class busDetails : Window
    {
        Bus currentBus;
        public busDetails(Bus bus)
        {
            InitializeComponent();
            currentBus = bus;
            ID.Text = currentBus.GetId.ToString();
            active.Text = currentBus.Active.ToShortDateString();
            KM.Text = currentBus.Kilometrage.ToString();
            fuel.Text = currentBus.Fuel.ToString();
            fixDate.Text = currentBus.LastFix.ToShortDateString();
            kmFromFix.Text = currentBus.KmForTravel.ToString();
            status.Text = currentBus.Status.ToString();
        }

    }
}
