using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus
{
    public class Bus
    {
        ///fields
        private string Id;
        private DateTime ActiveDate;
        private int kilometrage;
        private bool dangerous;
        private int fuel;
        private int kmAfterBusFixing=20000;
        private DateTime lastFix;
        ///properties
        public string GetId{get=>Id;}
        public DateTime active{get=>ActiveDate;}
        public int Kilometrage{get=>kilometrage; set=>kilometrage=value;}
        public int Fuel { get=>fuel; set=>fuel=value; }
        public int KmForTravel { get=>kmAfterBusFixing; set=>kmAfterBusFixing=value; }
        public DateTime LastFix{get=>lastFix;set=>lastFix=value;}
        //constructor
        public Bus(string id, int year, int month, int day)
        {
            Id = id;
            ActiveDate = new DateTime(year, month, day);
            lastFix = new DateTime(year, month, day);
        }
    }
}