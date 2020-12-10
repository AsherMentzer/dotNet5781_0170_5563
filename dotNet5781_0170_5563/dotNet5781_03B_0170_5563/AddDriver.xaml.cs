using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddDriver.xaml
    /// </summary>
    public partial class AddDriver : Window
    {
        ObservableCollection<Driver> addDriver;
        public AddDriver(ObservableCollection<Driver> drivers)
        {
            InitializeComponent();
            addDriver = drivers;
        }

      

        private void addD_Click(object sender, RoutedEventArgs e)
        {
           
            addDriver.Add(new Driver(tbDriver.Text));
            this.Close();
        }
    }
}
