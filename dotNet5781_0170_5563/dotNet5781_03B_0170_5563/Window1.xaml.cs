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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DateTime date;
        string licenseId;
        double kilometrage;
        double fuel;
        double kmAfterBusFixing;
        DateTime lastFix;
        ObservableCollection<Bus> bu;
        public Window1(ObservableCollection<Bus> b)
        {
            InitializeComponent();
            bu = b;
            tbId.MaxLength = 8;
            // dpActivityDate.SelectedDateChanged += DpActivityDate_SelectedDateChanged;
            //dpFixDate.SelectedDateChanged += DpFixDate_SelectedDateChanged;
        }

        private void dpActivityDate_CalendarClosed(object sender, RoutedEventArgs e) { }

        /// <summary>
        /// the active date and is only between 1990 to today
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DpActivityDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? d = picker.SelectedDate;

            if (d != null)
            {
                if (d.Value.Year < 1990 || d.Value > DateTime.Now)
                {

                    MessageBox.Show("the date must be between 1990 - today", "invalid date");                 
                }
                else
                {
                    //check that the id length is correcrt and show
                    //red background unless is correct that is turn to regular
                    date = (DateTime)d;
                    int length = 7;
                    if (date.Year >= 2018)
                        length = 8;

                    lbIDbackground.Content = $"enter {length} digits";

                    if (tbId.Text.Length < length)
                        //va.Text = "not valid";
                        dpFixDate.IsEnabled = true;
                }
                tbId.IsEnabled = true;
            }
        }

        /// <summary>
        /// geting the id and check validity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbId_TextChanged(object sender, TextChangedEventArgs e)
        {


            var v = sender as TextBox;
            //string v = d.Text;
            if (v != null)
            {
                string id = v.Text;
                int length = 7;
                if (date.Year >= 2018)
                    length = 8;
               
                if (id.Length != length)
                {
                    if (tbId.Text.Length == 0)
                    {
                        brdId.Background = Brushes.Transparent;
                    }
                    else
                    {
                        brdId.BorderBrush = Brushes.Red;
                        brdId.Background = Brushes.LightPink;
                    }
                                   
                    licenseId = null;
                }
                else
                {
                    double a;
                    if (double.TryParse(id, out a))
                    {
                        brdId.BorderBrush = Brushes.Green;
                        brdId.Background = Brushes.LightGreen;

                        //va.Text = "valid";
                        licenseId = id;
                    }
                }
            }
        }

        /// <summary>
        /// get the km the bus travle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbKm_TextChanged(object sender, TextChangedEventArgs e)
        {
            double check;
            var v = sender as TextBox;

            if (v != null)
            {
                double.TryParse(v.Text, out check);
                if (check >= 0)
                {
                    kilometrage = check;
                    tbKmAfterFix.IsEnabled = true;
                    v.Background = default;
                    v.BorderBrush = default;
                }
                else
                {
                    v.Background = Brushes.LightPink;
                    v.BorderBrush = Brushes.Red;
                    MessageBox.Show("the kilometrage can not to be negative","invalid",MessageBoxButton.OK, MessageBoxImage.Error);

                }

            }
        }

        /// <summary>
        /// get the km he travle after the last fix
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbKmAfterFix_TextChanged(object sender, TextChangedEventArgs e)
        {
            double check;
            var v = sender as TextBox;
            if (v != null)
            {

                double.TryParse(v.Text, out check);
                if (check >= 0)
                {
                    if(check>kilometrage)
                        MessageBox.Show("the kilometrage after the fix can not to be bigger that kilomtrage");
                    kmAfterBusFixing = check;
                }
                else
                    MessageBox.Show("the kilometrage can not to be negative");
            }
        }

        /// <summary>
        /// get the last fix date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DpFixDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? d = picker.SelectedDate;
            if (d != null)
            {
                if (d < date)
                    MessageBox.Show("the last fix date must be later than active date", "invalid date");
                else if (d > DateTime.Now)
                    MessageBox.Show("the last fix can not be later than today", "invalid date");
                else
                    lastFix = (DateTime)d;
            }
        }

        /// <summary>
        /// get he fuel the bus have
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbFuel_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = sender as TextBox;
            if (v != null)
            {
                double test;
                double.TryParse(v.Text, out test);
                if (test >= 0 && test < 1201)
                {
                    fuel = test;
                }
                else
                    MessageBox.Show("The tank contain between 0 to 1200 litter");
            }
        }

        /// <summary>
        /// check if all the fileds are filled and all is good than add the bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            int length;
            if (date.Year < 2018) length = 7;
            else length = 8;
            foreach (var id in bu)
            {
                if (licenseId == id.GetId)
                {
                    MessageBox.Show("the bus already exist", "not valid license id");
                    return;
                }
            }
            if ((licenseId == null || licenseId.Length != length) || date == default || lastFix == default || kilometrage == default
                || fuel == default || kmAfterBusFixing == default) { MessageBox.Show("fill all the fields"); return; }
            Bus bus = new Bus(licenseId, date, lastFix, kilometrage, fuel, kmAfterBusFixing);
            bu.Add(bus);
            this.Close();
        }
    }
}
