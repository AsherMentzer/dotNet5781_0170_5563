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
        List<BusStopLine> Stations = new List<BusStopLine>();

        List<BusLine> lines = new List<BusLine>();

        public LinesCollection(List<BusLine> _lines) => lines = _lines;
        public List<BusLine> Lines { get; set; }
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
            }
            else if (count == 1)
            {
                if (temp.FirstStop == newLine.LastStop && temp.LastStop == newLine.FirstStop)
                {
                    lines.Add(newLine);
                    Console.WriteLine("the addition successed");
                }
                else
                    Console.WriteLine("the first or last station of the new line isn't valid");
            }
            else
                Console.WriteLine("the addition failed, the line is in the lines' list 2 time alraedy");
        }

        public void RemoveLine(int lineNumber, int firstStationNum)
        {
            BusLine temp;
            IEnumerator enumerator = lines.GetEnumerator();
            while (enumerator.MoveNext())
            {
                temp = (BusLine)enumerator.Current;
                if (temp.GetBusLine == lineNumber &&
                    temp.FirstStop.GetStop.BusStopNumber == firstStationNum)
                {
                    lines.Remove(temp);
                    return;
                }
            }
            Console.WriteLine("the requested line not found");
        }

        public List<BusLine> GetLinesinStation(int busId)
        {
            List<BusLine> StationLines = new List<BusLine>();
            foreach (BusLine line in lines)
            {
                foreach (BusStopLine station in line.stations)
                {
                    if (station.GetStop.BusStopNumber == busId)
                        StationLines.Add(line);
                }
            }

           // if (StationLines.Count == 0) throw Exception;//////////////////////////--------------------------
            return StationLines;
        }

        public List<BusLine> SortLines()
        {
            List<BusLine> SortedLines = new List<BusLine>();
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = i + 1; j < lines.Count; j++)
                {
                    int check = ComparTwoLine(lines[i], lines[j]);
                    if (check < 0)
                    {
                        BusLine temp = lines[i];
                        lines[i] = lines[j];
                        lines[j] = temp;
                    }
                }
            }
            return SortedLines;
        }
        public int ComparTwoLine(BusLine busLineA, BusLine busLineB)
        {
            return busLineA.CompareTo(busLineB);
            ////check in main and print wich is bigger--------------------------------
        }
        public List<BusLine> this[int index]
        {
            get
            {
                List<BusLine> indexLines = new List<BusLine>();
                foreach (BusLine line in lines)
                {
                    if (line.GetBusLine == index)
                        indexLines.Add(line);
                }
                //if (indexLines.Count == 0) throw;////////////-------------------------------------
                return indexLines;
            }
        }


    }
}
