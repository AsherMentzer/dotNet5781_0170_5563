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

namespace dotNet5781_03B_0170_5563
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<Bus> myBuses = new List<Bus>();

       
        public void Buses()
        {

            myBuses.Add(new Bus("1234567", new DateTime(2017,2, 20), new DateTime(2019, 10, 20), 100000));
            myBuses.Add(new Bus("12345678", new DateTime(2018, 2, 20), new DateTime(2020, 10, 20), 100000,_kmAfterBusFixing:19980));
            myBuses.Add(new Bus("87654321", new DateTime(2019, 2, 20), new DateTime(2019, 10, 20), 100000,10));
            
            for (int i = 0; i < 7; ++i)
            {
                bool flag = true;
                Bus b1; 
                do
                {
                    b1 = new Bus();
                    foreach (Bus b in myBuses)
                    {
                        if (b.GetId == b1.GetId)
                        {
                            flag = false;
                            break;
                        }
                    }
                } while (!flag);
                myBuses.Add(b1);
            }

            

        }
        public MainWindow()
        {
            InitializeComponent();
            Buses();
            lbBuses.ItemsSource = myBuses;
        }
        

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 w1 = new Window1();
            w1.Show();
        }
    }
}
