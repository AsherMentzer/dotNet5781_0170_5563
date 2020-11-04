using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0170_5563
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> b1 = new List<int> { 1, 2, 3 };
            Console.WriteLine(b1.Count);
            b1.Insert(1, 8);
            try
            {
                b1.Insert(7, 9);
            }
            catch (ArgumentOutOfRangeException) { Console.WriteLine("the index is out of the range");}
            foreach (int i in b1)
                Console.Write(i + ", ");
            Console.ReadKey();
        }
    }
}
