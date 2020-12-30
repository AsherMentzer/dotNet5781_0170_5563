using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum Status { ready, traveling, fuelling, fixing, needFix };
    public class Bus
    {
        public string LicenseId { get; set; }
        public DateTime ActiveDate { get; set; }
        public double Killometrage { get; set; }
        public double Fuel { get; set; }
        public Status status { get; set; }
    }
}
