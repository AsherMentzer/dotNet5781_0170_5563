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
        public void AddBus()
        {
            Console.Write("enter the id number: ");
            string id = Console.ReadLine();
            int year, month, day;
            Console.WriteLine("enter the active date:");
            Console.Write("year: ");
            year = int.Parse(Console.ReadLine());
            Console.Write("month: ");
            month = int.Parse(Console.ReadLine());
            Console.Write("day: ");
            day = int.Parse(Console.ReadLine());
            Bus b1 = new Bus(id, year, month, day);
            buses.Add(b1);
        }

        public void ChooseBusToTravel()
        {
            Console.WriteLine("enter id number:");
            string id = Console.ReadLine();
            Bus temp = null;
            foreach (Bus b1 in buses)
            {
                if (b1.GetId == id)
                {
                    temp = b1;
                    break;
                }

            }
            if (temp == null)
            {
                Console.WriteLine("the bus not exist in the fleet");
                return;
            }

            if (temp.Dangerous)
            {
                Console.WriteLine("the bus is dangerous, please send it to fix");
                return;
            }
                
            DateTime t = temp.LastFix.AddYears(1);
            int a = DateTime.Now.CompareTo(t);
            if (a >= 0)
            {
                temp.Dangerous = true;
                Console.WriteLine("More than a year passed from the last fix, please send it to fix");
                return;
            }

            Random rand = new Random(DateTime.Now.Millisecond);
            int km = rand.Next(0, 1201);
            if (km > temp.Fuel)
                Console.WriteLine("there is not enough fuel for this travel");
            if (temp.KmForTravel < km)
                Console.WriteLine("the Bus can not travel this amount of km before fixing");

            temp.Kilometrage = km + temp.Kilometrage;
            temp.KmForTravel = temp.KmForTravel - km;
            temp.Fuel = temp.Fuel - km;
        }

        public void FuelOrFixBus()
        {
            Console.WriteLine("enter id number:");
            string id = Console.ReadLine();
            Bus temp = null;
            foreach (Bus b1 in buses)
            {
                if (b1.GetId == id)
                {
                    temp = b1;
                    break;
                }

            }
            if (temp == null)
            {
                Console.WriteLine("the bus not exist in the fleet");
                return;
            }

            int choice;
            do
            {
                Console.WriteLine("To fuel press 1\nTo fix press 2");
                choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                    temp.Fuel = 1200;
                else if (choice == 2)
                {
                    temp.KmForTravel = 20000;
                    temp.LastFix = DateTime.Now;
                }
            } while (choice != 1 && choice != 2);
        }

        public void ShowKmFromLastFix()
        {
            Console.WriteLine("Bus ID:     KM from last fix");
            foreach (Bus b in buses)
            {
                Console.WriteLine("{0} {1 ,13}", b.PrintID(), 20000 - b.KmForTravel );
            }
        }
    }
}
