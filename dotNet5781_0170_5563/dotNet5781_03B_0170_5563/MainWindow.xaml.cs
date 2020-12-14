using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dotNet5781_03B_0170_5563
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// collection that store all the buses
        /// </summary>
        ObservableCollection<Bus> myBuses = new ObservableCollection<Bus>();
        /// <summary>
        /// collection that store all the drivers
        /// </summary>
        public ObservableCollection<Driver> myDrivers = new ObservableCollection<Driver>();

        /// <summary>
        /// function to inital 10 buses
        /// </summary>
        public void Buses()
        {
            //3 buses special 1 need fix 1 few fuel 1 close to be need fix
            myBuses.Add(new Bus("1234567", new DateTime(2017, 2, 20), new DateTime(2019, 10, 20), 100000, _kmAfterBusFixing: 0));
            myBuses.Add(new Bus("12345678", new DateTime(2018, 2, 20), new DateTime(2020, 10, 20), 100000, _kmAfterBusFixing: 19980));
            myBuses.Add(new Bus("87654321", new DateTime(2019, 2, 20), new DateTime(2020, 10, 20), 100000, 10, _kmAfterBusFixing: 0));

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

        /// <summary>
        /// our main func
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Buses();
            lbBuses.ItemsSource = myBuses;
            drivers.ItemsSource = myDrivers;
            myDrivers.Add(new Driver("dani zilber"));
            myDrivers.Add(new Driver("avi test"));
            drivers.Tag = "text";
        }

        /// <summary>
        /// when you press to add bus you open new window to 
        /// insert all the new bus details
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e"></param>
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 w1 = new Window1(myBuses);
            w1.Show();
        }

        /// <summary>
        /// button to take specifc bus to travel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bTravel_Click(object sender, RoutedEventArgs e)
        {
            var temp = sender as FrameworkElement;
            Bus bus = temp.DataContext as Bus;

            //check if we have drivers avilabls to take this travel
            if (myDrivers.Count == 0)
            {
                MessageBox.Show("you don't have any drivers");
            }
            else
            {
                bool flag = false;
                foreach (var d in myDrivers)
                {
                    if (d.Ready)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    MessageBox.Show("all the drivers busy");
                else //in case we have drivers open new window to insert the details
                {
                    travelDistance newTravel = new travelDistance(bus, myDrivers);
                    newTravel.Show();
                }

            }
        }

        /// <summary>
        /// button to fuel the bus and if the tank is full show
        /// message that you can't fuel more else take this bus to gas station and 
        /// shut him down for 12 seconds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bFueling_Click(object sender, RoutedEventArgs e)
        {
            var temp = sender as FrameworkElement;
            Bus bus = temp.DataContext as Bus;
            if (bus.Fuel < 1200)
            {
                new Thread(() =>
                {

                    bus.Fuel = 1200;
                    bus.Status = Status.fuelling;
                    new Thread(() =>
                    {
                        bus.EnableFuel = false;
                        bus.EnableTravel = false;
                        bus.EnableFix = false;
                        bus.Max = 12;
                        bus.ReverseTimer = 0;
                        //show timer how much time we have till the bus ready again
                        for (bus.Timer = 12; bus.Timer > 0; ++bus.ReverseTimer, --bus.Timer)
                            Thread.Sleep(1000);
                    }).Start();
                    Thread.Sleep(12000);
                    bus.Status = Status.ready;
                    this.Dispatcher.Invoke(() =>
                    {
                        bus.EnableFuel = true;
                        bus.EnableTravel = true;
                        bus.EnableFix = true;
                    });

                }).Start();
            }
            else
                MessageBox.Show("the tank is full already");
        }

        /// <summary>
        /// double mouse click on bus open new window that show all the deatials of the bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbBuses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var temp = lbBuses.SelectedItem;
            Bus bus = (Bus)temp;
            busDetails chosenBus = new busDetails(bus);
            chosenBus.ShowDialog();
        }

        /// <summary>
        /// addd driver to the company
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addDriver_Click(object sender, RoutedEventArgs e)
        {
            AddDriver add = new AddDriver(myDrivers);
            add.Show();
        }
    }
}
