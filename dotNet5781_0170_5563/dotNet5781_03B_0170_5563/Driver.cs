using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_0170_5563
{
    public class Driver
    {
        private string driverName;
        private bool ready=true;
        public Driver(string name) { driverName = name; }

        public string DriverName { get => driverName; set => driverName = value; }
        public bool Ready { get => ready; set => ready = value; }
        public override string ToString()
        {
            return driverName;
        }
    }
}
