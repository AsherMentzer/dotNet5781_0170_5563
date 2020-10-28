using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_0170_5563
{
    class Program
    {

        public enum Menu { exit, addBus, chooseBus, fuelBusOrfixBus, showKmFromLastFix };

        static void Main(string[] args)
        {
            BusesFleetManagement fleet = new BusesFleetManagement();
            int choice;
            do
            {
                Console.WriteLine("\n1: add a bus to the list");
                Console.WriteLine("2: choose a bus for your travel");
                Console.WriteLine("3: fix or fuel a bus");
                Console.WriteLine("4: show how many KM traveled each bus in the fleet after the last fix");
                Console.WriteLine("0: exit");

                while (!int.TryParse(Console.ReadLine(), out choice))
                    Console.WriteLine("Wrong input, enter a number again:");

                switch ((Menu)choice)
                {
                    case Menu.exit:
                        break;
                    case Menu.addBus:
                        fleet.AddBus();
                        break;
                    case Menu.chooseBus:
                        fleet.ChooseBusToTravel();
                        break;
                    case Menu.fuelBusOrfixBus:
                        fleet.FuelOrFixBus();
                        break;
                    case Menu.showKmFromLastFix:
                        fleet.ShowKmFromLastFix();
                        break;
                    default:
                        break;
                }
            }
            while (choice != 0);
        }
    }
}
