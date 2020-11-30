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
using System.Windows.Shapes;

namespace dotNet5781_03B_0170_5563
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Bus bus;
        DateTime date;
        string licenseId;
        double kilometrage;
        bool dangerous;
        double fuel;
        double kmAfterBusFixing;
        DateTime lastFix;

        public Window1()
        {
            InitializeComponent();
            dpActivityDate.SelectedDateChanged += DpActivityDate_SelectedDateChanged;
            dpFixDate.SelectedDateChanged += DpFixDate_SelectedDateChanged;
        }



        private void DpActivityDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? d = picker.SelectedDate;

            if (d != null)
                date =(DateTime)d;
            //    this.Title = "No date";
            //}
            //else
            //{
            //   this.title = date.Value.ToShortDateString();
            //}
        }

        private void tbId_TextChanged(object sender, TextChangedEventArgs e)
        {
            var id = sender as string;

            if (date.Year < 2018)
            {
                if (id.Length != 7)
                    MessageBox.Show("the id must be 7 digits", "invalid id");
                else
                    licenseId = id;
            }
            else
            {
                if (id.Length != 8)
                    MessageBox.Show("the id must be 8 digits", "invalid id");
                else
                    licenseId = id;
            }
        }
        private void tbKm_TextChanged(object sender, TextChangedEventArgs e)
        {
            var k = sender as string;
            kilometrage = double.Parse(k);
        }

        private void tbKmAfterFix_TextChanged(object sender, TextChangedEventArgs e)
        {
            var k = sender as string;
            kmAfterBusFixing = double.Parse(k);

        }

        private void DpFixDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? d = picker.SelectedDate;
            if (d != null)
            {
                if (d < date)
                    MessageBox.Show("the last fix date must be later than active date", "invalid date");
                lastFix = (DateTime)d;
            }

        }
        private void tbFuel_TextChanged(object sender, TextChangedEventArgs e)
        {
            var k = sender as string;
            fuel = double.Parse(k);
        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            bus = new Bus(licenseId, date, lastFix, kilometrage, fuel, kmAfterBusFixing);
          
        }

      
    }
    
}
