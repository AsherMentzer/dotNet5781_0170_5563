using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_0170_5563
{
    public class Bus
    {
        ///fields
        private string id;
        private DateTime ActiveDate;
        private double kilometrage;
        private bool dangerous;
        private double fuel;  //we asuming that when you enter a bus its tank already full
        private double kmAfterBusFixing;  //we asuming  that when you enter a bus is fixed
        private DateTime lastFix = DateTime.Now;

        ///properties
        public string GetId { get => id; }
        public DateTime active { get => ActiveDate; }
        public bool Dangerous { get => dangerous; set => dangerous = value; }
        public double Kilometrage { get => kilometrage; set => kilometrage = value; }
        public double Fuel { get => fuel; set => fuel = value; }
        public double KmForTravel { get => kmAfterBusFixing; set => kmAfterBusFixing = value; }
        public DateTime LastFix { get => lastFix; set => lastFix = value; }

        //constructor
        public Bus(string newId, int year, int month, int day, double _kilometrage = 0,
            double _fuel = 1200, double _kmAfterBusFixing = 20000)
        {
            id = newId;
            ActiveDate = new DateTime(year, month, day);
            kilometrage = _kilometrage;
            fuel = _fuel;
            kmAfterBusFixing = _kmAfterBusFixing;
        }

        /// <summary>
        /// the function copies the id and adds hyphens betwwen the parts of the id
        /// </summary>
        /// <returns>the function returns string that contain the id with hyphens between the parts of the id</returns>
        public string PrintID()
        {
            string temp = id.Insert(5, "-");
            if (id.Length == 7)
            {
                temp = temp.Insert(2, "-");
                temp = temp + '\0';
            }
            else
                temp = temp.Insert(3, "-");
            return temp;
        }
    }
}