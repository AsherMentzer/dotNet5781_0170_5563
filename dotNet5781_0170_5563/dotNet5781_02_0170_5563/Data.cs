using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
    class Data
    {
      public  LinesCollection lines = new LinesCollection();
      public  List<BusStation> busStations = new List<BusStation>();
        static Random r = new Random(DateTime.Now.Millisecond);
        public Data()
        {
            /*areas a1 = areas.Center;
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
             BusStationLine bl11 = new BusStationLine(s11);

          BusLine l1 = new BusLine(1, s31, s40, a1);
          BusLine l2 = new BusLine(11, s10, s1, a1);
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


          lines.AddLine(l1);
          lines.AddLine(l2);
          lines.AddLine(l3);
          lines.AddLine(l4);
          lines.AddLine(l5);
          lines.AddLine(l6);
          lines.AddLine(l7);
          lines.AddLine(l8);
          lines.AddLine(l9);
          lines.AddLine(l10);

          {
              s1,s2,s3,s4,s5,s6,s7,s8,s9,s10,s11,s12,s13,s14,s15
          ,s16,s17,s18,s19,s20,s21,s22,s23,s24,s25,s26,s27,s28,s29,s30,s31,s32,s33,s34,s35,s36,s37,s38,s39,s40};*/

          //  List<BusStation> busStations = new List<BusStation>();

          //  LinesCollection lines = new LinesCollection();

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
            for (int i = 0; i < 8; ++i)
            {
                int area = r.Next(1, 6);
                List<BusStation> lstA = new List<BusStation>();
                int numOfStations = r.Next(3, 40);
                int sts;
                for (int j = 0; j < numOfStations; ++j)
                {
                    sts = r.Next(0, 40);
                    //bool flag = true;
                   /* do
                    {
                        sts = r.Next(0, 40);
                        foreach (var station in lstA)
                        {
                            if (station.BusStationNumber == busStations[sts].BusStationNumber)
                            {
                                flag = false;
                                break;
                            }
                        }
                    } while (!flag);*/
                    lstA.Add(busStations[sts]);
                }
                BusLine newLine = new BusLine(i, (areas)area, lstA);
                lines.AddLine(newLine);
            }
            List<BusStation> lst = new List<BusStation>();
            for (int i = 0; i < 22; ++i)
            {
                lst.Add(busStations[i]);
            }

            BusLine l1 = new BusLine(100, (areas)2, lst);
            lines.AddLine(l1);

            List<BusStation> lstB = new List<BusStation>();
            for (int i = 39; i > 18; --i)
            {
                lstB.Add(busStations[i]);
            }

            BusLine l2 = new BusLine(200, (areas)2, lstB);
            lines.AddLine(l2);
        }





           
    }
}
