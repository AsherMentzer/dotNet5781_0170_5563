using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus
{
    public class Bus
    {
        public string Id;
        DateTime ActiveDate;
        public int Kilometrage;
        bool dangerous;
        int fuel;
        int kmAfterBusFixing;
        DateTime lastFix;
        
        public Bus(string id, int year, int month, int day)
        {
            Id = id;
            ActiveDate = new DateTime(year, month, day);
            lastFix = new DateTime(year, month, day);
        }
    }

    //class Date
    //{
    //    int Year;
    //    int Month;
    //    int Day;

    //    public Date(int y, int m, int d)
    //    {
    //        Year = y;
    //        Month = m;
    //        Day = d;
    //    }
    //}
}
