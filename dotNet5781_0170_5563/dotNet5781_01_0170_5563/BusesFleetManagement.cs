﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace dotNet5781_01_0170_5563
{
    public class BusesFleetManagement
    {
        static Random rand = new Random(DateTime.Now.Millisecond);
        /// <summary>
        /// this is fleet of buses stored in list
        /// </summary>
        private List<Bus> buses = new List<Bus>();
        /// <summary>
        /// the function add a new bus to the fleet and geting all the details
        /// on the bus like id etc.. and if he is new or old
        /// </summary>
        public void AddBus()
        {
            //check, if the bus is new so we have default to km fuel and fix,
            //if it is old so we will get all the details
            int condition;
            do
            {
                Console.WriteLine("what is the condition of the bus\n" +
                    "for new press: 1,  for used press: 2 ");
                condition = int.Parse(Console.ReadLine());
                if(condition != 1 && condition != 2)
                    Console.WriteLine("enter 1 or 2 only");
            } while (condition != 1 && condition != 2);

            bool valid = false;
            int year, month, day;
            Console.WriteLine("enter the active date:");
            // get the year of activitie of the bus. between 1950 to today
            //in loop to check the input is valid
            do
            {
                valid = false;
                Console.Write("year: ");
                while (!int.TryParse(Console.ReadLine(), out year))
                    Console.WriteLine("enter numbers only");
                valid = checkInput(1950, DateTime.Now.Year, year);
            } while (!valid);
            // get the month of activitie of the bus. 
            //in loop to check the input is valid
            do
            {
                valid = false;
                Console.Write("month: ");
                while (!int.TryParse(Console.ReadLine(), out month))
                    Console.WriteLine("enter numbers only");
                valid = checkInput(1, 12, month);
            } while (!valid);
            // get the day of activitie of the bus. 
            //in loop to check the input is valid
            do
            {
                valid = false;
                Console.Write("day: ");
                while (!int.TryParse(Console.ReadLine(), out day))
                    Console.WriteLine("enter numbers only");
                valid = checkInput(1, DateTime.DaysInMonth(year, month), day);
            } while (!valid);

            string id = InputId(year);
            //search if there is already bus with this licensId in the fleet
            foreach (Bus b in buses)
            {
                if (b.GetId == id)
                {
                    Console.WriteLine("the bus is in fleet already");
                    return;
                }
            }
                        
            Bus b1;
            //in case the bus is new
            if (condition == 1)
            {
                b1 = new Bus(id, year, month, day);
                buses.Add(b1);
                return;
            }

            //in case the bus is used we take hole following details
            bool check;
            double kilometrage, fuel, km;
            //loop to get the kilometrage and check the input
            do
            {
                check = false;
                Console.Write("enter the kilometrage of the bus: ");
                while (!double.TryParse(Console.ReadLine(), out kilometrage))
                    Console.WriteLine("enter number only");
                check = checkInput(0, double.MaxValue, kilometrage);
            } while (!check);

            //loop to get the fuel and check the input
            do
            {
                check = false;
                Console.Write("enter the amount of fuel in the tank: ");
                while (!double.TryParse(Console.ReadLine(), out fuel))
                    Console.WriteLine("enter number only");
                check = checkInput(0, 1200, fuel);
            } while (!check);

            //loop to get the kilometrage he can travel till next fix and check the input
            do
            {
                check = false;
                Console.Write("enter the amount of the km this bus traveled since the last fix: ");
                while (!double.TryParse(Console.ReadLine(), out km))
                    Console.WriteLine("enter number only");
                check = checkInput(0, 20000, km);
            } while (!check);
            km = 20000 - km;

            //get the date of the last fix and check input
            DateTime date = DateTime.Now;
            check = false;
            while (!check)
            {
                Console.Write("enter the date of the last fix: ");
                check = DateTime.TryParse(Console.ReadLine(), out date);
                if (!check)
                {
                    Console.WriteLine("the date is not valid");
                    continue;
                }

                // ensure that the last fix date is between active year to todays year
                if (!checkInput(year, DateTime.Now.Year, date.Year))
                    check = false;
            }

            b1 = new Bus(id, year, month, day, kilometrage, fuel, km);
            b1.LastFix = date;
            buses.Add(b1);

        }

        /// <summary>
        /// this funk is to get bus number for travel and check if the bus can take this 
        /// travel or he need fix or he dont have enough fuel
        /// </summary>
        public void ChooseBusToTravel()
        {
            //check if the list is empty
            if (buses.Count == 0)
            {
                Console.WriteLine("you don't have any bus in the fleet");
                return;
            }

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

            Console.WriteLine("the bus is ready to travel");
        }

        /// <summary>
        /// this funk get number of the bus and fuel or fix the bus
        /// </summary>
        public void FuelOrFixBus()
        {
            //check if the list is empty
            if (buses.Count == 0)
            {
                Console.WriteLine("you don't have any bus in the fleet");
                return;
            }
            
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

            int choice;
            //ask if he want to fuel or fix
            do
            {
                Console.WriteLine("To fuel press 1\nTo fix press 2");
                choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    temp.Fuel = 1200;
                    Console.WriteLine("the fueling success you have full tank");
                }
                else if (choice == 2)
                {
                    temp.KmForTravel = 20000;
                    temp.LastFix = DateTime.Now;
                    temp.Dangerous = false;
                    Console.WriteLine("the bus is fix now");
                }
            } while (choice != 1 && choice != 2);
        }

        /// <summary>
        /// this funk is to print how many km left to each bus in the fleet
        /// to travel before he will need fix
        /// </summary>
        public void ShowKmFromLastFix()
        {
            //check if the list is empty
            if (buses.Count == 0)
            {
                Console.WriteLine("you have not any bus in the fleet");
                return;
            }

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
                Console.Write("enter the licenseId number: ");
                id = Console.ReadLine();
                //check if the length of the id is valid
                if (id.Length != length)
                {
                    Console.WriteLine("the length of the licenseId number not valid");
                    Console.WriteLine("please enter licenseId in length of {0} digits", length);
                }
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
        bool checkInput(double min, double max, double input)
        {
            if (input < min || input > max)
            {
                Console.WriteLine("the input not valid\nenter number between {0} - {1}", min, max);
                return false;
            }
            return true;
        }
    }
}
