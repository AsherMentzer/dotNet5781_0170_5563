using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus
{
    class Bus
    {
        public string Id;
        public readonly DateTime ActiveDate;
        public int Kilometrage;
        bool dangerous;
        int fuel;
        int kmAfterBusFixing;
        DateTime lastFix;

        public Bus(string id, int day, int month, int year)
        {
            Id = id;
            int[] gap = new int[] { 2, 6 };
            if (Id.Length == 8)
            {
                gap[0] = 3;
            }
            Id = Id.Insert(gap[0], "-");
            Id = Id.Insert(gap[1], "-");
            ActiveDate = new DateTime(year, month, day);
            lastFix = new DateTime(year, month, day);
        }
    }
}
