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
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotNet5781_02_0170_5563;

namespace dotNet5781_03A_0170_5563
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private BusLine currentDisplayBusLine;
        Data linesData;

        public MainWindow()
        {
            InitializeComponent();
            linesData = new Data();
            cbBusLines.ItemsSource = linesData.lines;
            cbBusLines.DisplayMemberPath = " GetBusLine ";
            cbBusLines.SelectedIndex = 0;
        }

        void ShowBusLine(int index)
        {
            currentDisplayBusLine = linesData.lines[index].First();
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.stations;
        }
       

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).GetBusLine);

        }

    }
}
