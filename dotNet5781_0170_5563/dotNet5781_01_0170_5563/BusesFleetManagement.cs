using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace dotNet5781_01_0170_5563
{
    using Bus;
 

    public class BusesFleetManagement
    {

        static Random rand = new Random(DateTime.Now.Millisecond);

        private List<Bus> buses = new List<Bus>();
        /// <summary>
        /// the function add a new bus to the fleet
        /// </summary>
        public void AddBus()
        {
            bool valid = false;
            int year, month, day;
            Console.WriteLine("enter the active date:");
            // get the year of activitie of the bus. between 1950 to today
            do
            {
                valid = false;
                Console.Write("year: ");
                while (!int.TryParse(Console.ReadLine(), out year))
                    Console.WriteLine("enter numbers only");
                valid = checkInput(1950, DateTime.Now.Year, year) ;
            } while (!valid);

            do
            {
                valid = false;
                Console.Write("month: ");
                while (!int.TryParse(Console.ReadLine(), out month))
                    Console.WriteLine("enter numbers only");
                valid = checkInput(1, 12, month);
            } while (!valid);

            do
            {
                valid = false;
                Console.Write("day: ");
                while (!int.TryParse(Console.ReadLine(), out day))
                    Console.WriteLine("enter numbers only");
                valid = checkInput(1, DateTime.DaysInMonth(year,month), day);
            } while (!valid);

            string id = InputId(year);

            foreach (Bus b in buses)
            {
                if(b.GetId == id)
                {
                    Console.WriteLine("the bus is in fleet already");
                    return;
                }
            }

            Bus b1 = new Bus(id, year, month, day);
            buses.Add(b1);
        }

        public void ChooseBusToTravel()
        {
            // get the id number to find the bus in the list
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
            // in case the bus not in the list
            if (temp == null)
            {
                Console.WriteLine("the bus not exist in the fleet");
                return;
            }

            // check if the bus is dangerous. if the bus dangerous we can't use it.
            if (temp.Dangerous)
            {
                Console.WriteLine("the bus is dangerous, please send it to fix");
                return;
            }

            // check if the last fix was more than a year ago.
            // if so, the bus is dangerous.
            DateTime t = temp.LastFix.AddYears(1);
            int a = DateTime.Now.CompareTo(t);
            if (a >= 0)
            {
                temp.Dangerous = true;
                Console.WriteLine("More than a year passed from the last fix, please send it to fix");
                return;
            }

            // get a random number for legth of the travel.
            int km = rand.Next(0, 1201);
            // check if there is enough fuel for the travel 
            if (km > temp.Fuel)
                Console.WriteLine("there is not enough fuel for this travel");
            // chack if the bus will pass during this travel 20k KM limit from the last fix 
            if (temp.KmForTravel < km)
                Console.WriteLine("the Bus can not travel this amount of km before fixing");

            // update the detailes of the bus
            temp.Kilometrage = km + temp.Kilometrage;
            temp.KmForTravel = temp.KmForTravel - km;
            temp.Fuel = temp.Fuel - km;

            // in case the bus traveled 20k KM from the last fix
            if (temp.KmForTravel == 0)
                temp.Dangerous = true;
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
            Console.WriteLine("  Bus ID:      KM from last fix");
            foreach (Bus b in buses)
            {
                Console.WriteLine("{0} {1 ,12}", b.PrintID(), 20000 - b.KmForTravel);
            }
        }

        /// <summary>
        /// function to get valid input for the id
        /// </summary>
        /// <param name="year">to set the length of the id string</param>
        /// <returns>the valid id</returns>
        public string InputId(int year)
        {
            int length = 8;
            if (year < 2018)
                length = 7;
            string id;
            string ch = "[0-9]$";
            //loop to get and check the input for id
            do
            {
                Console.Write("enter the id number: ");
                id = Console.ReadLine();
                //check if the length of the id is valid
                if (id.Length != length)
                    Console.WriteLine("the ID number not valid");
                //check if the id is only numbers
                else if (!Regex.IsMatch(id, ch))
                    Console.WriteLine("enter only numbers");
            } while (id.Length != length || !Regex.IsMatch(id, ch));

            return id;
        }

        /// <summary>
        /// check if the numeric input is valid(between our limits)
        /// </summary>
        /// <param name="min">the lowest possible value </param>
        /// <param name="max">the higest possible value</param>
        /// <param name="input">our input</param>
        /// <returns>if the input is between the limits return true, otherwise false</returns>
        bool checkInput(int min, int max, int input)
        {
            if (input < min || input > max)
            {
                Console.WriteLine("the input not valid\nentr number between {0} - {1}", min , max);
                return false;
            }
            return true;
        }

    }
}
