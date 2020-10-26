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
            // List<Bus> buses = new List<Bus>();
            BusesFleetManagement b1= new BusesFleetManagement();
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
                      b1.addBus();
                        break;
                    case Menu.chooseBus:
                        b1.chooseBusToTravel();
                        break;
                    case Menu.fuelBusOrfixBus:
                        break;
                    case Menu.showKmFromLastFix:
                        break;
                    default:
                        break;
                }
            }
            while (choice != 0);


            Bus b1=new Bus("hello",1990,01,06);
            b1.Id = b1.Id.Insert(3,"-");
            Console.WriteLine(b1.Id);
            Console.ReadKey();
        }
    }
}
