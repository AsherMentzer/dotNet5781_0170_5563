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
namespace PLGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       IBL bl = BLFactory.GetBL("1");
        List<BO.Station>stations=new List<BO.Station>();
        void createBuses()
        {
         foreach(var b in  bl.GetAllStations())
            {
                stations.Add(b);
            }
        }
        //public ViewModel.MainWindow viewModel;
        public MainWindow()
        {
            InitializeComponent();
            createBuses();
            lbBuses.ItemsSource = stations;
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
    }
}
