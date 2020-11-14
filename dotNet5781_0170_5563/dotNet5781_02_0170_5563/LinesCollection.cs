using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
    /// <summary>
    /// class that contain all the lines and operations on them like add line etc...
    /// </summary>
    public class LinesCollection : IEnumerable
    {
        /// <summary>
        /// list that contain all the lines
        /// </summary>
        private List<BusLine> lines = new List<BusLine>();
        /// <summary>
        /// constructor that get list
        /// </summary>
        /// <param name="_lines">list of lines</param>
        public LinesCollection(List<BusLine> _lines) => lines = _lines;

        /// <summary>
        /// default constructor
        /// </summary>
        public LinesCollection() { }

        /// <summary>
        /// getter and setter
        /// </summary>
        public List<BusLine> Lines { get => lines; set => lines = value; }

        /// <summary>
        /// get enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)lines).GetEnumerator();
        }
        /// <summary>
        /// add line to the list if the lines
        /// </summary>
        /// <param name="newLine">new line to add</param>
        public void AddLine(BusLine newLine)
        {
            int count = 0;
            BusLine temp = null;
            //check if the line already exist
            foreach (var line in lines)
            {
                if (line.GetBusLine == newLine.GetBusLine)
                {
                    ++count;
                    temp = line;
                }
            }
            //in case not exsit
            if (count == 0)
            {
                lines.Add(newLine);//add the line
                Console.WriteLine("the addition successed");
                return;
            }
            //in case is exist once you can add another 0ne only if the first=last and last=first
            else if (count == 1)
            {
                if (temp.FirstStation.GetBusStationNumber == newLine.LastStation.GetBusStationNumber
                    && temp.LastStation.GetBusStationNumber == newLine.FirstStation.GetBusStationNumber)
                {
                    lines.Add(newLine);
                    Console.WriteLine("the addition successed");
                }
                else throw new ArgumentException("the first or last station of the new line isn't valid");
                // Console.WriteLine("the first or last station of the new line isn't valid");
            }
            //in case is exist 2 times you can't add another one
            else throw new ArgumentException("the addition failed, the line is in the lines' list 2 time alraedy");
            //  Console.WriteLine("the addition failed, the line is in the lines' list 2 time alraedy");
        }
        /// <summary>
        /// remove line from the collection
        /// </summary>
        /// <param name="lineNumber">num of the line</param>
        /// <param name="firstStationNum">the first station in the line</param>
        public void RemoveLine(int lineNumber, int firstStationNum)
        {
            BusLine temp;
            IEnumerator enumerator = lines.GetEnumerator();
            while (enumerator.MoveNext())
            {
                temp = (BusLine)enumerator.Current;
                if (temp.GetBusLine == lineNumber &&
                    temp.FirstStation.GetBusStationNumber == firstStationNum)
                {
                    lines.Remove(temp);
                    return;
                }
            }
            throw new KeyNotFoundException("the requested line not found");
            //Console.WriteLine("the requested line not found");
        }
        /// <summary>
        /// find all the lines that stop in spesic station
        /// </summary>
        /// <param name="stationNum">the num of the station</param>
        /// <returns>return list of all the linr=es that stop there</returns>
        public List<BusLine> GetLinesinStation(int stationNum)
        {
            List<BusLine> StationLines = new List<BusLine>();
            foreach (BusLine line in lines)
            {
                foreach (BusStationLine station in line.stations)
                {
                    if (station.GetBusStationNumber == stationNum)
                    {
                        StationLines.Add(line);
                        break;
                    }
                }
            }
            //in case not line found
            if (StationLines.Count == 0) throw new KeyNotFoundException("there is no lines in this station");
            return StationLines;
        }
        /// <summary>
        /// sort the lines by travel time from shortest to longest
        /// </summary>
        /// <returns>return the sorted list</returns>
        public List<BusLine> SortLines()
        {
            List<BusLine> SortedLines = new List<BusLine>();
            foreach (BusLine item in lines)
            {
                SortedLines.Add(item);
            }

            SortedLines.Sort();

            return SortedLines;
        }
        /// <summary>
        /// get num of line and return the line
        /// </summary>
        /// <param name="index">the num of the line</param>
        /// <returns>list of all the lines with this number</returns>
        public List<BusLine> this[int index]
        {
            get
            {
                if (index < 1 || index > 999)
                    throw new ArgumentOutOfRangeException("the lines numbers are between 1-999");

                List<BusLine> indexLines = new List<BusLine>();
                foreach (BusLine line in lines)
                {
                    if (line.GetBusLine == index)
                        indexLines.Add(line);
                    if (indexLines.Count == 2)
                        return indexLines;
                }
                if (indexLines.Count == 0)
                    throw new KeyNotFoundException("this line not exist");
                return indexLines;
            }
        }

    }
}
