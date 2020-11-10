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
            areas a1=areas.Center;
            areas a2=areas.General;
           
           
            BusStation b1 = new BusStation(1234561, (float)31.01, (float)32.01);
            BusStation b2 = new BusStation(1234562, (float)31.02, (float)32.02);
            BusStation b3 = new BusStation(1234563, (float)31.03, (float)32.03);
            BusStation b4 = new BusStation(1234564, (float)31.04, (float)32.04);
            BusStation b5 = new BusStation(1234565, (float)31.05, (float)32.05);
            BusStation b6 = new BusStation(1234566, (float)31.06, (float)32.06);
            BusStation b7 = new BusStation(1234567, (float)31.07, (float)32.07);
            BusStation b8 = new BusStation(1234568, (float)31.08, (float)32.08);
            BusStation b9 = new BusStation(1234569, (float)31.09, (float)32.09);
            BusStation b10 = new BusStation(1234510, (float)31.10, (float)32.10);
            BusStation b11 = new BusStation(1234511, (float)31.11, (float)32.11);

            BusStationLine bl1 = new BusStationLine(b1);
            BusStationLine bl2 = new BusStationLine(b2);
            BusStationLine bl3 = new BusStationLine(b3);
            BusStationLine bl4 = new BusStationLine(b4);
            BusStationLine bl5 = new BusStationLine(b5);
            BusStationLine bl6 = new BusStationLine(b6);
            BusStationLine bl7 = new BusStationLine(b7);
            BusStationLine bl8 = new BusStationLine(b8);
            BusStationLine bl9 = new BusStationLine(b9);
            BusStationLine bl10 = new BusStationLine(b10);
            BusStationLine bl11 = new BusStationLine(b11);

            BusLine l1 = new BusLine(1, bl1, bl3,a1);
            BusLine l2 = new BusLine(1, bl3, bl1,a1);
            BusLine l3 = new BusLine(2, bl5, bl10,a1);
            BusLine l4 = new BusLine(3, bl8, bl10,a1);

            List<BusStation> busStops = new List<BusStation> { b1,b2,b3,b4,b5,b6,b7,b8,b9,b10,b11};
            List<BusLine> lines = new List<BusLine> { l1, l2, l3 };
            LinesCollection linesCo = new LinesCollection ();
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
                        linesCo.AddLine(l3);                      
                        break;
                    case Menu.delete:
                        linesCo.RemoveLine(2, 1234565);
                        break;
                    case Menu.search:

                        break;
                    case Menu.print:
                        BusLine line = linesCo.Lines[0];
                        string l=line.ToString();
                        Console.WriteLine(l);
                        break;
                    default:
                        break;
                }
            }
            while (choice != 0);
        }
    }
}
