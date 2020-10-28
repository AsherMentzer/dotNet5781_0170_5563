using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus
{
    public class Bus
    {
        ///fields
        private string id;
        private DateTime ActiveDate;
        private int kilometrage;
        private bool dangerous;
        private int fuel;
        private int kmAfterBusFixing = 20000;
        private DateTime lastFix;
       
        ///properties
        public string GetId { get => id; }
        public DateTime active { get => ActiveDate; }
        public bool Dangerous { get => dangerous; set => dangerous = value; }
        public int Kilometrage { get => kilometrage; set => kilometrage = value; }
        public int Fuel { get => fuel; set => fuel = value; }
        public int KmForTravel { get => kmAfterBusFixing; set => kmAfterBusFixing = value; }
        public DateTime LastFix { get => lastFix; set => lastFix = value; }
        
        //constructor
        public Bus(string newId, int year, int month, int day)
        {
            id = newId;
            ActiveDate = new DateTime(year, month, day);
            lastFix = DateTime.Now;
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