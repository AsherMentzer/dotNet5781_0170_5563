using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_0170_5563
{
    using Bus;
    using System.Runtime.InteropServices;

    public class BusesFleetManagement
    {
        private List<Bus> buses = new List<Bus>();
        public void addBus()
        {
            Console.WriteLine("enter the id number");
            string id = Console.ReadLine();
            int year, month, day;
            Console.WriteLine("enter the active date");
            Console.WriteLine("enter the year of the active date");
            year = int.Parse(Console.ReadLine());
            Console.WriteLine("enter the month of the active date");
            month = int.Parse(Console.ReadLine());
            Console.WriteLine("enter the day of the active date");
            day = int.Parse(Console.ReadLine());
            Bus b1 = new Bus(id, year, month, day);
            buses.Add(b1);
        }
        public chooseBusToTravel()
        {
            Console.WriteLine("enter id number:");
            string id = Console.ReadLine();
            Bus temp = null;
            foreach (Bus b1 in buses)
            {
                if (b1.Id == id)
                {
                    temp = b1;
                    return;
                }

            }
            if (temp == null)
            {
                Console.WriteLine("the bus not exist in the fleet");
                return;
            }

            Random rand = new Random(DateTime.Now.Millisecond);
            int km = rand.Next(0, 1201);
            if (km > temp.Fuel)
                Console.WriteLine("there is not enough fuel for this travel");
            if(temp.KmForTravel<km)
                Console.WriteLine("the Bus can not travel this amount of km before fixing");
            // temp.Kilometrage += km;
            // temp.KmForTravel -= km;
            // temp.fuel -= km;
        }
    }
}
