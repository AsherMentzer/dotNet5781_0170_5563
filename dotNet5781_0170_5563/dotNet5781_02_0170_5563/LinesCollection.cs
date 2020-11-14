using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
    public class LinesCollection : IEnumerable
    {
        //List<BusStationLine> Stations = new List<BusStationLine>();

       private List<BusLine> lines = new List<BusLine>();

        public LinesCollection(List<BusLine> _lines) => lines = _lines;
        public LinesCollection() { }
        public List<BusLine> Lines { get => lines; set => lines = value; }
        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)lines).GetEnumerator();
        }

        public void AddLine(BusLine newLine)
        {
            int count = 0;
            BusLine temp = null;
            foreach (var line in lines)
            {
                if (line.GetBusLine == newLine.GetBusLine)
                {
                    ++count;
                    temp = line;
                }
            }
            if (count == 0)
            {
                lines.Add(newLine);
                Console.WriteLine("the addition successed");
                return;
            }
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
            else throw new ArgumentException("the addition failed, the line is in the lines' list 2 time alraedy");
              //  Console.WriteLine("the addition failed, the line is in the lines' list 2 time alraedy");
        }

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

            if (StationLines.Count == 0) throw new KeyNotFoundException("there is no lines in this station");
            return StationLines;
        }

        public List<BusLine> SortLines()
        {
            List<BusLine> SortedLines = new List<BusLine>();
            foreach(BusLine item in lines)
            {
                SortedLines.Add(item);
            }

            SortedLines.Sort();
            
            return SortedLines;
        }

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
                    if(indexLines.Count == 2)
                        return indexLines;
                }
                if (indexLines.Count == 0)
                    throw new KeyNotFoundException("this line not exist");
                return indexLines;
            }
        }

    }
}
