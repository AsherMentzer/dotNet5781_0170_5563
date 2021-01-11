﻿using System;
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

namespace PLGUI
{
    /// <summary>
    /// Interaction logic for updateStation.xaml
    /// </summary>
    public partial class updateStation : Window
    {
        TimeSpan time;
        double dis;
         int   ti,index ;
        BO.PairOfConsecutiveStation update;
        public updateStation(BO.PairOfConsecutiveStation p ,int _index)
        {
            index = _index;
           // dis = a;time = b;
            update = p;
            InitializeComponent();
        }

        private void tbDistance_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = sender as TextBox;
            double.TryParse(v.Text, out dis);
        }

        private void bUpdate_Click(object sender, RoutedEventArgs e)
        {
            update.Distance = dis;
            update.AverageTravleTime = time;
          
            this.Close();
        }

        private void tbTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = sender as TextBox;
            int.TryParse(v.Text, out ti);
            time = new TimeSpan(0, ti, 0);
        }
    }
}