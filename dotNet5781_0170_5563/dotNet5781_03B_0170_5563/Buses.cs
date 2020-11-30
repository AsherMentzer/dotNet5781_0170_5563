//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace dotNet5781_03B_0170_5563
//{
//    public class Buses
//    {
//        List<Bus> myBuses = new List<Bus>();
//       public List<Bus> GetBuses { get=>myBuses; set=>myBuses=value; }

//        public void buses()
//        {

//            myBuses.Add(new Bus("1234567", new DateTime(2017, 2, 20), new DateTime(2019, 10, 20), 100000));
//            myBuses.Add(new Bus("12345678", new DateTime(2018, 2, 20), new DateTime(2020, 10, 20), 100000, _kmAfterBusFixing: 19980));
//            myBuses.Add(new Bus("87654321", new DateTime(2019, 2, 20), new DateTime(2019, 10, 20), 100000, 10));

//            for (int i = 0; i < 7; ++i)
//            {
//                bool flag = true;
//                Bus b1;
//                do
//                {
//                    b1 = new Bus();
//                    foreach (Bus b in myBuses)
//                    {
//                        if (b.GetId == b1.GetId)
//                        {
//                            flag = false;
//                            break;
//                        }
//                    }
//                } while (!flag);
//                myBuses.Add(b1);
//            }



//        }
//    }
//}
