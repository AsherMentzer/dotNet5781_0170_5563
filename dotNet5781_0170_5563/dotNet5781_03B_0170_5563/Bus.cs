using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_0170_5563
{
    /// <summary>
    /// enum for the staus of the bus
    /// </summary>
    public enum Status { ready, traveling, fuelling, fixing, needFix };

    /// <summary>
    /// class for bus with all the bus details the class is heir from INotifyPropertyChanged
    /// interface to alow to be dependemcy property that can  update the window on every change 
    /// on real time
    /// </summary>
    public class Bus : INotifyPropertyChanged
    {
        static Random r = new Random(DateTime.Now.Millisecond);
        ///fields
        private string licenseId;
        private DateTime activeDate;
        private double kilometrage;
        private double fuel;
        private double kmAfterBusFixing;
        private DateTime lastFix = DateTime.Now;
        private bool enableFuel;
        private bool enableTravel;
        private bool enableFix;
        private Status status;
        private string imageStatus;
        int timer = 0;
        int max = 1;
        int reverse = 0;
        string str = "Hidden";
        public event PropertyChangedEventHandler PropertyChanged;

        ///properties
       // public ObservableCollection<string> Drivers { get => drivers; set => drivers = value; }
        public string GetId { get =>ToString(); }
        public DateTime Active { get => activeDate; }
        public double Kilometrage { get => kilometrage; set { kilometrage = value; OnPropertyChanged(); } }
        public double Fuel { get => fuel; set { fuel = value; OnPropertyChanged(); } }
        public double KmForTravel { get => kmAfterBusFixing; set { kmAfterBusFixing = value; OnPropertyChanged(); } }
        public DateTime LastFix { get => lastFix; set { lastFix = value; OnPropertyChanged(); } }
        public Status Status { get => status; set { status = value; OnPropertyChanged(); ImageStatus = ""; PbVisiblity = ""; } }
        public int Timer { get => timer; set { timer = value; OnPropertyChanged(); } }
        public int Max { get => max; set { max = value; OnPropertyChanged(); } }
        public int ReverseTimer { get => reverse; set { reverse = value; OnPropertyChanged(); } }

        //propertys to enable buttos by  contitions 
        public string PbVisiblity
        {
            get => str;
            set
            {
                if (status == Status.ready || status == Status.needFix) str = "Hidden";
                else str = "Visible"; OnPropertyChanged();
            }
        }
        public bool EnableFuel
        {
            get => enableFuel;
            set { if (status == Status.ready) enableFuel = true; else enableFuel = false; OnPropertyChanged(); }
        }
        public bool EnableTravel
        {
            get => enableTravel;
            set { if (status == Status.ready) enableTravel = true; else enableTravel = false; OnPropertyChanged(); }
        }
        public bool EnableFix
        {
            get => enableFix;
            set
            {
                if (status == Status.ready || status == Status.needFix) enableFix = true;
                else enableFix = false; OnPropertyChanged();
            }
        }

        /// <summary>
        /// property to set image for the status of the bus
        /// </summary>
        public string ImageStatus
        {
            get => imageStatus; set
            {
                if (status == Status.fixing) imageStatus = @"images\fix1.png";
                else if (status == Status.needFix) imageStatus = @"images\needFix.png";
                else if (status == Status.traveling) imageStatus = @"images\travel1.png";
                else if (status == Status.fuelling) imageStatus = @"images\fueling.png";
                else imageStatus = @"images\ready.png"; OnPropertyChanged();
            }
        }

        //public Status myStatus { get=>status; set=>status=value; }
        /// <summary>
        /// constructor how get at least the id and the active date for new bus
        /// all the others is for old bus
        /// </summary>
        /// <param name="newId">licenseId</param>
        /// <param name="year">year of the active date</param>
        /// <param name="month">month of the active date</param>
        /// <param name="day">day of the active date</param>
        /// <param name="_kilometrage">total kilometrage of the bus default is 0 </param>
        /// <param name="_fuel">fuel in bus default is 1200(full tank)</param>
        /// <param name="_kmAfterBusFixing">how much he can travel till he will need fix
        /// the default is 20000 we assuming he fixed when you get the bus</param>
        public Bus(string newId, DateTime active, DateTime _lastFix, double _kilometrage = 0,
            double _fuel = 1200, double _kmAfterBusFixing = 0, Status _status = Status.ready)
        {
            licenseId = newId;
            activeDate = active;
            kilometrage = _kilometrage;
            fuel = _fuel;
            kmAfterBusFixing = _kmAfterBusFixing;
            lastFix = _lastFix;
            if (_status != Status.ready && _status != Status.traveling)
                status = _status;
            else
            {
                DateTime t = lastFix.AddYears(1);
                if (t < DateTime.Now)
                    status = Status.needFix;
                else if (kmAfterBusFixing > 20000)
                    status = Status.needFix;
                else
                    status = Status.ready;
            }
            ImageStatus = "";
            EnableFuel = false;
            EnableTravel = false;
            EnableFix = false;

        }

        /// <summary>
        /// constructor that dont get any parameter and build rbus withe random details
        /// </summary>
        public Bus()
        {
            activeDate = randDate();
            if (activeDate.Year < 2018)
                licenseId = r.Next(1000000, 9999999).ToString();
            else
                licenseId = r.Next(10000000, 99999999).ToString();
            kilometrage = 0;
            fuel = 1200;
            kmAfterBusFixing = 0;
            status = Status.ready;
            ImageStatus = "";
            EnableFuel = false;
            EnableTravel = false;
            EnableFix = false;

        }
        public DateTime randDate()
        {
            int year = r.Next(1990, DateTime.Now.Year);
            int month = r.Next(1, 12);
            DateTime d = new DateTime(year, month, 5);
            return d;
        }
        /// <summary>
        /// the function copies the id and adds hyphens betwwen the parts of the id
        /// </summary>
        /// <returns>the function returns string that contain the id with hyphens 
        /// between the parts of the id</returns>
        public string PrintID()
        {
            string temp = licenseId.Insert(5, "-");
            if (licenseId.Length == 7)
            {
                temp = temp.Insert(2, "-");
                temp = temp + '\0';
            }
            else
                temp = temp.Insert(3, "-");
            return temp;
        }

        public override string ToString()
        {
            return PrintID();
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}