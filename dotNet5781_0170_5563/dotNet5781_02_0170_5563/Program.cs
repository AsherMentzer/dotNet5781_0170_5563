using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
    class Program
    {
        public enum Menu { exit, add, delete, search, print };
        static void Main(string[] args)
        {
            BusStop b1 = new BusStop(1234561, (float)31.01, (float)32.01);
            BusStop b2 = new BusStop(1234562, (float)31.02, (float)32.02);
            BusStop b3 = new BusStop(1234563, (float)31.03, (float)32.03);
            BusStop b4 = new BusStop(1234564, (float)31.04, (float)32.04);
            BusStop b5 = new BusStop(1234565, (float)31.05, (float)32.05);
            BusStop b6 = new BusStop(1234566, (float)31.06, (float)32.06);
            BusStop b7 = new BusStop(1234567, (float)31.07, (float)32.07);
            BusStop b8 = new BusStop(1234568, (float)31.08, (float)32.08);
            BusStop b9 = new BusStop(1234569, (float)31.09, (float)32.09);
            BusStop b10 = new BusStop(1234510, (float)31.10, (float)32.10);
            BusStop b11 = new BusStop(1234511, (float)31.11, (float)32.11);

            BusStopLine bl1 = new BusStopLine(b1);
            BusStopLine bl2 = new BusStopLine(b2, 30, 50);
            BusStopLine bl3 = new BusStopLine(b3, 20, 15);
            BusStopLine bl4 = new BusStopLine(b4);
            BusStopLine bl5 = new BusStopLine(b5);
            BusStopLine bl6 = new BusStopLine(b6, 15, 5);
            BusStopLine bl7 = new BusStopLine(b7);
            BusStopLine bl8 = new BusStopLine(b8);
            BusStopLine bl9 = new BusStopLine(b9);
            BusStopLine bl10 = new BusStopLine(b10);
            BusStopLine bl11 = new BusStopLine(b11);

            BusLine l1 = new BusLine(1, bl1, bl3);
            BusLine l2 = new BusLine(1, bl3, bl1);
            BusLine l3 = new BusLine(2, bl5, bl10);
            BusLine l4 = new BusLine(3, bl8, bl10);

            List<BusStop> busStops = new List<BusStop> { b1,b2,b3,b4,b5,b6,b7,b8,b9,b10,b11};
            List<BusLine> lines = new List<BusLine> { l1, l2, l3 };
            LinesCollection linesCo = new LinesCollection (lines);
            int choice;
            do
            {
                Console.WriteLine("press number to choose");
                Console.WriteLine("1: add new bus line\n or add station to bus line");
                Console.WriteLine("2: delete bus line\n or delete station from bus line ");
                Console.WriteLine("3: all the lines in the station\no r quick line to destanition");
                Console.WriteLine("4: print all the lines\n or print all stations and the lines inside them");
                Console.WriteLine("0: exit");

                while (!int.TryParse(Console.ReadLine(), out choice))
                    Console.WriteLine("Wrong input, enter a number again:");

                switch ((Menu)choice)
                {
                    case Menu.add:
                        linesCo.AddLine(l4);
                        break;
                    case Menu.delete:

                        break;
                    case Menu.search:

                        break;
                    case Menu.print:


                        break;
                    default:
                        break;
                }
            }
            while (choice != 0);
        }
    }
}
