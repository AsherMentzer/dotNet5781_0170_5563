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
                            
            if (km > 0)
            {
                if (varKm == null) return;
                if (e == null) return;           
                if (currentBus.Fuel - km < 0)
                    MessageBox.Show("thre is no enough fuel for this travel");
                else if (currentBus.KmForTravel + km > 20000)
                    MessageBox.Show($"the bus can travel {20000 - currentBus.KmForTravel} kilometers only");
                else if (currentBus.Status != Status.ready)
                    MessageBox.Show($"the status is {currentBus.Status}, the bus is not ready", "status failed");
                else if (e.Key == Key.Enter || e.Key == Key.Return)
                {
                    // update the detailes of the bus
                    currentBus.Kilometrage += km;
                    currentBus.KmForTravel += km;
                    currentBus.Fuel -= km;
                    // --------------------------------maby no need because the threads----------------------------------
                    if (currentBus.KmForTravel == 20000)
                        currentBus.Status = Status.needFix;
                    else 
                        currentBus.Status = Status.traveling;



                    e.Handled = true;
                    this.Close();
                    return;
                }
            }

        }
    }
}

