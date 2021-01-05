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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PO;
using BLAPI;
using ViewModel;
using BO;
using System.Windows.Controls.Primitives;

namespace PLGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        List<BO.BusLine> stations = new List<BO.BusLine>();
        void createBuses()
        {
            foreach (var b in bl.GetAllBusLines())
            {
                stations.Add(b);
            }
        }
       
        //public ViewModel.MainWindow viewModel;
        public MainWindow()
        {
            InitializeComponent();
            createBuses();
            lvStations.ItemsSource = stations;
           
            //viewModel = new ViewModel.MainWindow();
            //viewModel.Reset();
            //DataContext = viewModel;
        }

        private void signInButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lbBuses_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void bs_Click(object sender, RoutedEventArgs e)
        {
            BO.Station sbo = sender as BO.Station;
        }
    }
}
