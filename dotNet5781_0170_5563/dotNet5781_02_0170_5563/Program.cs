﻿using System;
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
            Data d = new Data();

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

                            //in case add line
                            if (choose == 1)
                            {
                                //get the line number
                                Console.WriteLine("enter line number");
                                int lineNumber;
                                while (!int.TryParse(Console.ReadLine(), out lineNumber)
                                        || (lineNumber < 1 || lineNumber > 999))
                                    Console.WriteLine("enter only number between 1-999");

                                //get the area of the line
                                Console.WriteLine("enter the area: 1:General,2:North,3:west,4:Center,5:jerusalem");
                                int area;
                                while (!int.TryParse(Console.ReadLine(), out area)
                                    || (area < 1 || area > 5))
                                    Console.WriteLine("enter only number between 1-5");

                                //get the first station in the line
                                Console.WriteLine("enter first station number");
                                int stationNumber;
                                while (!int.TryParse(Console.ReadLine(), out stationNumber)
                              || (stationNumber < 1 || stationNumber > 999999))
                                    Console.WriteLine("enter only number between 1-999999");

                                //check if the station exist
                                BusStation stationF = findStation(stationNumber);
                                if (stationF == null)
                                {
                                    Console.WriteLine("this station not exist");
                                    break;
                                }

                                //get the last station and check if it exist
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

                                //create the line 
                                BusLine newBusLine = new BusLine(lineNumber, stationF, stationL, (areas)area);

                                //try to add the line
                                try
                                {
                                    d.lines.AddLine(newBusLine);
                                }
                                catch (ArgumentException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }

                            }
                            //in case add station
                            if (choose == 2)
                            {
                                //get the line number and check if exist
                                Console.WriteLine("enter line number");
                                int lineNumber;
                                while (!int.TryParse(Console.ReadLine(), out lineNumber)
                           || (lineNumber < 1 || lineNumber > 999))
                                    Console.WriteLine("enter only number between 1-999");
                                List<BusLine> lin = null;
                                try
                                {
                                    lin = d.lines[lineNumber];
                                }
                                catch (KeyNotFoundException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    break;
                                }

                                //get the station number and check if exist
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

                                //if there is only 1 line with this number
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
                                else//if there is 2 lines with this number
                                {
                                    //ask in witch line to add
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

                            //in case delete line
                            if (input == 1)
                            {
                                //get the line number
                                Console.WriteLine("enter line number");
                                while (!int.TryParse(Console.ReadLine(), out input)
                           || (input < 1 || input > 999))
                                    Console.WriteLine("enter only number between 1-999");
                                //check if exist
                                try
                                {
                                    List<BusLine> tempList = d.lines[input];
                                    if (tempList.Count == 1)//in case only 1 line with this number
                                    {
                                        d.lines.RemoveLine(input, tempList[0].FirstStation.GetBusStationNumber);
                                        Console.WriteLine("the line removed");
                                        break;
                                    }
                                    else//in case 2 lines with this number ask witch one to delete from
                                        Console.WriteLine($"  1: delete the line from {0}\n  2: delete the line from {1}," +
                                            $" tempList[0].FirstStation.GetBusStationNumber, tempList[1].FirstStation.GetBusStationNumber");
                                    int choose;
                                    while (!int.TryParse(Console.ReadLine(), out choose)
                                || (choose != 1 && choose != 2))
                                        Console.WriteLine("enter only 1 or 2");
                                    if (choose == 1)
                                        d.lines.RemoveLine(input, tempList[0].FirstStation.GetBusStationNumber);
                                    else
                                        d.lines.RemoveLine(input, tempList[1].FirstStation.GetBusStationNumber);
                                }
                                catch (KeyNotFoundException ex) { Console.WriteLine(ex.Message); }
                            }

                            //in case delete station
                            if (input == 2)
                            {
                                //get the line number
                                Console.WriteLine("enter line number");
                                while (!int.TryParse(Console.ReadLine(), out input)
                           || (input < 1 || input > 999))
                                    Console.WriteLine("enter only number between 1-999");
                                //checke if exist
                                try
                                {
                                    List<BusLine> tempList = d.lines[input];

                                    //get the station number
                                    int stationNumber;
                                    Console.WriteLine("enter the numbet of the station to delete");
                                    while (!int.TryParse(Console.ReadLine(), out stationNumber)
                                  || (stationNumber < 1 || stationNumber > 999999))
                                        Console.WriteLine("enter only number between 1-999999");

                                    //if the line number exist once
                                    if (tempList.Count == 1)
                                    {
                                        foreach (var stationInLine in tempList[0].stations)
                                        {
                                            //chec the location of the station and remove it
                                            if (stationNumber == stationInLine.GetBusStationNumber)
                                            {
                                                tempList[0].DeleteStstion(stationInLine);
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                    else//in case the line number exist twice ask witch one
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
                                //in case line not found
                                catch (KeyNotFoundException ex) { Console.WriteLine(ex.Message); }
                                //in case only 2 stations in the line
                                catch (MinimumStationsException ex) { Console.WriteLine(ex.Message); }
                            }
                            break;
                        }
                    case Menu.search:
                        {
                            int input;
                            Console.WriteLine("  1: search lines which stop in a station\n  " +
                                "2: print the options to travel from one statin to another");
                            while (!int.TryParse(Console.ReadLine(), out input)
                                || (input != 1 && input != 2))
                                Console.WriteLine("enter only 1 or 2");
                            //search all the lines that stop in a station
                            if (input == 1)
                            {
                                //get the station number
                                Console.WriteLine("enter station number");
                                int stationNumber;
                                while (!int.TryParse(Console.ReadLine(), out stationNumber)
                              || (stationNumber < 1 || stationNumber > 999999))
                                    Console.WriteLine("enter only number between 1-999999");

                                //get all the lines that stop there
                                List<BusLine> linesStation = linesInStation(stationNumber, d.lines.Lines);

                                //print the bumbers of the lines
                                foreach (BusLine busLine in linesStation)
                                    Console.Write(busLine.GetBusLine + ", ");
                                Console.WriteLine();
                            }

                            //get 2 stations and return all the lines that travel from one to another
                            //and sort the lines from the shortest travrl time to longest
                            if (input == 2)
                            {
                                //get the starting station number
                                Console.WriteLine("enter starting station number");
                                int stationNum1;
                                while (!int.TryParse(Console.ReadLine(), out stationNum1)
                              || (stationNum1 < 1 || stationNum1 > 999999))
                                    Console.WriteLine("enter only number between 1-999999");

                                //get the destination station number
                                Console.WriteLine("enter destination station number");
                                int stationNum2;
                                while (!int.TryParse(Console.ReadLine(), out stationNum2)
                              || (stationNum2 < 1 || stationNum2 > 999999))
                                    Console.WriteLine("enter only number between 1-999999");


                                List<BusLine> finalList = new List<BusLine>();
                                //serach all the lines that travel from staring station to the destination
                                foreach (BusLine line in d.lines.Lines)
                                {
                                    bool first = false;
                                    foreach (var station in line.stations)
                                    {
                                        if (station.GetBusStationNumber == stationNum1)
                                            first = true;
                                        if (first && station.GetBusStationNumber == stationNum2)
                                            finalList.Add(line);
                                    }
                                }
                                LinesCollection tempCol = new LinesCollection(finalList);
                                //sort the list of lines to short travel time
                                finalList = tempCol.SortLines();

                                //print all the lines numbers
                                foreach (var item in finalList)
                                {
                                    Console.Write(item.GetBusLine + ", ");
                                }
                                Console.WriteLine();
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

                            //print all the lines in the system 
                            if (input == 1)
                                foreach (var busline in d.lines)
                                {
                                    Console.WriteLine(busline.ToString());
                                }

                            //print all the stations and the lines that stop there
                            if (input == 2)
                            {
                                foreach (var station in d.busStations)
                                {
                                    Console.Write($"station num: {station.BusStationNumber}, lines: ");
                                    List<BusLine> list = linesInStation(station.BusStationNumber, d.lines.Lines);
                                    foreach (BusLine line in list)
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

            //fnc to find all the lines that stop in a station
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

            //fnk to find if the station exist
            BusStation findStation(int stationNum)
            {
                foreach (var station in d.busStations)
                {
                    if (station.BusStationNumber == stationNum)
                        return station;
                }
                return null;
            }
        }
    }
}
