using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
    public class LinesCollection : IEnumerable
    {
        List<BusLine> lines = new List<BusLine>();

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

    }
}
