using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
    /// <summary>
    ///class for initial 40 stations and 10 lines that in each station will stop at least
    ///one line and in at least 10 station will stop more that 1 line 
    /// </summary>
    public class Data
    {
        public LinesCollection lines = new LinesCollection();//list of all the lines
        public List<BusStation> busStations = new List<BusStation>();//list of all the stations
        static Random r = new Random(DateTime.Now.Millisecond);
        public Data()
        {
            ///get 40 station and random deatails
            for (int i = 0; i < 40; ++i)
            {
                bool flag = true;
                int num;
                do
                {
                    num = r.Next(1, 1000000);
                    foreach (var station in busStations)
                    {
                        if (station.BusStationNumber == num)
                        {
                            flag = false;
                            break;
                        }
                    }
                } while (!flag);
                BusStation busStation = new BusStation(num);
                busStations.Add(busStation);
            }

            //for (int i = 0; i < 8; ++i)
            //{
            //    int area = r.Next(1, 6);
            //    List<BusStation> lstA = new List<BusStation>();
            //    int numOfStations = r.Next(2,3);
            //    int sts;
            //    for (int j = 0; j < numOfStations; ++j)
            //    {
            //       // sts = r.Next(0,40);
            //        bool flag = true;
            //       do
            //        {
            //            sts = r.Next(0, 40);
            //            foreach (var station in lstA)
            //            {
            //                if (station.BusStationNumber == busStations[sts].BusStationNumber)
            //                {
            //                    flag = false;
            //                    break;
            //                }
            //            }
            //        } while (!flag);
            //        lstA.Add(busStations[i+j]);
            //    }
            //    BusLine newLine = new BusLine(i, (areas)area, lstA);
            //    lines.AddLine(newLine);
            //}
            //initial 10 lines
            List<BusStation> lst1 = new List<BusStation>
            { busStations[1], busStations[3], busStations[5], busStations[7], busStations[8], };
            BusLine l1 = new BusLine(1, (areas)1, lst1);
            lines.AddLine(l1);

            List<BusStation> lst2 = new List<BusStation>
            { busStations[10], busStations[12], busStations[14], busStations[16], busStations[18], };
            BusLine l2 = new BusLine(2, (areas)1, lst2);
            lines.AddLine(l2);

            List<BusStation> lst3 = new List<BusStation>
            { busStations[18], busStations[16], busStations[14], busStations[12], busStations[10], };
            BusLine l3 = new BusLine(280, (areas)1, lst3);
            lines.AddLine(l3);

            List<BusStation> lst4 = new List<BusStation>
            { busStations[21], busStations[31], busStations[35], busStations[17], busStations[18], };
            BusLine l4 = new BusLine(3, (areas)4, lst4);
            lines.AddLine(l4);

            List<BusStation> lst5 = new List<BusStation>
            { busStations[7], busStations[37], busStations[25], busStations[33], busStations[9], };
            BusLine l5 = new BusLine(4, (areas)5, lst5);
            lines.AddLine(l5);

            List<BusStation> lst6 = new List<BusStation>
            { busStations[10], busStations[23], busStations[11], busStations[12], busStations[22], };
            BusLine l6 = new BusLine(66, (areas)3, lst6);
            lines.AddLine(l6);

            List<BusStation> lst7 = new List<BusStation>
            { busStations[23], busStations[34], busStations[15], busStations[13], busStations[36], };
            BusLine l7 = new BusLine(92, (areas)3, lst7);
            lines.AddLine(l7);

            List<BusStation> lst8 = new List<BusStation>
            { busStations[11], busStations[21], busStations[31], busStations[1], busStations[12], };
            BusLine l8 = new BusLine(402, (areas)1, lst8);
            lines.AddLine(l8);

            List<BusStation> lst9 = new List<BusStation>();
            for (int i = 0; i < 22; ++i)
            {
                lst9.Add(busStations[i]);
            }

            BusLine l9 = new BusLine(100, (areas)2, lst9);
            lines.AddLine(l9);

            List<BusStation> lst10 = new List<BusStation>();
            for (int i = 39; i > 18; --i)
            {
                lst10.Add(busStations[i]);
            }

            BusLine l10 = new BusLine(200, (areas)2, lst10);
            lines.AddLine(l10);
        }






    }
}
