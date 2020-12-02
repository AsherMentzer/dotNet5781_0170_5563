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
    /// Interaction logic for chooseBusForTravel.xaml
    /// </summary>
    public partial class travelDistance : Window
    {
        Bus currentBus;
        public travelDistance(Bus bus)
        {
            InitializeComponent();
            currentBus = bus;
            KM.PreviewKeyDown += KM_PreviewKeyDown;
        }

        private void KM_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            double km;
            var varKm = sender as TextBox;

            double.TryParse(varKm.Text, out km);
            if (varKm == null) return;
            if (e == null) return;
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                // ------------------------ need to do checks before updating------------------------
                // update the detailes of the bus
                currentBus.Kilometrage += km;
                currentBus.KmForTravel +=  km;
                currentBus.Fuel -= km;

                e.Handled = true;
                this.Close();
                return;
            }
            
        }
    }
}

