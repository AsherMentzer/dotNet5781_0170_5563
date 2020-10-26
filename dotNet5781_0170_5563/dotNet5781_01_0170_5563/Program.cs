using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_0170_5563
{
    using Bus;
    class Program
    {
        static void Main(string[] args)
        {
            Bus b1 = new Bus("1234567", 1, 6, 1990);
            
            Console.WriteLine(b1.ActiveDate.ToString());
            Console.WriteLine(b1.ActiveDate.ToShortDateString());
            Console.WriteLine(b1.Id);
        }
        foreach 
    }
}
