﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        Driver driver;
        Bus currentBus;
        ObservableCollection<Driver> myDrivers;
        public travelDistance(Bus bus, ObservableCollection<Driver> Drivers)
        {
            InitializeComponent();
            myDrivers = Drivers;
            currentBus = bus;
            KM.PreviewKeyDown += KM_PreviewKeyDown;
            cbDriver.ItemsSource = myDrivers;//set the combo box to contain all the drivers
            
        }

        /// <summary>
        /// enter the distance to travle with enter only and allow inly numbers
        /// than take the bus to travle and update all the details according to this travle
        /// like the km the bus did and shut the bus and the driver for the time of the travle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                                    driver.Ready = false;
                                    currentBus.Status = Status.traveling;
                                    double sum = (km / 40) * 6000;
                                    currentBus.EnableTravel = false;
                                    currentBus.EnableFuel = false;
                                    currentBus.Max = (int)sum / 1000;
                                    currentBus.ReverseTimer = 0;
                                    new Thread(() =>
                                    {
                                        for (currentBus.Timer = (int)sum / 1000; currentBus.Timer > 0; ++currentBus.ReverseTimer, --currentBus.Timer)
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
                                    driver.Ready = true;
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


        /// <summary>
        /// choose 1 driver to take this travle and check if he is avilable or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbDriver_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             var d = cbDriver.SelectedItem as Driver;
            driver = d;
            if (!d.Ready)
            {
                MessageBox.Show("this driver is in travel");
            }
            else
            {     
                //d.Ready = false;
                KM.IsEnabled = true;
            }
        }
    }
}

