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

        private void dpActivityDate_CalendarClosed(object sender, RoutedEventArgs e){ }

        private void DpActivityDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? d = picker.SelectedDate;

            if (d != null)
            {
                if (d.Value.Year < 1990 || d.Value > DateTime.Now)
                {

                    MessageBox.Show("the date must be between 1990 - today", "invalid date");
                    //  dpActivityDate.SelectedDate = null;
                    // e.Handled = true;
                }
                else
                {
                    date = (DateTime)d;
                    int length = 7;
                    if (date.Year >= 2018)
                        length = 8;
                    if (tbId.Text.Length < length)
                        va.Text = "not valid";
                    dpFixDate.IsEnabled = true;
                }
                tbId.IsEnabled = true;
            }
        }

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

                //MessageBox.Show("the id must be 7 digits", "invalid id");
                //    else

                //}
                //else
                //{
                if (id.Length != length)
                {
                    tbId.BorderBrush = bordColor.BorderBrush;
                    tbId.Background = bordColor.Background;
                    va.Text = "not valid";
                    //MessageBox.Show("the id must be 8 digits", "invalid id");
                    licenseId = null;
                }
                else
                {
                    tbId.BorderBrush = default;
                    tbId.Background = default;
                    double a;
                    if (double.TryParse(id, out a))
                    {
                        va.Text = "valid";
                        licenseId = id;
                    }
                }
            }
        }
        private void tbKm_TextChanged(object sender, TextChangedEventArgs e)
        {
            double check;
            var v = sender as TextBox;

            if (v != null)
            {
                double.TryParse(v.Text, out check);
                if (check >= 0)
                    kilometrage = check;
                else
                    MessageBox.Show("the kilometrage can not to be negative");
            }
        }

        private void tbKmAfterFix_TextChanged(object sender, TextChangedEventArgs e)
        {
            double check;
            var v = sender as TextBox;
            if (v != null)
            {

                double.TryParse(v.Text, out check);
                if (check >= 0)
                    kmAfterBusFixing = check;
                else
                    MessageBox.Show("the kilometrage can not to be negative");
            }
        }

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

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            int length;
            if (date.Year < 2018) length = 7;
            else length = 8;
            foreach(var id in bu)
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
