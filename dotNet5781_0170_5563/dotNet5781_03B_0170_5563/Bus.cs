﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_0170_5563
{

    public enum Status { ready, traveling, fuelling, fixing };
    public class Bus
    {
        static Random r = new Random(DateTime.Now.Millisecond);
        ///fields
        private string licenseId;
        private DateTime activeDate;
        private double kilometrage;
        private bool dangerous;
        private double fuel;
        private double kmAfterBusFixing;
        private DateTime lastFix = DateTime.Now;
        private Status status = Status.ready;
        ///properties
        public string GetId { get => licenseId; }
        public DateTime Active { get => activeDate; }
        public bool Dangerous { get => dangerous; set => dangerous = value; }
        public double Kilometrage { get => kilometrage; set => kilometrage = value; }
        public double Fuel { get => fuel; set => fuel = value; }
        public double KmForTravel { get => kmAfterBusFixing; set => kmAfterBusFixing = value; }
        public DateTime LastFix { get => lastFix; set => lastFix = value; }
        public Status Status { get; set; }
        //public Status myStatus { get=>status; set=>status=value; }
        /// <summary>
        /// constructor how get at least the id and the active date for new bus
        /// all the others is for old bus
        /// </summary>
        /// <param name="newId">licenseId</param>
        /// <param name="year">year of the active date</param>
        /// <param name="month">month of the active date</param>
        /// <param name="day">day of the active date</param>
        /// <param name="_kilometrage">total kilometrage of the bus default is 0 </param>
        /// <param name="_fuel">fuel in bus default is 1200(full tank)</param>
        /// <param name="_kmAfterBusFixing">how much he can travel till he will need fix
        /// the default is 20000 we assuming he fixed when you get the bus</param>
        public Bus(string newId, int year, int month, int day, DateTime _lastFix, double _kilometrage = 0,
            double _fuel = 1200, double _kmAfterBusFixing = 20000)
        {
            licenseId = newId;
            activeDate = new DateTime(year, month, day);
            kilometrage = _kilometrage;
            fuel = _fuel;
            kmAfterBusFixing = _kmAfterBusFixing;
            lastFix = _lastFix;
        }

        public Bus()
        {
            activeDate = randDate();
            if (activeDate.Year < 2018)
                licenseId = r.Next(0000000, 9999999).ToString();
            else
                licenseId = r.Next(00000000, 99999999).ToString();
            kilometrage = 0;
            fuel = 1200;
            kmAfterBusFixing = 0;
        }
        public DateTime randDate()
        {
            int year = r.Next(1990,DateTime.Now.Year);
            int month = r.Next(1, 12);
            DateTime d = new DateTime(year, month, 5);
            return d;
        }
        /// <summary>
        /// the function copies the id and adds hyphens betwwen the parts of the id
        /// </summary>
        /// <returns>the function returns string that contain the id with hyphens 
        /// between the parts of the id</returns>
        public string PrintID()
        {
            string temp = licenseId.Insert(5, "-");
            if (licenseId.Length == 7)
            {
                temp = temp.Insert(2, "-");
                temp = temp + '\0';
            }
            else
                temp = temp.Insert(3, "-");
            return temp;
        }

        public override string ToString()
        {
            return PrintID();
        }
    }
}