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
            if (currentBus.Status == Status.needFix || currentBus.Status == Status.ready)
                bFix.IsEnabled = true;
            if (currentBus.Status == Status.fixing || currentBus.Status == Status.fuelling ||
                currentBus.Status == Status.traveling || currentBus.Status == Status.needFix)
                bFuel.IsEnabled = false;
        }

        private void bFuel_Click(object sender, RoutedEventArgs e)
        {
            if (currentBus.Status == Status.traveling)
            {
                MessageBox.Show("You Can not fueling, the Bus is in the travel"); return;
            }
            else if (currentBus.Fuel == 1200)
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
                         status.Text = currentBus.Status.ToString();
                         fuel.Text = "...";
                     });
                     new Thread(() =>
                     {
                         for (currentBus.Timer = 12; currentBus.Timer > 0; --currentBus.Timer)
                             Thread.Sleep(1000);
                     }).Start();
                     Thread.Sleep(12000);
                     
                     if (lastStatus != Status.needFix && lastStatus != Status.needFix)
                         currentBus.Status = Status.ready;
                     else
                         currentBus.Status = lastStatus;
                     this.Dispatcher.Invoke(() =>
                     {
                         bFuel.IsEnabled = true;
                         status.Text = currentBus.Status.ToString();
                         fuel.Text = currentBus.Fuel.ToString();
                     });
                 }).Start();


                //this.Close();
            }
        }

        private void bFix_Click(object sender, RoutedEventArgs e)
        {
            Thread thread;
            new Thread(() =>
            {
                thread = Thread.CurrentThread;

                currentBus.KmForTravel = 0;
                currentBus.LastFix = DateTime.Now;
                currentBus.Fuel = 1200;
                currentBus.Status = Status.fixing;
                this.Dispatcher.Invoke(() =>
                {
                    bFix.IsEnabled = false;
                    bFuel.IsEnabled = false;
                    status.Text = currentBus.Status.ToString();
                    fuel.Text = "...";

                });                
                new Thread(() => { MessageBox.Show("start fixing"); }).Start();
                new Thread(() =>
                {
                    for (currentBus.Timer = 144; currentBus.Timer > 0; --currentBus.Timer)
                        Thread.Sleep(1000);
                }).Start();
                Thread.Sleep(144000);
                currentBus.Status = Status.ready;
                currentBus.EnableTravel = true;
                this.Dispatcher.Invoke(() =>
                {
                    bFix.IsEnabled = true;
                    bFuel.IsEnabled = true;
                    status.Text = currentBus.Status.ToString();
                    fuel.Text = currentBus.Fuel.ToString();
                });
                MessageBox.Show("finish fixing");

            }).Start();
            //this.Close();
        }

    }
}
