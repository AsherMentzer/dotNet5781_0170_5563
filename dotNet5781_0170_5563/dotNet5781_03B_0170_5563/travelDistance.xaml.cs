using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            if (currentBus.Status != Status.ready)
            {
                MessageBox.Show($"the status is {currentBus.Status}, the bus is not ready", "status failed");
                this.Close();
            }

            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (text.Text.Length > 0)
                {
                    double km;
                    double.TryParse(text.Text, out km);
                    if (km > 0)
                    {
                        if (currentBus.Fuel - km < 0)
                            MessageBox.Show("there is no enough fuel for this travel");
                        else if (currentBus.KmForTravel + km > 20000)
                            MessageBox.Show($"the bus can travel {20000 - currentBus.KmForTravel} kilometers only");
                        else
                        {
                            new Thread(() =>
                                {

                                    // --------------------------------maby no need because the threads----------------------------------
                                    //if (currentBus.KmForTravel == 20000)
                                    //    currentBus.Status = Status.needFix;
                                    //else
                                    currentBus.Status = Status.traveling;
                                    double sum = (km / 40) * 6000;
                                    currentBus.EnableTravel = false;
                                    currentBus.EnableFuel = false;
                                    new Thread(() =>
                                    {
                                        for (currentBus.Timer = (int)sum/1000; currentBus.Timer > 0; --currentBus.Timer)
                                            Thread.Sleep(1000);
                                    }).Start();
                                    Thread.Sleep((int)sum);
                                    // update the detailes of the bus
                                    currentBus.Kilometrage += km;
                                    currentBus.KmForTravel += km;
                                    currentBus.Fuel -= km;
                                    currentBus.Status = Status.ready;
                                    currentBus.EnableTravel = true;
                                    currentBus.EnableFuel = true;
                                }).Start();
                        }
                        e.Handled = true;
                        this.Close();
                        return;
                    }
                }
            }
            //  It`s  a  system  key  (add  other  key  here  if  you  want  to  allow)
            if (e.Key == Key.Escape || e.Key == Key.Tab || e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.CapsLock ||
            e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl || e.Key == Key.LeftAlt || e.Key == Key.RightAlt ||
            e.Key == Key.LWin || e.Key == Key.RWin || e.Key == Key.System || e.Key == Key.Left || e.Key == Key.Up ||
            e.Key == Key.Down || e.Key == Key.Right) return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (Char.IsControl(c)) return;
            if (Char.IsDigit(c))
            {
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift) || Keyboard.IsKeyDown(Key.LeftCtrl)
                    || Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return;
            }

            e.Handled = true;
            MessageBox.Show("Only  numbers  are  allowed", "Account", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}

