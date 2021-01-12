using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BO;

namespace PO
{
  public  class Bus: DependencyObject
    {   
        static readonly DependencyProperty IDProperty = DependencyProperty.Register("LicenceId", typeof(string), typeof(Bus));
        static readonly DependencyProperty ActiveDateProperty = DependencyProperty.Register("ActiveDate", typeof(DateTime), typeof(Bus));
        static readonly DependencyProperty StatusProperty = DependencyProperty.Register("BusStatus", typeof(BO.Status), typeof(Bus));
        static readonly DependencyProperty KilometrageProperty = DependencyProperty.Register("Kilometrage", typeof(double), typeof(Bus));
        static readonly DependencyProperty FuelProperty = DependencyProperty.Register("Fuel", typeof(double), typeof(Bus));

        public string LicenseId { get => (string)GetValue(IDProperty); set => SetValue(IDProperty, value); }
        public DateTime ActiveDate { get => (DateTime)GetValue(ActiveDateProperty); set => SetValue(ActiveDateProperty, value); }
        public double Killometrage { get => (double)GetValue(KilometrageProperty); set => SetValue(KilometrageProperty, value); }
        public double Fuel { get => (double)GetValue(FuelProperty); set => SetValue(FuelProperty, value); }
        public Status status { get => (BO.Status)GetValue(StatusProperty); set => SetValue(StatusProperty, value); }
        
    }
}
