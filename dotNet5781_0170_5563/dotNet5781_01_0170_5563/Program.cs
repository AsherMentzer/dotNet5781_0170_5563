using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_0170_5563
{
    using Bus;
    //using BusesFleetManagement;
    
    class Program
    {
     
        public enum Menu {exit,addBus,chooseBus,fuelBusOrfixBus,showKmFromLastFix};
   
        static void Main(string[] args)
        {
            Console.WriteLine(tostring(DateTime.Now.Year)); 
            // List<Bus> buses = new List<Bus>();
            BusesFleetManagement fleet= new BusesFleetManagement();
            int choice;
            do
            {
                Console.WriteLine("1: add a bus to the list");
                Console.WriteLine("2: choose the bus you want to use for the travel");
                Console.WriteLine("3: fix or fuel a bus");
                Console.WriteLine("4: show how much km all the buses traveled from last fix");
                Console.WriteLine("0: exit");
               
                while (!int.TryParse(Console.ReadLine(), out choice))
                    Console.WriteLine("Wrong input, enter a number again:");

                switch ((Menu)choice)
                {
                    case Menu.exit:
                        break;
                    case Menu.addBus:
                      fleet.addBus();
                        break;
                    case Menu.chooseBus:
                        fleet.chooseBusToTravel();
                        break;
                    case Menu.fuelBusOrfixBus:
                        fleet.fuelOrFixBus();
                        break;
                    case Menu.showKmFromLastFix:
                        break;
                    default:
                        break;
                }
            }
            while (choice != 0);


        
            Console.ReadKey();
        }
    }
}
