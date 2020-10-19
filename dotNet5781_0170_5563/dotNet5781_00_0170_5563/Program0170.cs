using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_0170_5563
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome0170();
            welcome5563();
            Console.ReadKey();
        }
        static partial void welcome5563();
        private static void welcome0170()
        {
            Console.Write("Enter your name: ");
            String name0 = Console.ReadLine();
            Console.WriteLine("{ name0 }, welcome to my first console application");
        }
    }
}
