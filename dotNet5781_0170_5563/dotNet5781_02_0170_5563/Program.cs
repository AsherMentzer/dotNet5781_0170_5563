using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
    class Program
    {
        public enum Menu { exit, add, delete, search, print };
        static void Main(string[] args)
        {
            areas a1 = areas.Center;
            areas a2 = areas.General;


            BusStation s1 = new BusStation(1231, (float)31.01, (float)34.31);
            BusStation s2 = new BusStation(1232, (float)31.02, (float)34.32);
            BusStation s3 = new BusStation(1233, (float)31.03, (float)34.33);
            BusStation s4 = new BusStation(1234, (float)31.04, (float)34.34);
            BusStation s5 = new BusStation(1235, (float)31.05, (float)34.35);
            BusStation s6 = new BusStation(1236, (float)31.06, (float)34.36);
            BusStation s7 = new BusStation(1237, (float)31.07, (float)34.37);
            BusStation s8 = new BusStation(1238, (float)31.08, (float)34.38);
            BusStation s9 = new BusStation(1239, (float)31.09, (float)34.39);
            BusStation s10 = new BusStation(1230, (float)31.10, (float)34.40);

            BusStation s11 = new BusStation(12340, (float)31.11, (float)34.51);
            BusStation s12 = new BusStation(12341, (float)31.12, (float)34.52);
            BusStation s13 = new BusStation(12342, (float)31.13, (float)34.53);
            BusStation s14 = new BusStation(12343, (float)31.14, (float)34.54);
            BusStation s15 = new BusStation(12344, (float)31.15, (float)34.55);
            BusStation s16 = new BusStation(12345, (float)31.16, (float)34.56);
            BusStation s17 = new BusStation(12346, (float)31.17, (float)34.57);
            BusStation s18 = new BusStation(12347, (float)31.18, (float)34.58);
            BusStation s19 = new BusStation(12348, (float)31.19, (float)34.59);
            BusStation s20 = new BusStation(12349, (float)31.20, (float)34.60);

            BusStation s21 = new BusStation(123451);
            BusStation s22 = new BusStation(123452);
            BusStation s23 = new BusStation(123453);
            BusStation s24 = new BusStation(123454);
            BusStation s25 = new BusStation(123455);
            BusStation s26 = new BusStation(123456);
            BusStation s27 = new BusStation(123457);
            BusStation s28 = new BusStation(123458);
            BusStation s29 = new BusStation(123459);
            BusStation s30 = new BusStation(120);

            BusStation s31 = new BusStation(121);
            BusStation s32 = new BusStation(123);
            BusStation s33 = new BusStation(124);
            BusStation s34 = new BusStation(125);
            BusStation s35 = new BusStation(126);
            BusStation s36 = new BusStation(127);
            BusStation s37 = new BusStation(128);
            BusStation s38 = new BusStation(129);
            BusStation s39 = new BusStation(1222);
            BusStation s40 = new BusStation(12336);

            /*   BusStationLine bl1 = new BusStationLine(s1);
               BusStationLine bl15 = new BusStationLine(s1);
               BusStationLine bl2 = new BusStationLine(s2);
               BusStationLine bl3 = new BusStationLine(s3);
               BusStationLine bl4 = new BusStationLine(s4);
               BusStationLine bl5 = new BusStationLine(s5);
               BusStationLine bl6 = new BusStationLine(s6);
               BusStationLine bl7 = new BusStationLine(s7);
               BusStationLine bl8 = new BusStationLine(s8);
               BusStationLine bl9 = new BusStationLine(s9);
               BusStationLine bl10 = new BusStationLine(s10);
               BusStationLine bl11 = new BusStationLine(s11);*/

            BusLine l1 = new BusLine(1, s31, s40, a1);
            BusLine l2 = new BusLine(1, s10, s1, a1);
            BusLine l3 = new BusLine(2, s11, s20, a1);
            BusLine l4 = new BusLine(2, s20, s11, a1);
            BusLine l5 = new BusLine(3, s35, s25, a1);
            BusLine l6 = new BusLine(4, s4, s5, a2);
            BusLine l7 = new BusLine(5, s22, s28, a2);
            BusLine l8 = new BusLine(6, s18, s10, a2);
            BusLine l9 = new BusLine(7, s8, s36, a2);
            BusLine l10 = new BusLine(8, s28, s2, a2);

            l1.addStationToLine(s2);
            l1.addStationToLine(s21);
            l1.addStationToLine(s5);
            l1.addStationToLine(s22);
            l1.addStationToLine(s6);
            l1.addStationToLine(s23);
            l1.addStationToLine(s9);
            l1.addStationToLine(s24);

            List<BusStation> busStations = new List<BusStation> { s1,s2,s3,s4,s5,s6,s7,s8,s9,s10,s11,s12,s13,s14,s15
            ,s16,s17,s18,s19,s20,s21,s22,s23,s24,s25,s26,s27,s28,s29,s30,s31,s32,s33,s34,s35,s36,s37,s38,s39,s40};
            /* bool isStation(int stationNumber)
            {
                foreach (var station in busStations)
                {
                    if (station.BusStationNumber == stationNumber)
                        return true;
                }
                return false;
            }*/
            BusStation findStation(int stationNum)
            {
                foreach (var station in busStations)
                {
                    if (station.BusStationNumber == stationNum)
                        return station;
                }
                return null;
            }
            LinesCollection lines = new LinesCollection();
            int choice;
            do
            {
                Console.WriteLine("press number to choose");
                Console.WriteLine("1: add new bus line\n    or add station to bus line");
                Console.WriteLine("2: delete bus line\n    or delete station from bus line");
                Console.WriteLine("3: all the lines in the station\n    or quick line to destanition");
                Console.WriteLine("4: print all the lines\n    or print all stations and the lines inside them");
                Console.WriteLine("0: exit");

                while (!int.TryParse(Console.ReadLine(), out choice))
                    Console.WriteLine("Wrong input, enter a number again:");

                switch ((Menu)choice)
                {
                    case Menu.add:
                        {
                            Console.WriteLine("  1: add line\n  2: add station to line");
                            int choose;
                            while (!int.TryParse(Console.ReadLine(), out choose)
                                || (choose != 1 && choose != 2))
                                Console.WriteLine("enter only 1 or 2");
                            if (choose == 1)
                            {
                                Console.WriteLine("enter line number");
                                int lineNumber;
                                while (!int.TryParse(Console.ReadLine(), out lineNumber)
                           || (lineNumber < 1 || lineNumber > 999))
                                    Console.WriteLine("enter only number between 1-999");

                                Console.WriteLine("enter the area: 1:General,2:North,3:west,4:Center,5:jerusalem");
                                int area;
                                while (!int.TryParse(Console.ReadLine(), out area)
                          || (lineNumber < 1 || lineNumber > 6))
                                    Console.WriteLine("enter only number between 1-5");

                                Console.WriteLine("enter first station number");
                                int stationNumber;
                                while (!int.TryParse(Console.ReadLine(), out stationNumber)
                              || (stationNumber < 1 || stationNumber > 999999))
                                    Console.WriteLine("enter only number between 1-999999");
                                BusStation stationF = findStation(stationNumber);
                                if (stationF == null)
                                {
                                    Console.WriteLine("this station not exist");
                                    break;
                                }
                                Console.WriteLine("enter last station number");

                                while (!int.TryParse(Console.ReadLine(), out stationNumber)
                              || (stationNumber < 1 || stationNumber > 999999))
                                    Console.WriteLine("enter only number between 1-999999");
                                BusStation stationL = findStation(stationNumber);
                                if (stationL == null)
                                {
                                    Console.WriteLine("this station not exist");
                                    break;
                                }
                                BusLine newBusLine = new BusLine(lineNumber, stationF, stationL, (areas)area);

                                try
                                {
                                    lines.AddLine(newBusLine);
                                }
                                catch (ArgumentException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }

                            }
                            else
                            {
                                Console.WriteLine("enter line number");
                                int lineNumber;
                                while (!int.TryParse(Console.ReadLine(), out lineNumber)
                           || (lineNumber < 1 || lineNumber > 999))
                                    Console.WriteLine("enter only number between 1-999");
                                List<BusLine> lin = null;
                                try
                                {
                                    lin = lines[lineNumber];
                                }
                                catch (KeyNotFoundException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    break;
                                }
                                Console.WriteLine("enter station number");
                                int stationNumber;
                                while (!int.TryParse(Console.ReadLine(), out stationNumber)
                              || (stationNumber < 1 || stationNumber > 999999))
                                    Console.WriteLine("enter only number between 1-999999");
                                BusStation station = findStation(stationNumber);
                                if (station == null)
                                {
                                    Console.WriteLine("this station not exist");
                                    break;
                                }

                                else if (lin.Count == 1)
                                    try
                                    {
                                        lin[0].addStationToLine(station);
                                    }
                                    catch (DuplicateNameException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        break;
                                    }
                                else
                                {
                                    Console.WriteLine($"1: from station {0}\n2: from station {1}"
                                        , lin[0].FirstStation.GetBusStationNumber, lin[1].FirstStation.GetBusStationNumber);
                                    while (!int.TryParse(Console.ReadLine(), out choose) || (choose != 1 && choose != 2))
                                        Console.WriteLine("enter only 1 or 2");
                                    if (choose == 1)
                                    {
                                        try
                                        {
                                            lin[0].addStationToLine(station);
                                        }
                                        catch (DuplicateNameException ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            lin[1].addStationToLine(station);
                                        }
                                        catch (DuplicateNameException ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                            break;
                                        }
                                    }
                                }
                                break;
                            }
                            break;
                        }
                    case Menu.delete:
                        {
                            Console.WriteLine("  1: delete line\n  2: delete station from line");
                            int input;
                            while (!int.TryParse(Console.ReadLine(), out input)
                                || (input != 1 && input != 2))
                                Console.WriteLine("enter only 1 or 2");
                            if (input == 1)
                            {
                                Console.WriteLine("enter line number");
                                while (!int.TryParse(Console.ReadLine(), out input)
                           || (input < 1 || input > 999))
                                    Console.WriteLine("enter only number between 1-999");
                                try
                                {
                                    List<BusLine> tempList = lines[input];
                                    if (tempList.Count == 1)
                                        lines.RemoveLine(input, tempList[0].FirstStation.GetBusStationNumber);
                                    else
                                        Console.WriteLine($"  1: delete the lie from {0}\n  2: delete the line from {1}," +
                                            $" tempList[0].FirstStation.GetBusStationNumber, tempList[1].FirstStation.GetBusStationNumber");
                                    int choose;
                                    while (!int.TryParse(Console.ReadLine(), out choose)
                                || (choose != 1 && choose != 2))
                                        Console.WriteLine("enter only 1 or 2");
                                    if (choose == 1)
                                        lines.RemoveLine(input, tempList[0].FirstStation.GetBusStationNumber);
                                    else
                                        lines.RemoveLine(input, tempList[1].FirstStation.GetBusStationNumber);
                                }
                                catch (KeyNotFoundException ex) { Console.WriteLine(ex.Message); }
                            }
                            else
                            {
                                Console.WriteLine("enter line number");
                                while (!int.TryParse(Console.ReadLine(), out input)
                           || (input < 1 || input > 999))
                                    Console.WriteLine("enter only number between 1-999");
                                try
                                {
                                    List<BusLine> tempList = lines[input];
                                    int stationNumber;
                                    Console.WriteLine("enter the numbet of the station to delete");
                                    while (!int.TryParse(Console.ReadLine(), out stationNumber)
                                  || (stationNumber < 1 || stationNumber > 999999))
                                        Console.WriteLine("enter only number between 1-999999");
                                    if (tempList.Count == 1)
                                    {  //lines[input][0].DeleteStstion(;
                                        foreach (var stationInLine in tempList[0].stations)
                                        {
                                            if (stationNumber == stationInLine.GetBusStationNumber)
                                            {
                                                tempList[0].DeleteStstion(stationInLine);
                                                break;
                                            }
                                        }
                                    }
                                    else
                                        Console.WriteLine($"  1: delete station of line from {0}\n  2: delete station of line from {1}," +
                                            $" tempList[0].FirstStation.GetBusStationNumber, tempList[1].FirstStation.GetBusStationNumber");
                                    int choose;
                                    while (!int.TryParse(Console.ReadLine(), out choose)
                                || (choose != 1 && choose != 2))
                                        Console.WriteLine("enter only 1 or 2");
                                    if (choose == 1)
                                        foreach (var stationInLine in tempList[0].stations)
                                        {
                                            if (stationNumber == stationInLine.GetBusStationNumber)
                                            {
                                                tempList[0].DeleteStstion(stationInLine);
                                                break;
                                            }
                                        }
                                    else
                                        foreach (var stationInLine in tempList[0].stations)
                                        {
                                            if (stationNumber == stationInLine.GetBusStationNumber)
                                            {
                                                tempList[1].DeleteStstion(stationInLine);
                                                break;
                                            }
                                        }
                                }
                                catch (KeyNotFoundException ex) { Console.WriteLine(ex.Message); }
                            }
                            break;
                        }
                    case Menu.search:
                        {
                            int input;
                            Console.WriteLine("  1: search buses which stop instation\n  " +
                                "2: print the options to travel from one statin to another");
                            while (!int.TryParse(Console.ReadLine(), out input)
                                || (input != 1 && input != 2))
                                Console.WriteLine("enter only 1 or 2");
                            if (input == 1)
                            {
                                Console.WriteLine("enter station number");
                                int stationNumber;
                                while (!int.TryParse(Console.ReadLine(), out stationNumber)
                              || (stationNumber < 1 || stationNumber > 999999))
                                    Console.WriteLine("enter only number between 1-999999");
                                List<BusLine> linesStation = linesInStation(stationNumber, lines.Lines);
                            }
                            else
                            {
                                Console.WriteLine("enter station number");
                                int stationNum1;
                                while (!int.TryParse(Console.ReadLine(), out stationNum1)
                              || (stationNum1 < 1 || stationNum1 > 999999))
                                    Console.WriteLine("enter only number between 1-999999");

                                Console.WriteLine("enter station number");
                                int stationNum2;
                                while (!int.TryParse(Console.ReadLine(), out stationNum2)
                              || (stationNum2 < 1 || stationNum2 > 999999)) ;
                                Console.WriteLine("enter only number between 1-999999");

                                List<BusLine> linesStation1 = linesInStation(stationNum1, lines.Lines);
                                List<BusLine> finalList = linesInStation(stationNum2, linesStation1);
                                LinesCollection tempCol = new LinesCollection(finalList);
                                tempCol.SortLines();
                                foreach (BusLine item in tempCol.Lines)
                                {
                                    Console.WriteLine(item.ToString());
                                }
                            }
                            break;
                        }
                    case Menu.print:
                        {
                            int input;
                            Console.WriteLine("  1: print all lines\n  2: print all stations and the lines which stop there");
                            while (!int.TryParse(Console.ReadLine(), out input)
                                || (input != 1 && input != 2))
                                Console.WriteLine("enter only 1 or 2");

                            if (input == 1)
                                foreach (var busline in lines)
                                {
                                    Console.WriteLine(busline.ToString());
                                }
                            else
                            {
                                foreach (var station in busStations)
                                {
                                    Console.Write($"station num: {station.BusStationNumber}, lines: ");
                                    List<BusLine> lst = linesInStation(station.BusStationNumber, lines.Lines);
                                    foreach (BusLine line in lst)
                                    {
                                        Console.Write(line.GetBusLine + ", ");
                                    }
                                    Console.WriteLine();
                                }
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
            while (choice != 0);


            List<BusLine> linesInStation(int stationNum, List<BusLine> list)
            {
                List<BusLine> linesStation = new List<BusLine>();

                foreach (BusLine line in list)
                {
                    foreach (var station in line.stations)
                    {
                        if (station.GetBusStationNumber == stationNum)
                        {
                            linesStation.Add(line);
                            break;
                        }
                    }
                }
                return linesStation;
            }
        }
    }
}
