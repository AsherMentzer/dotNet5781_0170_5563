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
    /// Interaction logic for busDetails.xaml
    /// </summary>
    public partial class busDetails : Window
    {
        Bus currentBus;

        /// <summary>
        /// initial our window 
        /// </summary>
        /// <param name="bus"></param>
        public busDetails(Bus bus)
        {
            InitializeComponent();
            currentBus = bus;
            this.DataContext = currentBus;
            //if the bus in travle or fuel or fix you can't send him to fix
            if (currentBus.Status == Status.needFix || currentBus.Status == Status.ready)
                bFix.IsEnabled = true;
            else
                bFix.IsEnabled = false;
            //if the bus in travle or fuel or fix you can't send him to fuel
            if (currentBus.Status == Status.fixing || currentBus.Status == Status.fuelling ||
                currentBus.Status == Status.traveling || currentBus.Status == Status.needFix)
                bFuel.IsEnabled = false;
        }

        /// <summary>
        /// when you send him to fuel shut the bus down to 12 seconds and show timer
        /// and update all the deatails
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bFuel_Click(object sender, RoutedEventArgs e)
        {
            if (currentBus.Fuel == 1200)
            {
                MessageBox.Show("the tank is full already"); return;
            }
            else
            {
                Status lastStatus = currentBus.Status;
                Thread thread;
                new Thread(() =>
                 {
                     thread = Thread.CurrentThread;
                     currentBus.Fuel = 1200;
                     currentBus.Status = Status.fuelling;

                     this.Dispatcher.Invoke(() =>
                     {
                         bFuel.IsEnabled = false;
                         bFix.IsEnabled = false;
                         currentBus.EnableFix = false;
                         currentBus.EnableFuel = false;
                         currentBus.EnableTravel = false;
                         status.Text = currentBus.Status.ToString();
                         fuel.Text = "...";
                     });
                     currentBus.Max = 12;
                     currentBus.ReverseTimer = 0;
                     new Thread(() =>
                     {
                         for (currentBus.Timer = 12; currentBus.Timer > 0; ++currentBus.ReverseTimer, --currentBus.Timer)
                         {
                             this.Dispatcher.Invoke(() =>
                             {
                                 timer.Text = currentBus.Timer.ToString();

                             });
                             Thread.Sleep(1000);

                         }
                         this.Dispatcher.Invoke(() =>
                         {
                             timer.Text = currentBus.Timer.ToString();

                         });
                     }).Start();
                     Thread.Sleep(12000);
                     currentBus.Status = Status.ready;
                     //if (lastStatus != Status.needFix && lastStatus != Status.needFix)
                     //    currentBus.Status = Status.ready;
                     //else
                     //    currentBus.Status = lastStatus;
                     this.Dispatcher.Invoke(() =>
                     {
                         bFuel.IsEnabled = true;
                         bFix.IsEnabled = true;
                         currentBus.EnableFix = true;
                         currentBus.EnableFuel = true;
                         currentBus.EnableTravel = true;
                         status.Text = currentBus.Status.ToString();
                         fuel.Text = currentBus.Fuel.ToString();
                     });
                 }).Start();

            }
        }

        /// <summary>
        /// send the bus to fix and shut him down to 144 seconds 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bFix_Click(object sender, RoutedEventArgs e)
        {
            Thread thread;
            new Thread(() =>
            {
                thread = Thread.CurrentThread;
                currentBus.PbVisiblity = "Visible";
                currentBus.KmForTravel = 0;
                currentBus.LastFix = DateTime.Now;
                currentBus.Fuel = 1200;
                currentBus.Status = Status.fixing;
                this.Dispatcher.Invoke(() =>
                {
                    bFix.IsEnabled = false;
                    bFuel.IsEnabled = false;
                    currentBus.EnableFix = false;
                    currentBus.EnableFuel = false;
                    currentBus.EnableTravel = false;
                    status.Text = currentBus.Status.ToString();
                    fuel.Text = "...";

                });
                currentBus.Max = 144;
                currentBus.ReverseTimer = 0;
                new Thread(() =>
                {
                    for (currentBus.Timer = 144; currentBus.Timer > 0; ++currentBus.ReverseTimer, --currentBus.Timer)
                    {
                        this.Dispatcher.Invoke(() => { timer.Text = currentBus.Timer.ToString(); });
                        Thread.Sleep(1000);
                    }
                }).Start();
                Thread.Sleep(144000);
                currentBus.Status = Status.ready;
                currentBus.EnableTravel = true;
                this.Dispatcher.Invoke(() =>
                {
                    bFix.IsEnabled = true;
                    bFuel.IsEnabled = true;
                    currentBus.EnableFix = true;
                    currentBus.EnableFuel = true;
                    currentBus.EnableTravel = true;
                    status.Text = currentBus.Status.ToString();
                    fuel.Text = currentBus.Fuel.ToString();
                });              

            }).Start();
            
        }      
    }
}
